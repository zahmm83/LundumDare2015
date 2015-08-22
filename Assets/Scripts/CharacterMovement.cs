using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float moveForce = 50;
    public float speed = 5;
    public float sensitivity = 5;
    public float rotationClamp = 90;
    public float jumpHeight = 20;
    public float maxVelocityChange = 5;
    public Transform playerCamera;
    private Rigidbody playerRigidbody;
    private bool grounded = false;
    private float xPos;
    private float yPos;
    private Vector3 targetVelocity;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        yPos = Mathf.Clamp(yPos - Input.GetAxis("Mouse Y") * sensitivity, -rotationClamp, rotationClamp);
        xPos = xPos + Input.GetAxis("Mouse X") * sensitivity;

        gameObject.transform.localRotation = Quaternion.AngleAxis(xPos, Vector3.up);
        playerCamera.localRotation = Quaternion.AngleAxis(yPos, Vector3.right);

        if (Physics.Raycast(transform.position, -transform.up, 1.125f))
            grounded = true;
        else
            grounded = false;

        if (Input.GetButtonDown("Jump") && grounded)
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpHeight, playerRigidbody.velocity.z);
    }

    void FixedUpdate()
    {
        targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        Vector3 velocity = playerRigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        playerRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}