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
    public float jumpforce = 0.00001f;

    // olika vilkor
    bool isJump = false;
    bool down = false;
    float moveHorizontal;
    bool getIsJumpingOkej;

    // coyote and jumpbuff
    float coyoteTime = 0.2f;
    float coyotetimeDiff;
    float jumpBuff = 0.5f;
    float jumpBuffDiff;

    //hopp fix
    float jumpFixTime = 0.7f;
    float jumpFixTimeDiff;

    //hopp saker
    bool holdingDownSpace;
    float longJump = 1f;
    float longJumpDiff;
    bool stopHold;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        JumpingOkej = LayerMask.GetMask("JumpingOkej");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        cC2D = gameObject.GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        #region coyote and jumpbuff and LongJump
        if (IsJumpimngOkej())
        {
            jumpFixTimeDiff = jumpFixTime;
            coyotetimeDiff = coyoteTime;
            longJumpDiff = longJump;
        }
        else
        {
            coyotetimeDiff -= Time.deltaTime;
            jumpFixTimeDiff -= Time.deltaTime;
            longJumpDiff -= Time.deltaTime;
        }

        #endregion
        #region input 
        //gå input
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //hop input
        if (Input.GetKey(KeyCode.Space) == true)
        {
            jumpBuffDiff = jumpBuff;
        }
        else
        {
            jumpBuffDiff -= Time.deltaTime;
        }

        if (coyotetimeDiff > 0f && jumpBuffDiff > 0f)
        {
            isJump = true;
            coyotetimeDiff = 0;
            jumpBuffDiff = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            holdingDownSpace = true;
        }
        else
        {
            stopHold = false;
        }

        if (IsJumpimngOkej() == true) { stopHold = true; }

        Debug.Log(stopHold);
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
        if (isJump == true)
        {
            rb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            isJump = false;
        }
        else { isJump = false; }

        if (holdingDownSpace == true && longJumpDiff > 0f && stopHold == true)
        {
            rb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            holdingDownSpace = false;
        }

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
        if (getIsJumpingOkej == false && IsJumpimngOkej() == true && rb2D.velocity.magnitude > 14 && Mathf.Abs(moveHorizontal) == 1 && jumpFixTimeDiff < 0f)
        {
            rb2D.AddForce(new Vector2(rb2D.velocity.x, 0), ForceMode2D.Impulse);
            jumpBuffDiff = 0;
        }
        getIsJumpingOkej = IsJumpimngOkej();

        #endregion
    }

    public bool IsJumpimngOkej()
    {
        return Physics2D.BoxCast(cC2D.bounds.center, new Vector2(cC2D.size.x - .2f, cC2D.size.y), 0f, Vector2.down, .5f, JumpingOkej);
    }
}