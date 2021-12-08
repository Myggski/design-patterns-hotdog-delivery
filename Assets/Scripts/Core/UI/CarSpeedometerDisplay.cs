using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class CarSpeedometerDisplay : MonoBehaviour {
    [SerializeField]
    private CarSpeedometer carSpeedometer;
    
    private Label _labelText;

    private void Update() {
        if (!ReferenceEquals(_labelText, null)) {
            _labelText.text = $"{Mathf.Floor(carSpeedometer.Speed)} {carSpeedometer.SpeedTypeText}";    
        }
    }

    private void Setup() {
        _labelText = GetComponent<UIDocument>()?.rootVisualElement.Q<Label>("speedometerText");
    }

    private void Awake() {
        Setup();
    }
}
