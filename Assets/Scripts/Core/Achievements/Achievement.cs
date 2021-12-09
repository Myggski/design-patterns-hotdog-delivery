using System;
using UnityEngine;

public abstract class Achievement<T> : ScriptableObject {
    [SerializeField]
    [ScriptableObjectId]
    protected string id;
    [SerializeField]
    protected string header;
    [SerializeField]
    [TextArea]
    protected string description;
    [SerializeField]
    protected AchievementGoalType achievementGoalType;
    [SerializeField]
    protected T goalValue;

    protected T CurrentValue;
    protected bool AchievementCompleted;

    public static event Action<Achievement<T>> OnAchievementCompleted;

    public string Header => header;
    public string Description => description;

    public abstract bool AchievementDone { get; }
    public abstract void AddValue(T valueToAdd);
    protected abstract T TryGetSavedValue();

    /// <summary>
    /// Invokes whenever an achievement has been completed from any of the sub-classes
    /// </summary>
    /// <param name="achievement">The achievement that has been completed</param>
    protected void InvokeAchievementCompleted(Achievement<T> achievement) {
        OnAchievementCompleted?.Invoke(achievement);
    }

    /// <summary>
    /// Resets the achievements onEnable, future plan is to save the achievements by ID in PlayerPrefs
    /// </summary>
    private void ResetValue() {
        CurrentValue = TryGetSavedValue();
        AchievementCompleted = AchievementDone;
    }

    protected virtual void OnEnable() {
        ResetValue();
    }
}