using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveCommand))]
public class PlayerCarInput : MonoBehaviour, IMoveInput, IBreakInput, IFoodDeliveryInput {
    [SerializeField]
    private CommandBase moveCommand;
    [SerializeField]
    private CommandBase breakCommand;
    [SerializeField]
    private CommandBase foodDeliveryCommand;

    private Vector3 _moveDirection;
    private bool _isPressingBreak;
    private CarInputAction _carInputAction;

    private bool HasMoveCommand => !ReferenceEquals(moveCommand, null);
    private bool HasBreakCommand => !ReferenceEquals(breakCommand, null);
    private bool HasFoodDeliveryCommand => !ReferenceEquals(foodDeliveryCommand, null);
    
    public Vector3 MoveDirection => _moveDirection;
    public bool IsPressingBreak => _isPressingBreak;

    private void Setup() {
        _carInputAction = new CarInputAction();
    }

    private void EnableInputAction() {
        _carInputAction.Enable();
    }
    
    private void RegisterCallbacks() {
        if (HasMoveCommand) {
            _carInputAction.Car.Movement.performed += OnMove;    
        }

        if (HasBreakCommand) {
            _carInputAction.Car.Break.performed += OnBreak;
        }

        if (HasFoodDeliveryCommand) {
            _carInputAction.Car.SausageDelivery.performed += OnFoodDelivery;
        }
    }

    private void UnregisterCallbacks() {
        if (HasMoveCommand) {
            _carInputAction.Car.Movement.performed -= OnMove;
        }

        if (HasBreakCommand) {
            _carInputAction.Car.Break.performed -= OnBreak;
        }
        
        if (HasFoodDeliveryCommand) {
            _carInputAction.Car.SausageDelivery.performed -= OnFoodDelivery;
        }
    }

    private void OnFoodDelivery(InputAction.CallbackContext context) {
        if (HasFoodDeliveryCommand) {
            foodDeliveryCommand.Execute();
        }
    }

    private void OnMove(InputAction.CallbackContext context) {
        Vector2 inputDirectionValue = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(inputDirectionValue.x, 0, inputDirectionValue.y);

        if (HasMoveCommand) {
            moveCommand.Execute();    
        }
    }

    private void OnBreak(InputAction.CallbackContext context) {
        _isPressingBreak = !_isPressingBreak; ;

        if (HasBreakCommand) {
            breakCommand.Execute();
        }
    }

    private void Start() {
        EnableInputAction();
    }

    private void OnEnable() {
        RegisterCallbacks();
    }
    
    private void OnDisable() {
        UnregisterCallbacks();
    }

    private void Awake() {
        Setup();
    }
}