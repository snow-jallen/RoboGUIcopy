using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Shared
{
    public class RobocopyRunnerVM : INotifyPropertyChanged
    {
        public RobocopyRunnerVM()
        {
            JustDoIt = new BasicCommand(
                //execute
                () => RoboRun(Source, Destination, Recursive, Restartable),
                //can execute
                () =>
                    {
                        return (Source != null && Destination != null);
                    }
                );
        }
        public string Source
        {
            get => source; set
            {
                source = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JustDoIt)));
            }
        }
        public string Destination
        {
            get => destination; set
            {
                destination = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JustDoIt)));
            }
        }
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
        private string source;
        private string destination;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
            }
        }
        public ICommand JustDoIt { get; set; }


    }
}
