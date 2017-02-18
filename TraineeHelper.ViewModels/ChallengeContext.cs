using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class ChallengeContext
    {
        public string ChallengeId { get; set; }

        public string TraineeId { get; set; }

        public string TrainerId { get; set; }

        public string ChallengeType { get; set; }

        public string ChallengeValue { get; set; }

        public string ChallengeTime { get; set; }

        public bool IsCompleted { get; set; }

        public ChallengeStatus ChallengeStatus { get; set; }
    }
}
