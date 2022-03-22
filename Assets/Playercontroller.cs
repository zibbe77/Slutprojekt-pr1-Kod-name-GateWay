using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float speed = 1;
    public float jumpforce = 10;
    bool isJump = false;
    bool isGrund = true;

    float moveHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Space) == true)
        {
            if (isGrund == true)
            {
                Debug.Log("j");
                isJump = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (moveHorizontal > 0 || moveHorizontal < 0)
        {
            Debug.Log(rb2D.velocity);
            rb2D.AddForce(new Vector2(moveHorizontal * speed, 0), ForceMode2D.Impulse);
            //rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);

        }

        if (isGrund == true && isJump == true)
        {
            rb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            //rb2D.velocity = new Vector2(rb2D.velocity.x, jumpforce);
            isJump = false;
        }

        if (rb2D.velocity.x >= 5)
        {
            Debug.Log("yes");
            rb2D.AddForce(new Vector2(moveHorizontal * -speed, 0), ForceMode2D.Impulse);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "box")
        {

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Grid")
        {
            isGrund = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "Grid")
        {
            isGrund = false;
        }
    }
}