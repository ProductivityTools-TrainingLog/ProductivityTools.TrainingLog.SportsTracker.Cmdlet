using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
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

        IConfigurationRoot Configuration
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddMasterConfiguration(true)
                    .Build();
                return configuration;
            }
        }

        protected string Login
        {
            get
            {
                return Configuration["login"];
            }
        }

        protected string Password
        {
            get
            {
                return Configuration["password"];
            }
        }

        protected string TrainingLogApiAddress
        {
            get
            {
                return Configuration["trainingLogApiAddress"];
            }
        }

        protected string Account
        {
            get
            {
                return Configuration["account"];
            }
        }


        protected void ValidateEmpty(params string[] s)
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
