using ProductivityTools.TrainingLog.SportsTracker.Cmdlet.ImportTrainingsFromSportsTracker.Commands;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.TrainingLog.SportsTracker.ImportTrainingsFromSportsTracker
{
    [Cmdlet("Import", "TrainingsFromSportTracker")]
    public class ImportTrainingsFromSportsTrackerCmdlet : ProductivityTools.PSCmdlet.PSCmdletPT
    {
        [Parameter(Mandatory = false, HelpMessage = "Login to SportsTracker website")]
        public string Login { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Password to SportsTracker website")]
        public string Password { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Account from TrainingLog application")]
        public string Account { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "TrainingLog API address")]
        public string TrainingLogApiAddress { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Exports only trainings equals or later than date provided. If not set all trainings taken")]
        public DateTime FromDate { get; set; }

        protected override void ProcessRecord()
        {
            WriteVerbose("Hello from Import-TrainingsToSportTracker");
            base.AddCommand(new Default(this));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
