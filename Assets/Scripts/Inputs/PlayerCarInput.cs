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

    /// <summary>
    /// Getting the CarInputAction to listen to different key action events
    /// </summary>
    private void Setup() {
        _carInputAction = new CarInputAction();
    }

    /// <summary>
    /// Enables the CarInputAction instance
    /// </summary>
    private void EnableInputAction() {
        _carInputAction.Enable();
    }
    
    /// <summary>
    /// Subscribes to the events that's included in the game object
    /// </summary>
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

    /// <summary>
    /// Unsubscribes to the events that's included in the game object
    /// </summary>
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

    /// <summary>
    /// Executes the FoodDeliveryCommand
    /// </summary>
    /// <param name="context">Input Action from Unitys Input System</param>
    private void OnFoodDelivery(InputAction.CallbackContext context) {
        if (HasFoodDeliveryCommand) {
            foodDeliveryCommand.Execute();
        }
    }

    /// <summary>
    /// Executes the MoveCommand
    /// </summary>
    /// <param name="context">Input Action from Unitys Input System</param>
    private void OnMove(InputAction.CallbackContext context) {
        Vector2 inputDirectionValue = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(inputDirectionValue.x, 0, inputDirectionValue.y);

        if (HasMoveCommand) {
            moveCommand.Execute();    
        }
    }

    /// <summary>
    /// Executes the BreakCommand
    /// </summary>
    /// <param name="context">Input Action from Unitys Input System</param>
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