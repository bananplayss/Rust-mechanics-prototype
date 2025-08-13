
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[Header("Movement")]
	public float moveSpeed;

	public Transform orientation;
	public AnimationCurve crouchSpeed;

	float horizontalInput;
	float verticalInput;
	bool crouching;

	Vector3 moveDirection;
	Vector3 desiredCrouchScale;
	Vector3 originalScale;

	Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		desiredCrouchScale = transform.localScale * .5f;
		originalScale = transform.localScale;
	}

	private void Update() {
		MyInput();
	}

	private void FixedUpdate() {
		MovePlayer();
		Crouching();
	}

	private void MyInput() {
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");
		if (Input.GetKeyDown(KeyCode.C)) {
			crouching = !crouching; 
			rb.AddForce(Vector3.down * 3, ForceMode.Impulse);
		}
	}

	private void MovePlayer() {
		// calculate movement direction
		moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
		rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
	}

	private void Crouching() {
		if (crouching) {
			
			transform.localScale = Vector3.MoveTowards(transform.localScale, desiredCrouchScale,crouchSpeed.Evaluate(Time.deltaTime));
		} else {
			transform.localScale = Vector3.MoveTowards(transform.localScale, originalScale, crouchSpeed.Evaluate(Time.deltaTime));
		}

	}

}