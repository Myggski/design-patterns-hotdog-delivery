using System;
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
    
    public void Eat(FoodBase foodBase) {
        if (_currentNumberOfFood < maxNumberOfFood) {
            _currentNumberOfFood++;
            foodBase.SoftRemove();
            foodDeliveredAchievement.AddValue(1);
        }
    }

    private void Awake() {
        Setup();
    }
}
