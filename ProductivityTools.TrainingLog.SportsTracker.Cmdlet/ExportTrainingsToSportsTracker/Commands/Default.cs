using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
using ProductivityTools.TrainingLog.SportsTracker.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.TrainingLog.SportsTracker.Cmdlet.ExportTrainingsToSportsTracker.Commands
{
    public class Default : PSCmdlet.PSCommandPT<ExportTrainingsToSportsTrackerCmdlet>
    {
        public Default(ExportTrainingsToSportsTrackerCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddMasterConfiguration(true)
                .Build();


            var r = configuration["login"];

            this.Cmdlet.WriteOutput("Hello Default ");
            Application application = new Application("https://localhost:5001");
            application.ExportTrainingsToSportTracker(configuration["login"],configuration["password"]);
        }
    }
}
