using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunningCharacter : MonoBehaviour
{
    public float running = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move the character to the right based on the walkSpeed.
        rb.velocity = new Vector2(running, rb.velocity.y);
    }
}
