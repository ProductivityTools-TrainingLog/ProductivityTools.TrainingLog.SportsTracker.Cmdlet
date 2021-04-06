using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    class TrainingLog
    {
        private HttpPostClient HttpPostClient { get; set; }

        public TrainingLog(string trainingLogApiAddress)
        {
            this.HttpPostClient = new HttpPostClient(true);
            this.HttpPostClient.SetBaseUrl(trainingLogApiAddress);
        }

        public List<Training> GetTrainingsFromTrainingLog(string account)
        {
            List<Training> result2 = HttpPostClient.PostAsync<List<Training>>("Training", "List", account).Result;
            return result2;
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
    }
}
