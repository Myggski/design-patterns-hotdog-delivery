using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hotdog : FoodBase {
    /// <summary>
    /// Shoots the food when it's spawned by the "food canon"
    /// </summary>
    /// <param name="startVelocity">velocity of the vehicle that's shooting the food</param>
    public override void Eject(Vector3 startVelocity) {
        _rigidbody.AddForce(startVelocity + transform.forward * 25, ForceMode.VelocityChange);
    }
}