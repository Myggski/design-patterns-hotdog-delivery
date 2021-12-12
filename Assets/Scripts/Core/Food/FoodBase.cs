
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class FoodBase : MonoBehaviour, IPooledHotdog {
    private protected Rigidbody _rigidbody;

    public abstract void Eject(Vector3 startVelocity);
    
    private void Setup() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Checks if the food has collided with something that can eat the food
    /// </summary>
    /// <param name="other">The object that the food is colliding with</param>
    private void CheckCollision(Collision other) {
        IEater eater = other.gameObject.GetComponent<IEater>();

        if (!ReferenceEquals(eater, null)) {
            eater.Eat(this);
        }
    }
    
    /// <summary>
    /// Sets the position and rotation of the food and sets it as active
    /// This is being used for the object pool
    /// </summary>
    /// <param name="position">The position that the food will be placed</param>
    /// <param name="rotation">The rotation that the food will have</param>
    public void Initialize(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
        
        gameObject.SetActive(true);
    }

    public void SoftDelete() {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        CheckCollision(other);
    }

    private void Awake() {
        Setup();
    }
}