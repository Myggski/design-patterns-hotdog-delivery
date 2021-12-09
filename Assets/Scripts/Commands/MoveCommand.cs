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

    /// <summary>
    /// Setting up the wheels and the move input components
    /// </summary>
    protected override void Setup() {
        _moveInput = GetComponent<IMoveInput>();
        _wheels = GetComponentsInChildren<Wheel>();
    }

    /// <summary>
    /// Starting to move if there's no move coroutine already that has been started
    /// </summary>
    public override void Execute() {
        _moveCoroutine ??= StartCoroutine(Move());
    }

    /// <summary>
    /// Adding torque to the wheels so they'll rotate and move the car
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Setting the mesh of the wheels correctly, syncs the mesh with the collider
    /// </summary>
    private void UpdateMeshTransformation() {
        Array.ForEach(_wheels, wheel => wheel.UpdateWheelTransform());
    }

    private void Update() {
        UpdateMeshTransformation();
    }
}