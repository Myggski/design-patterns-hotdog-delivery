using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IMoveInput))]
public class MoveCommand : CommandBase {
    [SerializeField]
    private float power = 15000f;
    
    private Wheel[] _wheels;
    private IMoveInput _moveInput;
    private Coroutine _moveCoroutine;

    protected override void Setup() {
        _moveInput = GetComponent<IMoveInput>();
        _wheels = GetComponentsInChildren<Wheel>();
    }

    public override void Execute() {
        _moveCoroutine ??= StartCoroutine(Move());
    }

    private IEnumerator Move() {
        while (_moveInput.MoveDirection != Vector3.zero) {
            foreach (Wheel wheel in _wheels) {
                wheel.Steer(_moveInput.MoveDirection.x);
                wheel.Accelerate(_moveInput.MoveDirection.z * power);
            }

            yield return new WaitForFixedUpdate();
        }
        
        // This resets the angle of the wheels
        Array.ForEach(_wheels, wheel => {
            wheel.Steer(0);
            wheel.Accelerate(0);
        });

        _moveCoroutine = null;
    }

    private void Update() {
        Array.ForEach(_wheels, wheel => wheel.UpdateWheelTransform());
    }
}