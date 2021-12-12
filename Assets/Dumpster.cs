using System;
using ObjectPoolers;
using UnityEngine;

public class Dumpster : MonoBehaviour, IEater {
    [SerializeField]
    private int maxNumberOfFood;

    [SerializeField]
    private CombinedAchievementsInt foodDeliveredAchievement; 

    private int _currentNumberOfFood;

    private void Setup() {
        _currentNumberOfFood = 0;
    }
    
    public void Eat(IPooledHotdog hotdog) {
        if (_currentNumberOfFood < maxNumberOfFood) {
            HotdogObjectPooler.Instance.Release(hotdog);
            foodDeliveredAchievement.AddValue(1);
            _currentNumberOfFood++;
        }
    }

    private void Awake() {
        Setup();
    }
}
