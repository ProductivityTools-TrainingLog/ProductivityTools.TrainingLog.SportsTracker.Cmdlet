using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using ProductivityTools.TrainingLog.SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    class TrainingLog
    {
        private HttpPostClient HttpPostClient { get; set; }
        private ProductivityTools.TrainingLog.SDK.TrainingLog TrainingLogSdk { get; set; }

        public TrainingLog(string trainingLogApiAddress, bool logging)
        {
            this.HttpPostClient = new HttpPostClient(logging);
            this.HttpPostClient.SetBaseUrl(trainingLogApiAddress);
            this.TrainingLogSdk = new SDK.TrainingLog(trainingLogApiAddress, logging);
        }

        public List<Training> GetTrainingsFromTrainingLog(string account, DateTime fromDate)
        {
            var result = this.TrainingLogSdk.TrainingList(account, fromDate);
            return result;
        }

        public Training GetTrainingsDetailsFromTrainingLog(int trainingId)
        {
            string address = string.Format($"Get/?trainingId={trainingId}");
            Training result2 = HttpPostClient.PostAsync<Training>("Training", address, null).Result;
            return result2;
        }

        public void UpdateExternalTrainingId(int trainingId, string externalTrainingId)
        {
            string address = string.Format($"UpdateExternalTrainingId/?trainingId={trainingId}&externalTrainingId={externalTrainingId}&externalSystemName=SportsTracker");
            object result2 = HttpPostClient.PostAsync<object>("Training", address, null).Result;
        }

        public void AddTraining(string account,
            ProductivityTools.SportsTracker.SDK.Model.Training stTraining,
            List<ProductivityTools.SportsTracker.SDK.Model.TrainingImage> images,
            Stream gpx)
        {
            Training training = new Training();
            training.Account = account;
            training.Application = "SportsTracker";
            training.Calories = stTraining.EnergyConsumption;
            //  training.Comment = stTraining.Description;
            training.Distance = Convert.ToDecimal(stTraining.Distance);
            training.Duration = Convert.ToInt32(stTraining.Duration.TotalSeconds);
            training.Start = stTraining.StartDate;
            training.End = training.Start.AddSeconds(training.Duration);
            training.ExternalIdList = new Dictionary<string, string>();
            training.ExternalIdList.Add("SportsTracker", stTraining.WorkoutKey);
            training.Name = stTraining.Description;
            training.Source = "TrainingLog.SportsTracker.Cmdlet";
            var trainingMap = TrainingMapConfiguration.GetTraining(stTraining.TrainingType);
            training.Sport = trainingMap.TrainingLogTrainingType;
            training.ExternalIdList = new Dictionary<string, string>();
            training.ExternalIdList.Add("SportsTracker", stTraining.WorkoutKey);

            training.Pictures = new List<byte[]>();
            foreach (var image in images)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Stream.CopyTo(ms);
                    training.Pictures.Add(ms.ToArray());
                }
            }

            if (gpx!=null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    gpx.CopyTo(ms);
                    training.Gpx = ms.ToArray();
                }
            }

            TrainingLogSdk.PostTraining(training);
        }
    }
}
