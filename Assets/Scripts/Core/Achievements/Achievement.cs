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
    [HideInInspector]
    public static event Action<Achievement<T>> OnAchievementCompleted;
    
    public string Header => header;
    public string Description => description;
    public abstract bool AchievementDone { get; }
    public abstract void AddValue(T valueToAdd);
    protected abstract T TryGetSavedValue();

    private void ResetValue() {
        CurrentValue = TryGetSavedValue();
        AchievementCompleted = AchievementDone;
    }

    protected void InvokeAchievementCompleted(Achievement<T> achievement) {
        OnAchievementCompleted?.Invoke(achievement);
    }

    protected virtual void OnEnable() {
        ResetValue();
    }
}