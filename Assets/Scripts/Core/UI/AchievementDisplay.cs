using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class AchievementDisplay : MonoBehaviour {
    private VisualElement _wrapper;

    private void DisplayAchievement<T>(Achievement<T> achievement) {
        VisualElement badge = AchievementBadgeBuilder
            .Init()
            .SetHeader(achievement.Header)
            .SetDescription(achievement.Description)
            .Build();

        _wrapper.Add(badge);

        StartCoroutine(RemoveAfterTime(badge));
    }
    
    private void Setup() {
        _wrapper = GetComponent<UIDocument>()?.rootVisualElement.Q<VisualElement>("AchievementWrapper");
        Achievement<int>.OnAchievementCompleted += DisplayAchievement;
    }

    private void Clear() {
        Achievement<int>.OnAchievementCompleted -= DisplayAchievement;
    }

    private IEnumerator RemoveAfterTime(VisualElement badge) {
        yield return new WaitForSeconds(5);
         
        _wrapper.Remove(badge);
    }

    private void Awake() {
        Setup();
    }

    private void OnDestroy() {
        Clear();
    }
}
