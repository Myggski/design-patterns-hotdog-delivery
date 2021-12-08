using UnityEngine;

public abstract class CommandBase : MonoBehaviour {
    public abstract void Execute();
    protected virtual void Setup() {}

    protected virtual void Start() {
        Setup();
    }
}