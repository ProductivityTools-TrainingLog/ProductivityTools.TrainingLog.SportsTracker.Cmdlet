using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    class SportsTracker
    {
        // ProductivityTools.SportsTracker.SDK.SportsTracker sportTrackerSdk;
        ProductivityTools.SportsTracker.SDK.SportsTracker SportTrackerSdk { get; set; }
        //{
        //    get
        //    {
        //        if (sportTrackerSdk == null)
        //        {
        //            sportTrackerSdk = new ProductivityTools.SportsTracker.SDK.SportsTracker(this.Login, this.Password);
        //        }
        //        return sportTrackerSdk;
        //    }
        //}

        public SportsTracker(string sportTrackerLogin, string sportTrackerPassword)
        {
            SportTrackerSdk = new ProductivityTools.SportsTracker.SDK.SportsTracker(sportTrackerLogin, sportTrackerPassword);
        }

        public List<ProductivityTools.SportsTracker.SDK.Model.Training> GetSportsTrackerTrainings()
        {
            var trainings = SportTrackerSdk.GetTrainingList();
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
            sdkTraining.SharingFlags = 19;

            var result = this.SportTrackerSdk.AddTraining(sdkTraining, training.Gpx, training.Pictures);
            return result;
        }

      
    }
}
