using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour
{
    public float gravity = -10;
    public float rotationSpeedFactor = 50;

    public void Attract(FauxGravityBody body)
    {
        Vector3 gravityUp = (body.transform.position - transform.position).normalized;
        Vector3 bodyUp = body.transform.up;

        body.rigidbody.AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.transform.rotation;
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, targetRotation, rotationSpeedFactor * Time.deltaTime);
    }
}
