using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle, runnning, jump, falling }
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jspeed = 40;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform respawn1;
    [SerializeField] private Transform respawn2;
    [SerializeField] private Transform respawn3;
    [SerializeField] private Transform respawn4;
    [SerializeField] private int Deaths = 0;
    [SerializeField] private Text DeathCount;

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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            rb.transform.position = respawn1.transform.position;
            Deaths += 1;
            DeathCount.text = Deaths.ToString();

        }

        if (other.gameObject.tag == "Goal1")
        {
            rb.transform.position = respawn2.transform.position;
            respawn1.transform.position = respawn2.transform.position;
            camera.transform.position = new Vector3(30f, 1.05f, -10);
        }

        if (other.gameObject.tag == "Goal2")
        {
            rb.transform.position = respawn3.transform.position;
            respawn1.transform.position = respawn3.transform.position;
            camera.transform.position = new Vector3(60f, 1.05f, -10);
        }

        if (other.gameObject.tag == "Goal3")
        {
            rb.transform.position = respawn4.transform.position;
            respawn1.transform.position = respawn4.transform.position;
            camera.transform.position = new Vector3(90f, 1.05f, -10);
            DeathCount.fontSize = 140;
        }

        if (other.gameObject.tag == "Goal4")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);                                   
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
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, jspeed);
            state = State.jump;
        }

    }
    private void VelocityState()
    {
        

        if (state == State.jump | !(coll.IsTouchingLayers(ground)))
        {
            if (rb.velocity.y < 0.1)
            {                              
                state = State.falling;
                rb.gravityScale = 9.81f;
                
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
    } //falta fazer a animação de grudar na wall
}
