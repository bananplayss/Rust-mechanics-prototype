using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
	public float smooth;
	public float swayMultiplier;

    public Transform orientation;
	public Transform handPivot;

    float xRotation;
    float yRotation;

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	private void Update() {
		float mouseX = Input.GetAxisRaw("Mouse X") *Time.deltaTime*sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") *Time.deltaTime*sensY;

		yRotation += mouseX;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);

		Quaternion rotationX = Quaternion.AngleAxis(-mouseX* swayMultiplier, Vector3.right);
		Quaternion rotationY = Quaternion.AngleAxis(-mouseX* swayMultiplier, Vector3.up);

		Quaternion targetRotation = rotationX * rotationY;

		if(handPivot != null) {
			handPivot.localRotation = Quaternion.Slerp(handPivot.localRotation, targetRotation, smooth * Time.deltaTime);
		}
			
	}
}
