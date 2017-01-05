using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    private float followSpeed = 12f;
    private Rigidbody2D rb;
    private Vector2 bottomLeftScreen;
    private Vector2 topRightScreen;

    [SerializeField]
    GameObject ball;
    // Use this for initialization
    void Awake()
    {
        bottomLeftScreen = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRightScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(ball.transform.position.y - transform.position.y) > transform.localScale.y/2f - .8f)
        {
            Vector2 targetVect = (new Vector2(transform.position.x,ball.transform.position.y - transform.position.y) + ball.GetComponent<Rigidbody2D>().velocity * Time.fixedDeltaTime).normalized;
            rb.AddForce(targetVect * followSpeed, ForceMode2D.Impulse);
        }
        if(transform.position.y + transform.localScale.y/2f > topRightScreen.y && Mathf.Sign(rb.velocity.y) == 1 || transform.position.y - transform.localScale.y / 2f < bottomLeftScreen.y && Mathf.Sign(rb.velocity.y) == -1)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
