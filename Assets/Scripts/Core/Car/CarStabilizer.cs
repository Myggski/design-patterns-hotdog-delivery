using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class CarStabilizer : MonoBehaviour {
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    /// <summary>
    /// Setting up the components
    /// </summary>
    private void Setup() {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// Sets the center mass of the rigidbody to make the car more stable while driving, else it will flip
    /// </summary>
    private void SetCenterMass() {
        _rigidbody.centerOfMass = new Vector3(0, -transform.TransformPoint(_boxCollider.bounds.min).y / 2, 0);
    }
    
    private void Awake() {
        Setup();
    }

    private void Start() {
        SetCenterMass();
    }

    private void OnDrawGizmos() {
        if (_rigidbody == null) {
            return;
        } 

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(_rigidbody.centerOfMass), 0.1f);
    }
}
