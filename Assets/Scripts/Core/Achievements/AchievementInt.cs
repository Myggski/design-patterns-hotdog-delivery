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

    public override void AddValue(int valueToAdd) {
        CurrentValue += valueToAdd;

        if (AchievementDone && !AchievementCompleted) {
            AchievementCompleted = true;
            InvokeAchievementCompleted(this);
        }
    }

    protected override int TryGetSavedValue() {
        return PlayerPrefs.GetInt(id, 0);
    }
}