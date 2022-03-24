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
    bool down = false;

    float moveHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region input 
        //gå input
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //hop input
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
        #region Gå
        if (moveHorizontal > 0 || moveHorizontal < 0)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * speed, 0), ForceMode2D.Impulse);
            //rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);

        }
        #endregion
        #region Hoppa
        if (isGrund == true && isJump == true)
        {
            rb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            //rb2D.velocity = new Vector2(rb2D.velocity.x, jumpforce);
            isJump = false;
        }
        #endregion
        #region ner åt
        if(down == true) {
            rb2D.AddForce(new Vector2(0, -speed), ForceMode2D.Impulse);
            down = false;
        }
        #endregion
        #region Maxhstighet
        // läs här igen sen
        // https://answers.unity.com/questions/9985/limiting-rigidbody-velocity.html den är om, magnitude. då avänder den inte men bra att veta

        Debug.Log(rb2D.velocity.magnitude);
        if (Mathf.Abs(rb2D.velocity.x) > 8)
        {
            Debug.Log("yes");
            rb2D.AddForce(new Vector2(-moveHorizontal * speed, 0), ForceMode2D.Impulse);
        }
    #endregion
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