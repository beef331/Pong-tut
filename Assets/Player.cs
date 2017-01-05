using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;
    private float vel = 3f;
    private Vector2 bottomLeftScreen;
    private Vector2 topRightScreen;
    // Use this for initialization
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bottomLeftScreen = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRightScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float vertAxis = Input.GetAxis("Vertical");
        Vector2 targetVect = new Vector2(0, vertAxis).normalized;
        rb.AddForce(targetVect * vel, ForceMode2D.Impulse);
        if (transform.position.y + transform.localScale.y / 2f > topRightScreen.y && Mathf.Sign(rb.velocity.y) == 1 || transform.position.y - transform.localScale.y / 2f < bottomLeftScreen.y && Mathf.Sign(rb.velocity.y) == -1)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
