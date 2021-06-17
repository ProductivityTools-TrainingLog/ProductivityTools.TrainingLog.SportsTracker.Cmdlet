using ProductivityTools.SimpleHttpPostClient;
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

        public Application(string trainingLogApiAddress, string sportTrackerLogin, string sportTrackerPassword, bool logging)
        {
            this.SportsTracker = new SportsTracker(sportTrackerLogin, sportTrackerPassword, logging);
            this.TrainingLog = new TrainingLog(trainingLogApiAddress, logging);
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
                    Console.WriteLine($"Training {training.TrainingId} not exists in SportTracker - training added");
                }
                else
                {
                    Console.WriteLine($"Training {training.TrainingId} already exists in SportTracker - skipping");
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
                    Console.WriteLine($"Training {sportsTrackingTraining.WorkoutKey} already exists in TrainingLog - skipping");
                }
                else
                {
                    var gpx = this.SportsTracker.GetGpx(sportsTrackingTraining.WorkoutKey);
                    var images = this.SportsTracker.GetTrainingPhotos(sportsTrackingTraining.WorkoutKey);
                    this.TrainingLog.AddTraining(account, sportsTrackingTraining, images, gpx);
                    Console.WriteLine($"Training {sportsTrackingTraining.WorkoutKey} is missing from TrainingLog - added");
                }
            }
        }
    }
}
