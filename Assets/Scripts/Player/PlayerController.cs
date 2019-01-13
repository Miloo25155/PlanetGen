using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    public FauxGravityAttractor attractor;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    public float smoothMoveTime = 0.2f;

    float smoothRotationVelocity;
    public float smoothRotationTime = 0.2f;

    Rigidbody rigidbody;
    Transform cameraT;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;

        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 inputDir = input.normalized;


        //FAUX GRAVITY
        Vector3 gravityUp = attractor.GetGravityUp(rigidbody);
        Vector3 bodyUp = transform.up;
        rigidbody.AddForce(gravityUp * attractor.gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * transform.rotation;
        //END FAUX GRAVITY

        Vector3 targetUpRotationVector = new Vector3(0, targetRotation.eulerAngles.y, 0);

        if (inputDir != Vector3.zero)
        {
            float targetLocalUpRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            targetUpRotationVector = targetUpRotationVector * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetLocalUpRotation, ref smoothRotationVelocity, smoothRotationTime);
        }

        Quaternion targetUpRotation = Quaternion.LookRotation(targetUpRotationVector, targetRotation.eulerAngles);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetUpRotation, attractor.rotationSpeedFactor * Time.deltaTime);


        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = (running ? runSpeed : walkSpeed) * inputDir.magnitude;
        Vector3 targetMoveAmount = inputDir * targetSpeed;

        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, smoothMoveTime);
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
