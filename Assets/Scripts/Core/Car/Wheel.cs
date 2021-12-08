using UnityEngine;

public class Wheel : MonoBehaviour {
    [SerializeField]
    private bool powered;
    [SerializeField]
    private float maxAngle;
    [SerializeField]
    [Tooltip("Time in seconds")]
    private float timeToReachMaxAngle = 2f;

    private float _turnAngle;
    private WheelCollider _wheelCollider;
    private Transform _wheelMesh;
    private bool _isBreaking;
    private float _currentSteerTime = 0;

    private void Setup() {
        _wheelCollider = GetComponentInChildren<WheelCollider>();
        _wheelMesh = GetComponentInChildren<MeshRenderer>()?.transform;
    }

    public void Steer(float steerInput) {
        if (steerInput == 0) {
            _wheelCollider.steerAngle = steerInput;
            _currentSteerTime = 0;
        } else {
            _currentSteerTime += Time.fixedDeltaTime;
            _turnAngle = Mathf.LerpAngle(_wheelCollider.steerAngle, maxAngle, _currentSteerTime / timeToReachMaxAngle);
            _wheelCollider.steerAngle = _turnAngle * steerInput;    
        }
        
    }

    public void Accelerate(float powerInput) {
        if (!powered) {
            return;
        }
        
        _wheelCollider.motorTorque = !_isBreaking 
            ? powerInput 
            : 0;
    }

    public void Break(float brakeTorque) {
        if (!powered) {
            _wheelCollider.brakeTorque = brakeTorque;
        }
        
        _isBreaking = brakeTorque > 0;   
    }

    public void UpdateWheelTransform() {
        Vector3 currentPosition = transform.position; 
        Quaternion currentRotation = transform.rotation;
        
        _wheelCollider.GetWorldPose(out currentPosition, out currentRotation);
        _wheelMesh.transform.position = currentPosition;
        _wheelMesh.transform.rotation = currentRotation;
    }
    
    private void Start() {
        Setup();
    }
}