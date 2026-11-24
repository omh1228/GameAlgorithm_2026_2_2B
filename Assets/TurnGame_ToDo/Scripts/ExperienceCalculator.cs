using TurnGame;

namespace TurnGame.Student
{
    public class ExperienceCalculator : IExperienceCalculator
    {
        public int RequiredExperience(int level)
        {
            // TODO 12: 100 + (level - 1) * 50을 반환하세요.
            return 100 + (level - 1) * 50;
        }

        public ExperienceResult AddExperience(int level, int currentExperience, int gainedExperience)
        {
            // TODO 13: 획득 경험치를 더하세요.
            currentExperience += gainedExperience;
            int gainedLevel = 0;

            // TODO 14: 필요 경험치 이상인 동안 반복하세요.
            while (currentExperience >= RequiredExperience(level))
            {
                // TODO 15: 필요 경험치를 빼고 레벨을 올리세요.
                currentExperience -= RequiredExperience(level);
                level++;

                // TODO 16: 여러 레벨 상승 횟수도 기록해서 반환하세요.
                gainedLevel++;
            }

            return new ExperienceResult { Level = level, RemainingExperience = currentExperience, LevelsGained = gainedLevel };
        }
    }
}