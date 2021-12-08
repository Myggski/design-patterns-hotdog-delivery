using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hotdog : FoodBase {
    private Rigidbody _rigidbody;

    private void Setup() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Eject(Vector3 startVelocity) {
        _rigidbody.AddForce(startVelocity + transform.forward * 25, ForceMode.VelocityChange);
    }

    private void Awake() {
        Setup();
    }
}