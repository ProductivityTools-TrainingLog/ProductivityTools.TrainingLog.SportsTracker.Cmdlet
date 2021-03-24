using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
using ProductivityTools.TrainingLog.SportsTracker.App;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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


            string login = this.Cmdlet.Login ?? configuration["login"];
            string password = this.Cmdlet.Password ?? configuration["password"];
            string trainingLogApiAddress = this.Cmdlet.TrainingLogApiAddress ?? configuration["trainingLogApiAddress"];
            string account = this.Cmdlet.Account ?? configuration["account"];

            ValidateEmpty(login, password, trainingLogApiAddress, account);
            this.Cmdlet.WriteOutput("Hello Default ");
            Application application = new Application(trainingLogApiAddress, configuration["login"], configuration["password"]);
            application.ExportTrainingsToSportTracker(account);
        }

        private void ValidateEmpty(params string[] s)
        {
            foreach (var s1 in s)
            {
                ValidateEmpty(s1);
            }
        }

        private void ValidateEmpty(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new Exception("Some parameter hadn't been provided, please provide parameters or update master configuration");
            }
        }
    }
}
