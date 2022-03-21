using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    Rigidbody2D rb2D;
    float speed;
    float jumpforce;
    bool isJump;
    bool isGrund;
    public Vector2 position = new Vector2();

    float moveHorizontal;

    public int hp = 100;
    public int dmg = 20;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();


        speed = 3;
        jumpforce = 5;
        isJump = false;
        isGrund = true;

        Debug.Log("runing2");
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey("space") == true)
        {
            if (isGrund == true)
            {
                isJump = true;
            }
        }

    }
    void FixedUpdate()
    {
        if (moveHorizontal > 0.1 || moveHorizontal < 0.1)
        {
            // rb2D.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Impulse);
            rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);
        }

        if (isGrund == true && isJump == true)
        {
            //rb2D.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpforce);
            isJump = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "box")
        {
            Debug.Log("box");
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
            Debug.Log("nya");
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