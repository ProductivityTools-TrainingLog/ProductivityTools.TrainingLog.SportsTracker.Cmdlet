using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    class SportsTracker
    {
        ProductivityTools.SportsTracker.SDK.SportsTracker SportTrackerSdk { get; set; }

        public SportsTracker(string sportTrackerLogin, string sportTrackerPassword)
        {
            SportTrackerSdk = new ProductivityTools.SportsTracker.SDK.SportsTracker(sportTrackerLogin, sportTrackerPassword);
        }

        public List<ProductivityTools.SportsTracker.SDK.Model.Training> GetSportsTrackerTrainings(DateTime fromDate)
        {
            var trainings = SportTrackerSdk.GetTrainingList(fromDate);
            return trainings;
        }

        public string PushTrainingsToSportsTracker(Training training)
        {
            ProductivityTools.SportsTracker.SDK.Model.Training sdkTraining = new ProductivityTools.SportsTracker.SDK.Model.Training();
            sdkTraining.StartDate = training.Start;
            sdkTraining.Distance = decimal.ToDouble(training.Distance);
            var trainingMap = TrainingMapConfiguration.GetTraining(training.Sport);
            sdkTraining.TrainingType = trainingMap.SportsTrackerTrainingType;
            sdkTraining.Duration = TimeSpan.FromSeconds(training.Duration);
            sdkTraining.Description = $"{trainingMap.Name} {training.Name}";
            sdkTraining.EnergyConsumption = Convert.ToInt32(training.Calories);
            Console.WriteLine(sdkTraining.EnergyConsumption);
            sdkTraining.SharingFlags = 19;

            var result = this.SportTrackerSdk.AddTraining(sdkTraining, training.Gpx, training.Pictures);
            return result;
        }

        public List<ProductivityTools.SportsTracker.SDK.Model.TrainingImage> GetTrainingPhotos(string trainingId)
        {
            List<ProductivityTools.SportsTracker.SDK.Model.TrainingImage> images = this.SportTrackerSdk.GetTrainingImages(trainingId);
            return images;
        }

        public byte[] GetTrainingGpx(string trainingId)
        {
            Stream r = this.SportTrackerSdk.GetGpx(trainingId);
            byte[] br = ReadFully(r);
            return br;
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
