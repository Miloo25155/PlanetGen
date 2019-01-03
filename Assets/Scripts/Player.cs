using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpHeight = 20f;
    public float gravity = 50f;

    Transform transform;
    Rigidbody rigidbody;

    public void Start()
    {
        transform = GetComponent<Transform>();

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    public void move(Vector3 translationVector)
    {
        this.transform.Translate(translationVector);
    }
}
