using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class TrainingPlanContext
    {
        public string Id { get; set; }
        public string TrainerId { get; set; }
        public string TraineeId { get; set; }
        public string PlanName { get; set; }
        public string TrainerName { get; set; }
        public string TraineeName { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public List<ExerciseContext> Exercises { get; set; }
        public TrainingPlanStatus TrainingPlanStatus { get; set; }
    }
}
