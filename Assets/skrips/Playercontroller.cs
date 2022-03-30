using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    #region Class varjablar

    Rigidbody2D rb2D;
    CapsuleCollider2D cC2D;
    LayerMask JumpingOkej;

    // olika speed 
    public float speed = 1;
    public float jumpforce = 10;

    // olika vilkor
    bool isJump = false;
    bool down = false;
    float moveHorizontal;
    bool getIsJumpingOkej;
    bool inAir;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        JumpingOkej = LayerMask.GetMask("JumpingOkej");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        cC2D = gameObject.GetComponent<CapsuleCollider2D>();
        inAir = false;
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
            isJump = true;
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
        if (isJump == true && IsJumpimngOkej() == true)
        {
            rb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            isJump = false;
            inAir = true;
        }
        else { isJump = false; }

        //rör dig neråt 
        if (down == true)
        {
            rb2D.AddForce(new Vector2(0, -speed), ForceMode2D.Impulse);
            down = false;
        }

        #endregion
        #region rättar input

        // begränsar accelrationen 
        if (Mathf.Abs(rb2D.velocity.x) > 8)
        {
            rb2D.AddForce(new Vector2(-moveHorizontal * speed, 0), ForceMode2D.Impulse);
        }

        // Lägger til kraft när man landar

        Debug.Log(inAir);

        if (getIsJumpingOkej == false && IsJumpimngOkej() == true)
        {
            rb2D.AddForce(new Vector2(rb2D.velocity.x, 0), ForceMode2D.Impulse);
        }

        getIsJumpingOkej = IsJumpimngOkej();

        /*
        if (getIsJumpingOkej == true)
        {
            inAir = false;
        }
        */


        #endregion
    }

    public bool IsJumpimngOkej()
    {
        return Physics2D.BoxCast(cC2D.bounds.center, new Vector2(cC2D.size.x - .2f, cC2D.size.y), 0f, Vector2.down, .5f, JumpingOkej);
    }
}