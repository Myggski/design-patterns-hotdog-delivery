using UnityEngine;

public interface IPooledObject {
    public void Initialize(Vector3 position, Quaternion rotation);
    public void SoftRemove();
}