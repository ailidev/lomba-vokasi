// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerBehaviour : MonoBehaviour
// {
// 	private Vector3 playerDirection;

// 	[SerializeField]
// 	public float movementSpeed = 5f;

// 	// context stores all the information about what triggered this action
// 	public void Direction(InputAction.CallbackContext context) {
// 		// ReadValue enables us to fetch the Vector2 value stored in context
// 		Vector2 inputVector = context.ReadValue<Vector2>();
// 		playerDirection = new Vector3(inputVector.x, 0, inputVector.y);
// 	}

// 	private void Movement() {
// 		transform.Translate(playerDirection * movementSpeed * Time.deltaTime);
// 	}

// 	private void Awake() {

// 	}

// 	private void FixedUpdate() {
// 		Movement();
// 	}
// }
