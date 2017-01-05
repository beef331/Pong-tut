using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rb;
    private float startingVel = 10f;
    private float maxVel = 50f;
    private bool offScreen;
    private Vector2 bottomLeftScreen;
    private Vector2 topRightScreen;
    private TrailRenderer tr;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        bottomLeftScreen = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRightScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        ShootBall();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < bottomLeftScreen.x && !offScreen)
        {
            offScreen = true;
            ScoreManager.rightScore++;
            ResetBall();

        }
        else if (transform.position.x > topRightScreen.x && !offScreen)
        {
            offScreen = true;
            ScoreManager.leftScore++;
            tr.enabled = false;
            ResetBall();
        }
        if (transform.position.y + transform.localScale.y / 2f >= topRightScreen.y && Mathf.Sign(rb.velocity.y) == 1 || transform.position.y - transform.localScale.y / 2f <= bottomLeftScreen.y && Mathf.Sign(rb.velocity.y) == -1)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }
    void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        offScreen = false;
        tr.enabled = true;
        ShootBall();
    }
    void ShootBall()
    {
        Vector2 dir = Vector2.zero;
        int rdm = Random.Range(0, 2);
        if(rdm == 1)
        {
            dir = new Vector2(1, Random.Range(-.75f, .75f));
        }else
        {
            dir = new Vector2(-1, Random.Range(-.75f, .75f));
        }
        rb.AddForce(dir.normalized * startingVel, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float newYVel = rb.velocity.y + collision.gameObject.GetComponent<Rigidbody2D>().velocity.y/3f;
        if (newYVel < startingVel)
        {
            if (collision.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(startingVel, newYVel);
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-startingVel, newYVel);
            }
        }
        else
        {
            if (collision.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(startingVel, rb.velocity.y);
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-startingVel, rb.velocity.y);
            }
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = .125f;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 1f;
    }
    */
}
