﻿using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    public class Application
    {
        private SportsTracker SportsTracker;
        private TrainingLog TrainingLog;

        public Application(string trainingLogApiAddress, string sportTrackerLogin, string sportTrackerPassword)
        {
            this.SportsTracker = new SportsTracker(sportTrackerLogin, sportTrackerPassword);
            this.TrainingLog = new TrainingLog(trainingLogApiAddress);
        }

        public void ExportTrainingsToSportTracker(string account, DateTime fromDate)
        {
            var sportsTrackingTrainings = this.SportsTracker.GetSportsTrackerTrainings(fromDate);
            var trainingLogTrainings = this.TrainingLog.GetTrainingsFromTrainingLog(account, fromDate);


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

        public void ImportTrainingsFromSportTracker(string account, DateTime fromDate)
        {
            var sportsTrackingTrainings = this.SportsTracker.GetSportsTrackerTrainings(fromDate);
            var trainingLogTrainings = this.TrainingLog.GetTrainingsFromTrainingLog(account, fromDate);

            foreach (ProductivityTools.SportsTracker.SDK.Model.Training sportsTrackingTraining in sportsTrackingTrainings)
            {
                if (trainingLogTrainings.Any(x => x.ExternalIdList.ContainsKey("SportsTracker") && x.ExternalIdList["SportsTracker"] == sportsTrackingTraining.WorkoutKey))
                {
                    Console.WriteLine("Training exists");
                }
                else
                {
                    var images = this.SportsTracker.GetTrainingPhotos(sportsTrackingTraining.WorkoutKey);
                    byte[] gpx = this.SportsTracker.GetTrainingGpx(sportsTrackingTraining.WorkoutKey);
                    this.TrainingLog.AddTraining(account, sportsTrackingTraining, gpx, images);
                }
            }
        }
    }
}
