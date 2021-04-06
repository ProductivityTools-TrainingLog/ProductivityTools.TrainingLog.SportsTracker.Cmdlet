﻿using ProductivityTools.SimpleHttpPostClient;
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
            var trainingMap = GetTraining(training.Sport);
            sdkTraining.TrainingType = trainingMap.SportsTrackerTrainingType;
            sdkTraining.Duration = TimeSpan.FromSeconds(training.Duration);
            sdkTraining.Description = $"{trainingMap.Name} {training.Name}";
            sdkTraining.EnergyConsumption = Convert.ToInt32(training.Calories);
            sdkTraining.SharingFlags = 19;

            var result = this.SportTrackerSdk.AddTraining(sdkTraining, training.Gpx, training.Pictures);
            return result;
        }

        private TrainingMap GetTraining(ProductivityTools.TrainingLog.Contract.TrainingType tlSportsType)
        {
            var dict = new List<TrainingMap>();

            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Aerobics, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Areobics));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Badminton, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Badminton));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Climbing, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Climbing));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Cycling, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Cycling));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.MountainBiking, ProductivityTools.SportsTracker.SDK.Model.TrainingType.MountainBiking));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.IceSkating, ProductivityTools.SportsTracker.SDK.Model.TrainingType.IceSkating));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Kayaking, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Kayaking));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Dancing, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Dancing));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.RollerSkating, ProductivityTools.SportsTracker.SDK.Model.TrainingType.RollerSkating));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Rowing, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Rowing));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Squash, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Squash));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Stretching, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Stretching));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Swimming, ProductivityTools.SportsTracker.SDK.Model.TrainingType.PoolSwimming));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.TradmillRunning, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Treadmill));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.WeightTraining, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Gym));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.PowerYoga, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Yoga, "#PowerYoga"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Yoga, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Yoga));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Surfing, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Surfing));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Walking, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Walking));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.NordicWalking, ProductivityTools.SportsTracker.SDK.Model.TrainingType.NorticWalking));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Orienteering, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Orienteering));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Hiking, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Hiking));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.TableTennis, ProductivityTools.SportsTracker.SDK.Model.TrainingType.TableTennis));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.CrossCountrySkiing, ProductivityTools.SportsTracker.SDK.Model.TrainingType.CrossCountrySkiing));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Box, ProductivityTools.SportsTracker.SDK.Model.TrainingType.MatirialArts, "#Box"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.MuayThai, ProductivityTools.SportsTracker.SDK.Model.TrainingType.MatirialArts, "#MuayThai"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Soccer, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Soccer));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.SkiingDownhill, ProductivityTools.SportsTracker.SDK.Model.TrainingType.AlpineSkiing));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.CrossTraining, ProductivityTools.SportsTracker.SDK.Model.TrainingType.CrossFit));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Riding, ProductivityTools.SportsTracker.SDK.Model.TrainingType.HorsebackRiding));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Running, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Running));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Skateboarding, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Skateboarding));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Tennis, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Tennis));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.TrailRunning, ProductivityTools.SportsTracker.SDK.Model.TrainingType.TrailRunning));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Zumba, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Fitness, "#Zumba"));


            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Fitness, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Fitness));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.HulaHop, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Fitness, "#HulaHop"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.StairClimbing, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Other1, "#StairClimbing"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Bilard, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Other2, "#Bilard"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Sleed, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Other3, "#Sleed"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.RopeJumping, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Fitness, "#RopeJumping"));
            dict.Add(new TrainingMap(ProductivityTools.TrainingLog.Contract.TrainingType.Pilates, ProductivityTools.SportsTracker.SDK.Model.TrainingType.Yoga));

            var r = dict.Single(x => x.TrainingLogTrainingType == tlSportsType);

            return r;
        }
    }
}
