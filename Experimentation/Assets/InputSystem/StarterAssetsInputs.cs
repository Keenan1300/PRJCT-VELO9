using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public float scroll;
		public bool jump;
		public bool sprint;
		PlayerInput PlayerInput;
		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}



		public void OnScroll(InputValue value)
		{
			ScrollInput(value.Get<float>());
		}

		//public void OnScrollUp(InputValue value)
		//{
		//	ScrollUpInput(value.Get<float>());
		//}
		//public void OnScrollDown(InputValue value)
		//{
		//	ScrollDownInput(value.Get<float>());
		//}
#endif
		public void ScrollInput(float ScrollDirection)
		{
			scroll = ScrollDirection;
			//Debug.Log(scroll);
		}
		//public void ScrollUpInput(float ScrollUp)
		//{
		//	scrollup = ScrollUp;
		//}

		//public void ScrollDownInput(float ScrollDown)
		//{
		//	scrolldown = ScrollDown;
		//}

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
            UnityEngine.Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		
	}
	
}