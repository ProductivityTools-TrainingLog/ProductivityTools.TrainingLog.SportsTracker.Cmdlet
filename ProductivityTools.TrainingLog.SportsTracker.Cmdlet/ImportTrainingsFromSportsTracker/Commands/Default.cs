using ProductivityTools.TrainingLog.SportsTracker.ImportTrainingsFromSportsTracker;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.TrainingLog.SportsTracker.Cmdlet.ImportTrainingsFromSportsTracker.Commands
{
    public class Default : BaseCommand<ImportTrainingsFromSportsTrackerCmdlet>
    {
        public Default(ImportTrainingsFromSportsTrackerCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
