using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class ChallengeManager
    {
        private ChallengeEntityService ChallengeEntityService;

        public ChallengeManager()
        {
            ChallengeEntityService = new ChallengeEntityService();
        }

        public async Task<bool> CreateChallenge(ChallengesPackage challengepkg)
        {
            List<Challenge> challenges = new List<Challenge>();

            if (null == challengepkg)
                return false;
            for(int i = 0; i < challengepkg.Challenges.Count; i++)
            {
                Challenge challenge = challengepkg.Challenges[i].ConvertToChallenge(true);
                challenges.Add(challenge);
            }

            var result = await ChallengeEntityService.CreateChallenge(challenges);
            return result;
        }

        public async Task<ChallengeContext> UpdateChallenge(ChallengeContext challengectx)
        {
            if (null == challengectx)
                return null;
            Challenge challenge = challengectx.ConvertToChallenge(false);
            var result = await ChallengeEntityService.Update(challenge);
            return result.ConvertToChallengeContext();
        }

        public async Task<Challenge> UpdateChallenge(Challenge challenge)
        {
            var result = await ChallengeEntityService.Update(challenge);
            return result;
        }

        public async Task<bool> DeleteChallenge(ChallengeContext challengectx)
        {
            if (null == challengectx)
                return false;
            Challenge challenge = challengectx.ConvertToChallenge(false);
            var result = await ChallengeEntityService.Delete(challenge.Id.ToString());
            return result;
        }

        public async Task<IEnumerable<ChallengeContext>> FindUserChallenges(string userId)
        {
            if (null == userId)
                return null;
            UserManager userManager = new UserManager();
            var result = await ChallengeEntityService.GetByUserId(userId);
            foreach (Challenge a in result)
            {
                a.TraineeName = await userManager.GetUserNameById(a.TraineeId);
                a.TrainerName = await userManager.GetUserNameById(a.TrainerId);
            }

            return result.ConvertToChallengesContexts();
        }

        public async Task<ChallengeContext> FindChallengeById(string id)
        {
            if (null == id)
                return null;

            var achievement = await ChallengeEntityService.GetById(id);
            ChallengeContext achievementctx = achievement.ConvertToChallengeContext();
            return achievementctx;
        }

    }
}
