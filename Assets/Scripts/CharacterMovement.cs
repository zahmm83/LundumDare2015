﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacterMovement : NetworkBehaviour
{
    public float moveForce = 50;
    public float speed = 5;
    public float sensitivity = 5;
    public float rotationClamp = 90;
    public float jumpHeight = 20;
    public float maxVelocityChange = 5;
    public Transform playerCamera;
    public Animator anim;

    public float groundedDetectionLength = 0.25f;

    private Rigidbody playerRigidbody;
    private bool grounded = false;
    private float xPos;
    private float yPos;
    private Vector3 targetVelocity;


    [SyncVar]
    public float shootAngle;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if (anim.layerCount == 2)
            anim.SetLayerWeight(1, 1);
    }

    [Command]
    void CmdReportShootAngle(float pos)
    {
        shootAngle = pos;
    }

    void Update()
    {
        yPos = Mathf.Clamp(yPos - Input.GetAxis("Mouse Y") * sensitivity, -rotationClamp, rotationClamp);

        CmdReportShootAngle(yPos);

        xPos = xPos + Input.GetAxis("Mouse X") * sensitivity;

        gameObject.transform.localRotation = Quaternion.AngleAxis(xPos, Vector3.up);
        playerCamera.localRotation = Quaternion.AngleAxis(yPos, Vector3.right);

        var raycast_position = transform.position;
        raycast_position.y += 0.1f;

        bool objectIsDirectlyBelow = Physics.Raycast(raycast_position, -transform.up, groundedDetectionLength);
        bool objectIsFrontRight = Physics.Raycast(raycast_position, transform.forward + transform.right - transform.up, groundedDetectionLength * 1.2f);
        bool objectIsBackRight = Physics.Raycast(raycast_position, -transform.forward + transform.right - transform.up, groundedDetectionLength * 1.2f);
        bool objectIsBackLeft = Physics.Raycast(raycast_position, -transform.forward - transform.right - transform.up, groundedDetectionLength * 1.2f);
        bool objectIsFrontLeft = Physics.Raycast(raycast_position, transform.forward - transform.right - transform.up, groundedDetectionLength * 1.2f);
        
        grounded = objectIsDirectlyBelow
                || objectIsFrontRight
                || objectIsBackRight
                || objectIsBackLeft
                || objectIsFrontLeft;

        if (Input.GetButtonDown("Jump") && grounded)
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpHeight, playerRigidbody.velocity.z);


        anim.SetBool("falling", (0 > playerRigidbody.velocity.y) && !grounded);
        anim.SetBool("grounded", grounded);
        var linear_vector = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
        anim.SetFloat("speed", linear_vector.magnitude);
        anim.SetFloat("height", playerRigidbody.velocity.y);
        anim.speed = Mathf.Clamp((playerRigidbody.velocity.magnitude/2), 1, 3);
    }

    void FixedUpdate()
    {
        targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity = Vector3.Normalize(targetVelocity);
        targetVelocity *= speed;

        Vector3 velocity = playerRigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        
        if (grounded)
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
        }
        else
        {
            // Don't change velocity, just push the character in the forward direction.
            velocityChange = Vector3.zero;
            playerRigidbody.AddForce(transform.forward * 2.0f, ForceMode.Force);
        }


        playerRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}