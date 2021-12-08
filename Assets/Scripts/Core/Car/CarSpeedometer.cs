using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarSpeedometer : MonoBehaviour {
    [SerializeField]
    [Tooltip("Displays Kilometer Per Hour by default")]
    private bool displayMilesPerHour;

    private Rigidbody _rigidbody;
    
    public float Speed => displayMilesPerHour 
        ? _rigidbody.velocity.magnitude * 2.2369362912f 
        : _rigidbody.velocity.magnitude * 3.6f;
    public string SpeedTypeText => displayMilesPerHour 
        ? "MP/H" 
        : "KM/H";

    private void Setup() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Awake() {
        Setup();
    }
}
