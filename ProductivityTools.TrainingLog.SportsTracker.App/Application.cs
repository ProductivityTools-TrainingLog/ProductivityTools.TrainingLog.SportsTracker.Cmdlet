using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    public class Application
    {
        private readonly string TrainingLogApiAddress;

        public Application(string trainingLogApiAddress)
        {
            this.TrainingLogApiAddress = trainingLogApiAddress;
        }


        private HttpPostClient HttpPostClient
        {
            get
            {
                HttpPostClient client = new HttpPostClient(true);
                client.SetBaseUrl(this.TrainingLogApiAddress);
                return client;
            }
        }

        public void ExportTrainingsToSportTracker(string login, string password)
        {
            var sportsTrackingTrainings=GetSportsTrackerTrainings(login, password);

            var trinings = GetTrainingsFromTrainingLog();
            foreach (var training in trinings)
            {
                if (!sportsTrackingTrainings.Any(x => training.ExternalIdList.ContainsKey("SportsTracker") && x.WorkoutKey == training.ExternalIdList["SportsTracker"]))
                {
                    var trainingDetails = GetTrainingsDetailsFromTrainingLog(training.TrainingId);
                    PushTrainingsToSportsTracker(training);
                }
            }
        }

        private List<Training> GetTrainingsFromTrainingLog()
        {
            List<Training> result2 = HttpPostClient.PostAsync<List<Training>>("Training", "List", "pwujczyk").Result;
            return result2;
        }

        private Training GetTrainingsDetailsFromTrainingLog(int trainingId)
        {
            string address = string.Format($"Get/?trainingId={trainingId}");
            Training result2 = HttpPostClient.PostAsync<Training>("Training", address, null).Result;
            return result2;
        }

        private List<ProductivityTools.SportsTracker.SDK.Model.Training> GetSportsTrackerTrainings(string login, string password)
        {
            var sports = new ProductivityTools.SportsTracker.SDK.SportsTracker(login, password);
            var trainings=sports.GetTrainingList();
            return trainings;
        }

        private void PushTrainingsToSportsTracker(Training training)
        {

        }
    }
}
