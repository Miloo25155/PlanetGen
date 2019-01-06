using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 3.5f;
    public float mouseSensitivityY = 3.5f;

    public float walkSpeed;

    public Vector2 pitchMinMax = new Vector2(-60, 60);

    Transform cameraT;
    float verticalLookRotation;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    public float smoothMoveTime = 0.2f;

    Rigidbody rigidbody;

    void Start()
    {
        cameraT = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);

        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, pitchMinMax.x, pitchMinMax.y);

        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, smoothMoveTime);
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
