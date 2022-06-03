using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BouncyBall : MonoBehaviour
{
    private PlayerInputActions _inputPlayerActions;

    // Start is called before the first frame update
    void Start()
    {
        _inputPlayerActions = new PlayerInputActions();
        _inputPlayerActions.BouncyBall.Enable();
        _inputPlayerActions.BouncyBall.Jump.performed += JumpOnperformed;
        _inputPlayerActions.BouncyBall.Jump.canceled += JumpOncanceled;
    }

    private void JumpOncanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Duration: " + context.duration);
        var forceMultiplier = context.duration;
        
        if (context.duration < 1.0f)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * (10f * (float)forceMultiplier), ForceMode.Impulse);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 25f, ForceMode.Impulse);
        }
    }

    private void JumpOnperformed(InputAction.CallbackContext context)
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
