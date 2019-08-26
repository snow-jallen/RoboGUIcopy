using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Shared
{
    public class RobocopyRunner : INotifyPropertyChanged
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public bool Recursive { get; set; }
        public bool Restartable { get; set; }
        public void RoboRun(string src, string dest, bool recursive, bool restartable)
        {
            //1) Create the process with arguments robocopy {source} {dest} /E(recursive) /ZB(restartable mode)
            var p = new Process();
            p.StartInfo.FileName = "roboCopy.exe";
            p.StartInfo.Arguments = $"{src} {dest} {(recursive ? "/E" : "")} {(restartable ? "/ZB" : "")} ";
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.Start();

            //2) capture stdout
            Output = String.Empty;
            Error = String.Empty;

            while (!p.HasExited)
            {
                Output += p.StandardOutput.ReadLine();

            }

            Error = p.StandardError.ReadToEnd();


        }

        private string output;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Output)));
            }
        }
        private string error;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
            }
        }



    }
}
