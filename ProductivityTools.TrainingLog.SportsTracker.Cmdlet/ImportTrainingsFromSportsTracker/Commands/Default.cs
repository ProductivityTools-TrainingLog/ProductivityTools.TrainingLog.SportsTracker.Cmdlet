using ProductivityTools.TrainingLog.SportsTracker.App;
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
            string login = this.Cmdlet.Login ?? Login;
            string password = this.Cmdlet.Password ?? Password;
            string trainingLogApiAddress = this.Cmdlet.TrainingLogApiAddress ?? TrainingLogApiAddress;
            string account = this.Cmdlet.Account ?? Account;

            this.Cmdlet.WriteVerbose($"Login: {login}");
            this.Cmdlet.WriteVerbose($"Password: {password}");
            this.Cmdlet.WriteVerbose($"TrainingLogApiAddress: {trainingLogApiAddress}");
            this.Cmdlet.WriteVerbose($"Account: {account}");


            ValidateEmpty(login, password, trainingLogApiAddress, account);

            Application application = new Application(trainingLogApiAddress, login, password);
            application.ImportTrainingsFromSportTracker(account, this.Cmdlet.FromDate);
        }
    }
}
