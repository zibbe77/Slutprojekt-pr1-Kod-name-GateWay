using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    #region Class varjablar

    Rigidbody2D rb2D;
    CapsuleCollider2D cC2D;
    public float speed = 1;
    public float jumpforce = 10;
    bool isJump = false;
    bool isGrund = true;
    bool down = false;

    float moveHorizontal;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        cC2D = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region input 
        //gå input
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //hop input
        
        RaycastHit2D hit2D = Physics2D.BoxCast(rb2D.position, new Vector2(cC2D.size.x, 1f), 0, Vector2.down);

        if (Input.GetKey(KeyCode.Space) == true)
        {
            if (isGrund == true)
            {
                isJump = true;
            }
        }
        // neråt input
        if (Input.GetKey(KeyCode.S))
        {
            down = true;
        }
        #endregion
    }
    void FixedUpdate()
    {
        #region transulating input

        //gå 
        rb2D.AddForce(new Vector2(moveHorizontal * speed, 0), ForceMode2D.Impulse);

        // hoppar 
        if (isGrund == true && isJump == true)
        {
            rb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            isJump = false;
        }

        //rör dig neråt 
        if (down == true)
        {

            rb2D.AddForce(new Vector2(0, -speed), ForceMode2D.Impulse);
            down = false;
        }

        // begränsar accelrationen 
        if (Mathf.Abs(rb2D.velocity.x) > 8)
        {
            rb2D.AddForce(new Vector2(-moveHorizontal * speed, 0), ForceMode2D.Impulse);
        }



        #endregion
    }

    #region cheking coliders 
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
    #endregion
}