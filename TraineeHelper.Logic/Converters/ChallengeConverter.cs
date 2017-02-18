using DataModel.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class ChallengeConverter
    {
        public static List<ChallengeContext> ConvertToChallengesContexts(this IEnumerable<IChallenge> challenges)
        {
            List<ChallengeContext> challengesContext = new List<ChallengeContext>();
            if(null == challenges)
                return challengesContext;
            foreach(Challenge t in challenges)
            {
                challengesContext.Add(t.ConvertToChallengeContext());
            }

            return challengesContext;
        }

        public static List<Challenge> ConvertToChallenges(this IEnumerable<ChallengeContext> challengeContexts)
        {
            List<Challenge> challenges = new List<Challenge>();
            if (challengeContexts == null)
                return challenges;
            foreach(ChallengeContext t in challengeContexts)
            {
                challenges.Add(t.ConvertToChallenge());
            }

            return challenges;
        }

        public static ChallengeContext ConvertToChallengeContext(this Challenge challenge)
        {
            ChallengeContext challengeContext = new ChallengeContext();
            if (null == challenge)
                return challengeContext;
            challengeContext.ChallengeId = challenge.Id.ToString();
            challengeContext.TraineeId = challenge.TraineeId;
            challengeContext.TrainerId = challenge.TrainerId;
            //challengeContext.TrainerName = challenge.TrainerName;
            //challengeContext.TraineeName = challenge.TraineeName;
            challengeContext.IsCompleted = challenge.IsCompleted;
            //challengeContext.ChallengeName = challenge.ChallengeName;
            challengeContext.ChallengeStatus = challenge.ChallengeStatus;

            return challengeContext;
        }

        public static Challenge ConvertToNewChallenge(this ChallengeContext context)
        {
            Challenge challenge = new Challenge();
            return challenge;
        }

        public static Challenge ConvertToChallenge(this ChallengeContext context, bool generateId = false)
        {
            Challenge challenge = new Challenge();
            if (null == context)
                return challenge;
            //challenge.ChallengeName = context.ChallengeName;
            challenge.ChallengeStatus = context.ChallengeStatus;
            challenge.Description = challenge.Description;
            challenge.Id = generateId ? ObjectId.GenerateNewId() : ObjectId.Parse(context.ChallengeId);
            challenge.IsCompleted = context.IsCompleted;
            //challenge.TraineeName = context.TraineeName;
            challenge.TrainerId = context.TrainerId;
            challenge.TrainerId = context.TraineeId;

            return challenge;
        }
    }
}
