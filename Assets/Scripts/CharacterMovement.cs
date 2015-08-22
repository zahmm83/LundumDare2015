using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float moveForce = 50;
    public float maxSpeed = 5;
    public LayerMask worldLayer;
    public float sensitivity = 5;
    public float rotationClamp = 90;
    public float jumpHeight = 20;
    private Camera playerCamera;
    private Rigidbody playerRigidbody;
    private float rotationY = 0;
    private float rotationX = 0;
    private bool grounded = false;

	void Start ()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerRigidbody = GetComponent<Rigidbody>();
	}
	
    void Update()
    {
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -rotationClamp, rotationClamp);

        playerCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, rotationX, 0);

        if (Physics.Raycast(transform.position, -transform.up, 1.5f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown("space") && grounded)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpHeight, playerRigidbody.velocity.z);
        }
    }

	void FixedUpdate ()
    {
	    if (Input.GetKey("w"))
        {
            playerRigidbody.AddForce(moveForce * transform.forward);
        }
        else if (Input.GetKey("s"))
        {
            playerRigidbody.AddForce(moveForce * -transform.forward);
        }

        if (Input.GetKey("d"))
        {
            playerRigidbody.AddForce(moveForce * transform.right);
        } 
        else if (Input.GetKey("a"))
        {
            playerRigidbody.AddForce(moveForce * -transform.right);
        }

        playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, maxSpeed);
    }
}
