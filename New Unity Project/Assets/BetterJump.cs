using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] private float jFallmult = 40;
    [SerializeField] private float lowjumpmult = 40;

    Rigidbody2D rb;

    void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jFallmult - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowjumpmult - 1) * Time.deltaTime;
        }
    }
}
