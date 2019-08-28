using Prism.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Shared
{
    public class RobocopyRunnerVM
    {
        private readonly Action<string> updateOutput;
        private readonly Action<string> updateError;

        public RobocopyRunnerVM(Action<string> updateOutput, Action<string> updateError)
        {
            this.updateOutput = updateOutput;
            this.updateError = updateError;
        }

        public string Output { get; private set; }
        public string Error { get; private set; }

        public void RoboRun(string src, string dest, bool recursive, bool restartable)
        {
            //1) Create the process with arguments robocopy {source} {dest} /E(recursive) /ZB(restartable mode)
            var p = new Process();
            p.StartInfo.FileName = "roboCopy.exe";
            p.StartInfo.Arguments = $"{src} {dest} {(recursive ? "/E" : "")} {(restartable ? "/ZB" : "")} ";
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;

            p.OutputDataReceived += (sender, args) => updateOutput(args.Data);
            p.ErrorDataReceived += (sender, args) => updateError(args.Data);

            Output = String.Empty;
            Error = String.Empty;

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            p.WaitForExit();
        }
    }
}
