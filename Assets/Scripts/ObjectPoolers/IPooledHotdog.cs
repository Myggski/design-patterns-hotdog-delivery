using UnityEngine;

public interface IPooledHotdog {
    public void Initialize(Vector3 position, Quaternion rotation);
    public void SoftDelete();
}