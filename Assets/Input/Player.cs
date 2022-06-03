using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private PlayerInputActions _inputActionsScript;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private GameObject _vehicleGO;

    // Start is called before the first frame update
    void Start()
    {
        _inputActionsScript = new PlayerInputActions();
        
        _inputActionsScript.Player.Enable();
        _inputActionsScript.RandomColor.Enable();

        _inputActionsScript.Player.Movement.performed += MovementOnperformed;
        _inputActionsScript.RandomColor.AssignRandomColor.performed += AssignRandomColorOnperformed;
        _inputActionsScript.Player.Rotation.performed += RotationOnperformed;
        _inputActionsScript.Player.VehicleEntered.performed += VehicleEnteredOnperformed;
    }

    private void VehicleEnteredOnperformed(InputAction.CallbackContext context)
    {
        _inputActionsScript.Player.Disable();
        _inputActionsScript.Driving.Enable();
    }

    private void Update()
    {
        if (_inputActionsScript.Player.enabled)
        {
            Vector2 moveDirection = _inputActionsScript.Player.Movement.ReadValue<Vector2>();
            float moveX = moveDirection.x;
            float moveY = moveDirection.y;

            Vector3 rotationDirection = new Vector3(0f, _inputActionsScript.Player.Rotation.ReadValue<float>(), 0f);
            
            MovePlayer(new Vector3(moveX, 0f, moveY));
            RotatePlayer(rotationDirection);
        }

        if (_inputActionsScript.Driving.enabled)
        {
            Vector2 moveVehicleDirection = _inputActionsScript.Driving.Movement.ReadValue<Vector2>();
            float moveVehicleX = moveVehicleDirection.x;
            float moveVehicleY = moveVehicleDirection.y;
            
            MoveVehicle(new Vector3(moveVehicleX, 0f, moveVehicleY));
        }
    }
    
    private void RotationOnperformed(InputAction.CallbackContext context)
    {
        Debug.Log("Context: " + context);
    }

    private void AssignRandomColorOnperformed(InputAction.CallbackContext context)
    {
        _playerMaterial.color = Random.ColorHSV();
    }

    private void MovementOnperformed(InputAction.CallbackContext context)
    {
        Debug.Log($"X: {context.ReadValue<Vector2>().x} Y: {context.ReadValue<Vector2>().y}");

        // Vector2 moveDirection = context.ReadValue<Vector2>();
        // float moveX = moveDirection.x;
        // float moveY = moveDirection.y;
        //
        // MovePlayer(new Vector3(moveX, 0f, moveY));
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        transform.Translate(moveDirection * Time.deltaTime * 5);
    }

    private void MoveVehicle(Vector3 moveDirection)
    {
        if (_vehicleGO != null)
        {
            _vehicleGO.transform.Translate(moveDirection * Time.deltaTime * 15);
        }
        else
        {
            Debug.Log("_vehicleGo is null!");
        }
    }

    private void RotatePlayer(Vector3 rotationDirection)
    {
        transform.Rotate(rotationDirection * Time.deltaTime * 50);
    }
}
