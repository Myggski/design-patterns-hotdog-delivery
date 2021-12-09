using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/AchievementInt", order = 1)]
public class AchievementInt : Achievement<int> {
    public override bool AchievementDone {
        get {
            switch (achievementGoalType) {
                case AchievementGoalType.EqualTo:
                    return CurrentValue == goalValue;
                case AchievementGoalType.GreaterThan:
                    return CurrentValue > goalValue;
                case AchievementGoalType.LesserThan:
                    return CurrentValue < goalValue;
                default:
                    return false;
            }
        }
    }

    /// <summary>
    /// Adds value to the achievement, even if the achievement is already completed to keep track of the total value
    /// </summary>
    /// <param name="valueToAdd"></param>
    public override void AddValue(int valueToAdd) {
        CurrentValue += valueToAdd;

        if (AchievementDone && !AchievementCompleted) {
            AchievementCompleted = true;
            InvokeAchievementCompleted(this);
        }
    }

    /// <summary>
    /// This is future stuff. The achievements doesn't get saved in the PlayerPrefs right now. 
    /// </summary>
    /// <returns></returns>
    protected override int TryGetSavedValue() {
        return PlayerPrefs.GetInt(id, 0);
    }
}