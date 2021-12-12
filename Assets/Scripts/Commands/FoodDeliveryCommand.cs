using ObjectPoolers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FoodDeliveryCommand : CommandBase {
    [SerializeField]
    private PoolObjectTag foodPoolObjectTag;
    [SerializeField]
    private Transform barrel;

    private Rigidbody _rigidbody;
    
    private Transform SpawnPosition => ReferenceEquals(barrel, null) 
        ? transform 
        : barrel;

    /// <summary>
    /// Setting up the rigidbody component. Setup is being called in CommandBase
    /// </summary>
    protected override void Setup() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Delivers food
    /// </summary>
    public override void Execute() {
        DeliverFood();
    }

    /// <summary>
    /// Spawns food from the object pool and ejects it
    /// </summary>
    private void DeliverFood() {
        IPooledHotdog food = HotdogObjectPooler.Instance.Get();
        food.Initialize(SpawnPosition.position, SpawnPosition.rotation);
        (food as FoodBase)?.Eject(_rigidbody.velocity);
    }
}
