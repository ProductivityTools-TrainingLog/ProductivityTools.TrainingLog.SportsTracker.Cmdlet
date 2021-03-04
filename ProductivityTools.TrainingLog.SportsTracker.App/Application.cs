using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using System;
using System.Collections.Generic;

namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    public class Application
    {
        private readonly string TrainingLogApiAddress;

        public Application(string trainingLogApiAddress)
        {
            this.TrainingLogApiAddress = trainingLogApiAddress;
        }

        public void ExportTraingsToSportTracker()
        {
            var trinings=GetTrainingsFromTrainingLog();
            foreach(var training in trinings)
            {
                PushTrainingsToSportsTracker(training);
            }
        }

        private List<Training> GetTrainingsFromTrainingLog()
        {
            HttpPostClient client = new HttpPostClient(true);
            client.SetBaseUrl(this.TrainingLogApiAddress);

            List<Training> result2 = client.PostAsync<List<Training>>("Training", "List", "pwujczyk" ).Result;
            return result2;
        }

        private void PushTrainingsToSportsTracker(Training training)
        {

        }
    }
}
