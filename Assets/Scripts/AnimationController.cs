using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    private Vector2 _move;
    public CinemachineStateDrivenCamera stateDrivenCamera;
    public CinemachineFreeLook thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _move = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateDrivenCamera.IsLiveChild(thirdPersonCamera))
        {
            animator.SetBool("AlternateCamera", false);
            animator.SetFloat("VelocityX", _move.x);
            animator.SetFloat("VelocityZ", _move.y);
        }
        else
        {
            animator.SetBool("AlternateCamera", true);
            animator.SetFloat("VelocityZ", _move.magnitude);
        }
        
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector2>();
    }
}
