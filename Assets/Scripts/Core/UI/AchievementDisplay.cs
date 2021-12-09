using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class AchievementDisplay : MonoBehaviour {
    private VisualElement _wrapper;

    /// <summary>
    /// Adds an achievement badge to the UI 
    /// </summary>
    /// <param name="achievement">The achievement that has been completed</param>
    /// <typeparam name="T">Type of achievement-check</typeparam>
    private void DisplayAchievement<T>(Achievement<T> achievement) {
        VisualElement badge = AchievementBadgeBuilder
            .Init()
            .SetHeader(achievement.Header)
            .SetDescription(achievement.Description)
            .Build();

        _wrapper.Add(badge);

        StartCoroutine(RemoveAfterTime(badge));
    }
    
    /// <summary>
    /// Setting up wrapper and adds a subscriber to the OnAchievementCompleted event
    /// </summary>
    private void Setup() {
        _wrapper = GetComponent<UIDocument>()?.rootVisualElement.Q<VisualElement>("AchievementWrapper");
        Achievement<int>.OnAchievementCompleted += DisplayAchievement;
    }

    /// <summary>
    /// Clear the event when the gameObject is being destroyed
    /// </summary>
    private void Clear() {
        Achievement<int>.OnAchievementCompleted -= DisplayAchievement;
    }

    /// <summary>
    /// Removes the badge after a certain amount of time
    /// </summary>
    /// <param name="badge"></param>
    /// <returns></returns>
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
