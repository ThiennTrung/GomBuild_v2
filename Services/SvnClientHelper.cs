using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

public sealed class SvnExecResult
{
    public int ExitCode { get; set; }
    public string StdOut { get; set; } = "";
    public string StdErr { get; set; } = "";
    public bool Success => ExitCode == 0;
}

public sealed class SvnClientHelper
{
    private readonly string _svnExe;
    private readonly string _workingCopyPath;
    private readonly string _username;
    private readonly string _password;

    /// <param name="svnExe">
    /// Full path to svn.exe. If null/empty, uses "svn" on PATH.
    /// On Windows with TortoiseSVN: usually "C:\Program Files\TortoiseSVN\bin\svn.exe".
    /// </param>
    public SvnClientHelper(
        string workingCopyPath,
        string svnExe = null,
        string username = null,
        string password = null)
    {
        _workingCopyPath = workingCopyPath ?? throw new ArgumentNullException(nameof(workingCopyPath));
        _svnExe = string.IsNullOrWhiteSpace(svnExe) ? DefaultSvnPath() : svnExe;
        _username = username;
        _password = password;
    }

    public Task<SvnExecResult> AddAsync(string path, bool force = false, CancellationToken ct = default) =>
        RunAsync($"add {(force ? "--force " : "")}\"{path}\"", ct);

    /// <summary>
    /// Update working copy before committing (avoids out-of-date errors).
    /// </summary>
    public Task<SvnExecResult> UpdateAsync(string path = null, CancellationToken ct = default) =>
        RunAsync($"update {(string.IsNullOrWhiteSpace(path) ? "" : $"\"{path}\"")}", ct);

    /// <summary>
    /// Check if current credentials can commit by doing a dry-run.
    /// Returns Success=true if dry-run commit exits with 0.
    /// </summary>
    public Task<SvnExecResult> CheckCommitPermissionAsync(CancellationToken ct = default) =>
        RunAsync("commit --dry-run -m \"permission check\"", ct);

    public Task<SvnExecResult> CommitAsync(string targetPathOrDot, string message, CancellationToken ct = default) =>
        RunAsync($"commit \"{targetPathOrDot}\" -m \"{EscapeCommitMsg(message)}\"", ct);

    public Task<SvnExecResult> InfoAsync(string target = ".", CancellationToken ct = default) =>
        RunAsync($"info \"{target}\"", ct);

    public Task<SvnExecResult> LsAsync(string urlOrPath, CancellationToken ct = default) =>
        RunAsync($"ls \"{urlOrPath}\"", ct);

    // ---- core runner ----
    private async Task<SvnExecResult> RunAsync(string arguments, CancellationToken ct)
    {
        var psi = new ProcessStartInfo
        {
            FileName = _svnExe,
            Arguments = BuildArgsWithAuth(arguments),
            WorkingDirectory = _workingCopyPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var proc = new Process { StartInfo = psi, EnableRaisingEvents = true };

        var tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
        proc.OutputDataReceived += (s, e) => { if (e.Data != null) _stdoutWriter.WriteLine(e.Data); };
        proc.ErrorDataReceived += (s, e) => { if (e.Data != null) _stderrWriter.WriteLine(e.Data); };

        string stdout, stderr;
        using (_stdoutWriter = new StringWriter())
        using (_stderrWriter = new StringWriter())
        {
            try
            {
                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                await WaitForExitAsync(proc, ct).ConfigureAwait(false);

                stdout = _stdoutWriter.ToString();
                stderr = _stderrWriter.ToString();
                return new SvnExecResult { ExitCode = proc.ExitCode, StdOut = stdout, StdErr = stderr };
            }
            catch (OperationCanceledException)
            {
                try { if (!proc.HasExited) proc.Kill(true); } catch { /* ignore */ }
                throw;
            }
        }
    }

    private StringWriter _stdoutWriter;
    private StringWriter _stderrWriter;

    private static Task WaitForExitAsync(Process process, CancellationToken ct)
    {
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        void Handler(object s, EventArgs e) => tcs.TrySetResult(true);
        process.Exited += Handler;

        if (process.HasExited)
        {
            process.Exited -= Handler;
            return Task.CompletedTask;
        }

        ct.Register(() => tcs.TrySetCanceled());
        return tcs.Task.ContinueWith(_ =>
        {
            process.Exited -= Handler;
        }, TaskScheduler.Default);
    }

    private string BuildArgsWithAuth(string args)
    {
        // Non-interactive avoids credential prompts that would hang the process
        var auth = "--non-interactive ";
        // If talking to HTTPS and you expect self-signed certs, you can add:
        // auth += "--trust-server-cert-failures=unknown-ca,cn-mismatch,expired,not-yet-valid,other ";
        if (!string.IsNullOrEmpty(_username))
            auth += $"--username \"{_username}\" ";
        if (!string.IsNullOrEmpty(_password))
            auth += $"--password \"{_password}\" ";
        return auth + args;
    }

    private static string EscapeCommitMsg(string msg)
        => (msg ?? "").Replace("\"", "\\\"");

    private static string DefaultSvnPath()
    {
        // Use "svn" on PATH. On Windows, try common TortoiseSVN location as a fallback.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return "svn";

        var guess = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "TortoiseSVN", "bin", "svn.exe");
        return File.Exists(guess) ? guess : "svn";
    }
}
