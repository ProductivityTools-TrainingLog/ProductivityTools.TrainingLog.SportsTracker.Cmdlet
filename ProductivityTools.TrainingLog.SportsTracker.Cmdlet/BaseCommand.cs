using ProductivityTools.PSCmdlet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.TrainingLog.SportsTracker.Cmdlet
{
    public abstract class BaseCommand<T> : PSCmdlet.PSCommandPT<T> where T : PSCmdletPT
    {
        public BaseCommand(T cmdletType) : base(cmdletType)
        {
        }
    }
}
