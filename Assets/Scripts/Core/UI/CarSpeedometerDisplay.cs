using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class CarSpeedometerDisplay : MonoBehaviour {
    [SerializeField]
    private CarSpeedometer carSpeedometer;
    
    private Label _labelText;
    
    /// <summary>
    /// Setting up label text where the speed is displayed
    /// </summary>
    private void Setup() {
        _labelText = GetComponent<UIDocument>()?.rootVisualElement.Q<Label>("speedometerText");
    }

    /// <summary>
    /// Updates the speed text on Update because
    /// </summary>
    private void TryUpdateText() {
        if (!ReferenceEquals(_labelText, null)) {
            _labelText.text = $"{Mathf.Floor(carSpeedometer.Speed)} {carSpeedometer.SpeedTypeText}";    
        }
    }
    
    private void Update() {
        TryUpdateText();
    }

    private void Awake() {
        Setup();
    }
}
