using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;

    public bool Fire;
    public bool RightFire;
    public bool EAction;
    public bool QAction;
    public bool Jump;
    public Vector2 Move;
    public bool Pause;
    public Vector2 Look;
    public bool Dash;

    InputAction _fireAction;
    InputAction _rightFireAction;
    InputAction _eAction;
    InputAction _qAction;
    InputAction _moveAction;
    InputAction _jumpAction;
    InputAction _pauseAction;
    InputAction _lookAction;
    InputAction _dashAction;

    void Awake()
    {
        _fireAction = _playerInput.actions["Fire"];
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _pauseAction = _playerInput.actions["Pause"];
        _lookAction = _playerInput.actions["Look"];
        _rightFireAction = _playerInput.actions["RightFire"];
        _eAction = _playerInput.actions["EAction"];
        _qAction = _playerInput.actions["QAction"];
        _dashAction = _playerInput.actions["Dash"];
    }

    void Update()
    {
        Fire = _fireAction.WasPressedThisFrame();
        RightFire = _rightFireAction.WasPressedThisFrame();
        EAction = _eAction.WasPressedThisFrame();
        QAction = _qAction.WasPressedThisFrame();
        Move = _moveAction.ReadValue<Vector2>();
        Jump = _jumpAction.WasPressedThisFrame();
        Pause = _pauseAction.WasPressedThisFrame();
        Look = _lookAction.ReadValue<Vector2>();
        Dash = _dashAction.WasPressedThisFrame();
    }
}
