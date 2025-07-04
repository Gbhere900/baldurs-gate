#define USE_NEW_INPUT_SYSTERM
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    private  PlayerInputController PlayerInputController;
    private void Awake()
    {
        instance = this;
        PlayerInputController = new PlayerInputController();
        PlayerInputController.Enable();
    }

    public static InputManager Instance()
    {
        return instance;
    }

    public Vector2 GetMousePosition()
    {
#if USE_NEW_INPUT_SYSTERM
        Debug.Log(PlayerInputController.Player.MousePosition.ReadValue<Vector2>());
    return PlayerInputController.Player.MousePosition.ReadValue<Vector2>();
#else
    return Input.mousePosition;
#endif
    }

    public bool GetMouseButtonDown(int index)
    {
#if USE_NEW_INPUT_SYSTERM
        return PlayerInputController.Player.MouseButtonDown.WasPressedThisFrame();
#else
    return Input.GetMouseButtonDown(index);
#endif

    }

    public Vector2 GetWASDVector()
    {

#if USE_NEW_INPUT_SYSTERM
        return PlayerInputController.Player.CameraMovement.ReadValue<Vector2>();
#else
    Vector2 inputMoveDirection = new Vector2(0,0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = 1;
        }
        return inputMoveDirection;
#endif

    }

    public float GetQEDelta()
    {
#if USE_NEW_INPUT_SYSTERM
        return PlayerInputController.Player.CameraRotation.ReadValue<float>();
#else
    Vector3 inputRotateDirection = new Vector3(0,0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            return 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            return -1;
        }
        
#endif

    }

    public float GetMouseWheelDelta()
    {
#if USE_NEW_INPUT_SYSTERM
        return Math.Clamp(PlayerInputController.Player.CameraZoom.ReadValue<float>(),-1,1);
#else
    return Input.mouseScrollDelta.y;
#endif

    }


}
