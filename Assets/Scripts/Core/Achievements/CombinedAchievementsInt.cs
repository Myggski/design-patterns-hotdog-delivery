using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Achivements/CombinedAchievementsInt", order = 2)]
public class CombinedAchievementsInt : AchievementInt {
    [SerializeField]
    private List<AchievementInt> achivementsToCompleteFirst;

    private bool HasDoneAllChildAchivements =>
        !achivementsToCompleteFirst.Any()
        || achivementsToCompleteFirst
            .All(achievement => achievement.AchievementDone);

    public override bool AchievementDone =>
        HasDoneAllChildAchivements
        && base.AchievementDone;

    /// <summary>
    /// Adds the value to the CombinedAchievement if all children is completed, else it will add the new value to the child
    /// </summary>
    /// <param name="valueToAdd">The value to add to the achievement</param>
    public override void AddValue(int valueToAdd) {
        if (AchievementDone) {
            return;
        }

        if (HasDoneAllChildAchivements) {
            base.AddValue(valueToAdd);
        } else {
            foreach (AchievementInt achievement in achivementsToCompleteFirst) {
                if (!achievement.AchievementDone) {
                    achievement.AddValue(valueToAdd);
                    break;
                }
            }
        }
    }

    private void Awake() {
        achivementsToCompleteFirst = new List<AchievementInt>();
    }
}