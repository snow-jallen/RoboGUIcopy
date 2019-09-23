using Prism.Commands;
using Prism.Mvvm;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboGuiCopy.Wpf
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            var roboRunner = new RobocopyRunnerVM(
                    (output) => Output += output + "\n",
                    (error) => Error += error + "\n"
                );

            JustDoIt = new DelegateCommand(
                //execute
                () => roboRunner.RoboRun(Source, Destination, Recursive, Restartable),
                //can execute
                () =>
                {
                    return (Source != null && Destination != null);
                }
                );
        }

        private string output;
        private string error;
        private string source;
        private string destination;

        public string Source
        {
            get => source; set
            {
                source = value;
                JustDoIt.RaiseCanExecuteChanged();
            }
        }
        public string Destination
        {
            get => destination; set
            {
                destination = value;
                JustDoIt.RaiseCanExecuteChanged();
            }
        }

        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public bool Recursive { get; set; }
        public bool Restartable { get; set; }


        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                RaisePropertyChanged();
            }
        }

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand JustDoIt { get; set; }
    }
}
