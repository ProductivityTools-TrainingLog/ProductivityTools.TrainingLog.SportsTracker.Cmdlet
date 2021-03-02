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
            GetTrainingsFromTrainingLog();
        }

        private void GetTrainingsFromTrainingLog()
        {
            HttpPostClient client = new HttpPostClient(true);
            client.SetBaseUrl(this.TrainingLogApiAddress);

            List<Training> result2 = client.PostAsync<List<Training>>("Training", "Get", "pwujczyk" ).Result;
            Console.WriteLine(result2);
        }

        private void PushTrainingsToSportsTracker()
        {

        }
    }
}
