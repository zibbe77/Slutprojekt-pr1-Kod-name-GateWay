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


        speed = 0.1f;
        jumpforce = 2;
        isJump = false;
        isGrund = true;

        Debug.Log("runing2");
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        //position = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        // transform.Translate(position);
        if (Input.GetKey("space") == true)
        {
            isJump = true;
        }

    }
    void FixedUpdate()
    {
        if (moveHorizontal > 0 || moveHorizontal < 0)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Impulse);
        }

        if (isGrund && isJump == true)
        {
            rb2D.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            isJump = false;
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("isgrund");
        if (collision.gameObject.tag == "grid")
        {
            isGrund = true;
            
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "grid")
        {
            isGrund = false;
            Debug.Log("left");
        }
    }
}