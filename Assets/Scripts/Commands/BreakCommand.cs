using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IBreakInput))]
public class BreakCommand : CommandBase {
    [SerializeField]
    private float brakeTorque = 10;

    private Wheel[] _wheels;
    private IBreakInput _breakInput;
    private Coroutine _breakCoroutine;

    /// <summary>
    /// Getting the wheels and break input components
    /// </summary>
    protected override void Setup() {
        _wheels = GetComponentsInChildren<Wheel>();
        _breakInput = GetComponent<IBreakInput>();
    }

    /// <summary>
    /// Starting to break if there's no break coroutine already that has been started
    /// </summary>
    public override void Execute() {
        _breakCoroutine ??= StartCoroutine(Break());
    }

    /// <summary>
    /// Breaking until the user releases the break button
    /// </summary>
    /// <returns></returns>
    private IEnumerator Break() {
        while (_breakInput.IsPressingBreak) {
            Array.ForEach(_wheels, wheel => wheel.Break(brakeTorque));
            yield return new WaitForFixedUpdate();
        }
        
        Array.ForEach(_wheels, wheel => wheel.Break(0));

        _breakCoroutine = null;
    }
}
