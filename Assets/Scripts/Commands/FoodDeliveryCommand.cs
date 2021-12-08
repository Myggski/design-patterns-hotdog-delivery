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

    protected override void Setup() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Execute() {
        DeliverFood();
    }

    private void DeliverFood() {
        IPooledObject food = ObjectPooler.Instance.SpawnFromPool(foodPoolObjectTag, SpawnPosition.position, SpawnPosition.rotation);
        (food as FoodBase)?.Eject(_rigidbody.velocity);
    }
}
