using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.Z))
        {
            direction = Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = -Vector3.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direction = -Vector3.right;
        }

        player.move(direction);
    }
}
