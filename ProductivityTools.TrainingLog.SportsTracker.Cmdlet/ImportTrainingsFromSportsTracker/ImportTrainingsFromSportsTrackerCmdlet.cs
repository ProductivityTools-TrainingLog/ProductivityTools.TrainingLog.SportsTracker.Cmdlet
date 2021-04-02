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


        protected override void ProcessRecord()
        {
            WriteVerbose("Hello from Import-TrainingsToSportTracker");
            base.AddCommand(new Default(this));
            base.ProcessRecord();
        }
    }
}
