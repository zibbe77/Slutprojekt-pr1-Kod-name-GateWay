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
    public float speed;
    public float maxSpeed;
    public float upSpeed;

    // olika vilkor
    bool isJump = false;
    bool down = false;
    float moveHorizontal;

    //hopp saker
    bool holdingDownSpace;
    public float longJump;
    float longJumpDiff;
    bool isJumpOkejSet;

    //gå 
    public float Acceleration;
    public float decceleration;
    public float speedPower;

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
        //effiktivar hoppas jag 
        isJumpOkejSet = IsJumpimngOkej();

        #region LongJump timer
        if (isJumpOkejSet)
        {
            longJumpDiff = longJump;
        }
        else
        {
            longJumpDiff -= Time.deltaTime;
        }

        #endregion
        #region input 

        //gå input
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //hop input
        if (Input.GetKey(KeyCode.Space) == true)
        {
            isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            holdingDownSpace = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdingDownSpace = false;
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

        #region gå
        // kommer ifrån https://www.youtube.com/watch?v=KbtcEVCM7bw&list=LL&index=3&t=126s
        //räknar utt den hastigheten vi vill nå
        float targetSpeed = moveHorizontal * maxSpeed;
        //räknar utt skilnaden 
        float speedDif = targetSpeed - rb2D.velocity.x;
        //chekar om den ska axelera eller decceleration --- Går att ta bort för att den för den är bara kalubreing
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Acceleration : decceleration;
        //räknar ut hastigheren -- Sign tar rikningen 
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, speedPower) * Mathf.Sign(speedDif);

        rb2D.AddForce(movement * Vector2.right, ForceMode2D.Impulse);
        #endregion

        #region transulating input hopp

        if (holdingDownSpace == true && longJumpDiff > 0)
        {
            rb2D.AddForce(new Vector2(0, upSpeed), ForceMode2D.Impulse);
        }

        if (isJump == true && isJumpOkejSet == true)
        {
            rb2D.AddForce(new Vector2(0, upSpeed * 3), ForceMode2D.Impulse);
            isJump = false;
        }
        else if (isJump == true)
        {
            isJump = false;
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
        /*
        if (Mathf.Abs(rb2D.velocity.x) > maxSpeed)
        {
            rb2D.AddForce(new Vector2(-moveHorizontal * speed, 0), ForceMode2D.Impulse);
        }
        */
        #endregion
    }

    public bool IsJumpimngOkej()
    {
        return Physics2D.BoxCast(cC2D.bounds.center, new Vector2(cC2D.size.x - .2f, cC2D.size.y), 0f, Vector2.down, .1f, JumpingOkej);
    }
}