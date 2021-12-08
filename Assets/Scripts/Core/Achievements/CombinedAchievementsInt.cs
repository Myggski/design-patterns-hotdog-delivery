using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Achivements/CombinedAchievementsInt", order = 2)]
public class CombinedAchievementsInt : AchievementInt {
    [SerializeField]
    private List<AchievementInt> achivementsToCompleteFirst;

    private bool HasDoneAllChildAchivements =>
        achivementsToCompleteFirst
            .All(achievement => achievement.AchievementDone); 
    public override bool AchievementDone => HasDoneAllChildAchivements 
                                            && base.AchievementDone;

    public override void AddValue(int valueToAdd) {
        if (AchievementDone) {
            return;
        }

        if (HasDoneAllChildAchivements) {
            base.AddValue(valueToAdd);
        } else {
            foreach(AchievementInt achievement in achivementsToCompleteFirst)
            {
                if (!achievement.AchievementDone) {
                    achievement.AddValue(valueToAdd);
                    break;
                }
            }
        }
    }
}