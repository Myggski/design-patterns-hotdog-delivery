using UnityEngine;

public abstract class FoodBase : MonoBehaviour, IPooledObject {
    public abstract void Eject(Vector3 startVelocity);

    private void CheckCollision(Collision other) {
        IEater eater = other.gameObject.GetComponent<IEater>();

        if (!ReferenceEquals(eater, null)) {
            eater.Eat(this);
        }
    }
    
    public virtual void Initialize(Vector3 position, Quaternion rotation) {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
    }

    public virtual void SoftRemove() {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        CheckCollision(other);
    }
}