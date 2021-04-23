using ProductivityTools.TrainingLog.SportsTracker.Cmdlet.ExportTrainingsToSportsTracker.Commands;
using System;
using System.Management.Automation;

namespace ProductivityTools.TrainingLog.SportsTracker
{
    [Cmdlet("Export","TrainingsToSportTracker")]
    public class ExportTrainingsToSportsTrackerCmdlet : ProductivityTools.PSCmdlet.PSCmdletPT
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
            WriteVerbose("Hello from Export-TrainingsToSportTracker");
            base.AddCommand(new Default(this));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
