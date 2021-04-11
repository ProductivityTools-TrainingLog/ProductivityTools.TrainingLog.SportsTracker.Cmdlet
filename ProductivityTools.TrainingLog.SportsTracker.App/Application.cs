﻿using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    public class Application
    {
        //private readonly string TrainingLogApiAddress;
        //private readonly string Login;
        //private readonly string Password;

        private SportsTracker SportsTracker;
        private TrainingLog TrainingLog;

        public Application(string trainingLogApiAddress, string sportTrackerLogin, string sportTrackerPassword)
        {
            this.SportsTracker = new SportsTracker(sportTrackerLogin,sportTrackerPassword);
            this.TrainingLog = new TrainingLog(trainingLogApiAddress);
            //this.TrainingLogApiAddress = trainingLogApiAddress;
            //this.Login = sportTrackerLogin;
            //this.Password = sportTrackerPassword;
        }


        //private HttpPostClient HttpPostClient
        //{
        //    get
        //    {
        //        HttpPostClient client = new HttpPostClient(true);
        //        client.SetBaseUrl(this.TrainingLogApiAddress);
        //        return client;
        //    }
        //}


        public void ExportTrainingsToSportTracker(string account)
        {
            var sportsTrackingTrainings = this.SportsTracker.GetSportsTrackerTrainings();
            var trainingLogTrainings = this.TrainingLog.GetTrainingsFromTrainingLog(account);


            foreach (var training in trainingLogTrainings)
            {
                if (!sportsTrackingTrainings.Any(x => training.ExternalIdList.ContainsKey("SportsTracker") && x.WorkoutKey == training.ExternalIdList["SportsTracker"]))
                {
                    var trainingDetails = this.TrainingLog.GetTrainingsDetailsFromTrainingLog(training.TrainingId);
                    var externalTrainingId = this.SportsTracker.PushTrainingsToSportsTracker(trainingDetails);
                    this.TrainingLog.UpdateExternalTrainingId(training.TrainingId, externalTrainingId);
                }
            }
        }

        public void ImportTrainingsFromSportTracker(string account)
        {
            var sportsTrackingTrainings = this.SportsTracker.GetSportsTrackerTrainings();
            var trainingLogTrainings = this.TrainingLog.GetTrainingsFromTrainingLog(account);

            foreach(var sportsTrackingTraining in sportsTrackingTrainings)
            {
                if (trainingLogTrainings.Any(x=>x.ExternalIdList.ContainsKey("SportsTracker") && x.ExternalIdList["SportsTracker"]==sportsTrackingTraining.WorkoutKey))
                {
                    Console.WriteLine("Training exists");
                }
                else
                {
                    Console.WriteLine("Training missing");

                }
            }

        }
    }
}
