using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    //public bool jump;
    public bool sprint = false;
	public Image mobileSprintOnImage;

    [Header("Movement Settings")]
    public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
#endif

	public void OnMove(InputAction.CallbackContext value)
	{
		MoveInput(value.ReadValue<Vector2>());
		//Debug.Log("OnMove");
	}

	public void OnLook(InputAction.CallbackContext value)
	{
		if (cursorInputForLook)
		{
			LookInput(value.ReadValue<Vector2>());
		}

		LookInput(value.ReadValue<Vector2>());
	}

	/*public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}*/

	public void OnSprint(InputAction.CallbackContext value)
	{
#if UNITY_IOS || UNITY_ANDROID
		sprint = !sprint;
		SprintInput(sprint);
#else
		SprintInput(value.ReadValueAsButton());
		mobileSprintOnImage.gameObject.SetActive(sprint);
#endif
	}

	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	}

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	/*public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}*/

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}

#if !UNITY_IOS || !UNITY_ANDROID

	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	public void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}

#endif
}
