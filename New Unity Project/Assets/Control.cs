using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State {idle, runnning, jump, falling}
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jspeed = 40;
    

    public int dimas = 0;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    

    private void Update()
    {
        Movement();
        VelocityState();
        anim.SetInteger("state", (int)state);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            dimas += 1;
        }
    }
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jspeed);
            state = State.jump;
        }
        
    }
    private void VelocityState()
    {
        if (state == State.jump)
        {
            if(rb.velocity.y < 0.1)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
                   
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.1)
        {
            state = State.runnning;
        } 


        else
        {
            state = State.idle;
        }
    }
}
