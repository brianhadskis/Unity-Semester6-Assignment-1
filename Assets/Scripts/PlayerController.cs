using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector2 moveVal;
    private Vector3 direction;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;
    public CinemachineStateDrivenCamera stateDrivenCamera;
    public CinemachineFreeLook thirdPersonCamera;
    public float speed = 6.0f;
    public float gravity = -9.81f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        playerInput = player.GetComponent<PlayerInput>();

        DisableInput();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(moveVal.x, 0f, moveVal.y);

        if (moveVal.magnitude >= 0.1f)
        {
            if(stateDrivenCamera.IsLiveChild(thirdPersonCamera))
            {
                float targetAngle = mainCamera.transform.eulerAngles.y;
                //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * direction;
                //Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
            else
            {
                

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                direction = mainCamera.transform.forward * direction.z + mainCamera.transform.right * direction.x;
                direction.y = 0f;

                controller.Move(direction * speed * Time.deltaTime);


            }
        }
        if (!controller.isGrounded)
        {
            controller.Move(new Vector3(0f, gravity, 0f));
        }
        
    }

    public void EnableInput()
    {
        playerInput.ActivateInput();
    }

    public void DisableInput()
    {
        playerInput.DeactivateInput();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveVal = value.ReadValue<Vector2>();

        /*if (stateDrivenCamera.IsLiveChild(thirdPersonCamera))
        {
            direction = new Vector3(moveVal.x, 0f, moveVal.y);
        }
        else
        {
            direction += moveVal.x * GetCameraRight(mainCamera);
            direction += moveVal.y * GetCameraForward(mainCamera);
        }*/
        
        //player.transform.Translate(new Vector3(moveVal.x, 0f, moveVal.y));

    }

    public Vector3 GetCameraForward(GameObject playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public Vector3 GetCameraRight(GameObject playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }
}
