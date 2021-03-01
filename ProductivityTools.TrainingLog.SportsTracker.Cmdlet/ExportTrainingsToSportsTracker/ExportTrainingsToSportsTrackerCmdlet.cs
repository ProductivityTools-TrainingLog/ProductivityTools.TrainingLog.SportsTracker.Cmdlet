using System;
using System.Management.Automation;

namespace ProductivityTools.TrainingLog.SportsTracker
{
    [Cmdlet("Export","TrainingsToSportTracker")]
    public class ExportTrainingsToSportsTrackerCmdlet : ProductivityTools.PSCmdlet.PSCmdletPT
    {
        protected override void ProcessRecord()
        {
            WriteVerbose("Hello from Export-TrainingsToSportTracker");
            base.ProcessRecord();
        }
    }
}
