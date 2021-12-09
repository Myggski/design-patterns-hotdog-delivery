using UnityEngine;

public abstract class FoodBase : MonoBehaviour, IPooledObject {
    public abstract void Eject(Vector3 startVelocity);

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
    public virtual void Initialize(Vector3 position, Quaternion rotation) {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
    }

    /// <summary>
    /// Sets the food as active instead of destroying it, for the object pooling
    /// </summary>
    public virtual void SoftRemove() {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        CheckCollision(other);
    }
}