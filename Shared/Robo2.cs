using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Robo2 : RobocopyRunnerVM
    {
        public Robo2(Action<string> updateOutput, Action<string> updateError) : base(updateOutput, updateError)
        {
        }

        public string NewProperty { get; set; }
    }
}
