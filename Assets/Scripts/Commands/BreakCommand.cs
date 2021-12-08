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

    protected override void Setup() {
        _wheels = GetComponentsInChildren<Wheel>();
        _breakInput = GetComponent<IBreakInput>();
    }

    public override void Execute() {
        _breakCoroutine ??= StartCoroutine(Break());
    }

    private IEnumerator Break() {
        while (_breakInput.IsPressingBreak) {
            Array.ForEach(_wheels, wheel => wheel.Break(brakeTorque));
            yield return new WaitForFixedUpdate();
        }
        
        Array.ForEach(_wheels, wheel => wheel.Break(0));

        _breakCoroutine = null;
    }
}
