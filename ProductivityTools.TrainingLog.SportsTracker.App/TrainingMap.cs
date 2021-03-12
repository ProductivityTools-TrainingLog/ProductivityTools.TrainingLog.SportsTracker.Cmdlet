using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.TrainingLog.SportsTracker.App
{
    class TrainingMap
    {
        public ProductivityTools.SportsTracker.SDK.Model.TrainingType SportsTrackerTrainingType { get; set; }
        public ProductivityTools.TrainingLog.Contract.TrainingType TrainingLogTrainingType { get; set; }
        public string Name { get; set; }

        public TrainingMap(
            
            ProductivityTools.TrainingLog.Contract.TrainingType trainingLogTrainingType,
            ProductivityTools.SportsTracker.SDK.Model.TrainingType sportsTrackerTrainingType,
            string name
            )
        {
            this.SportsTrackerTrainingType = sportsTrackerTrainingType;
            this.TrainingLogTrainingType = trainingLogTrainingType;
            this.Name = name;
        }

        public TrainingMap(
          
           ProductivityTools.TrainingLog.Contract.TrainingType trainingLogTrainingType,
             ProductivityTools.SportsTracker.SDK.Model.TrainingType sportsTrackerTrainingType
           ) : this(trainingLogTrainingType, sportsTrackerTrainingType , string.Empty) { }
    }

}
