using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;

    private bool isGrounded; 
    private bool isJumped;

    private float jumpPower = 7f;


    void Awake() {

        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        CheckIfGrounded();
        PlayerJumo();
    }


    //Called as Fixed Timestamp, like 20 times in a sec
    void FixedUpdate(){
        PlayerWalk();
    }

    void PlayerWalk(){
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0f){
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);

        }else if(h < 0f){
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);

        }
        else{
            myBody.velocity = new Vector2(0f, myBody.velocity.y);

        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));


    }


    void ChangeDirection(int direction){
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }


    void OnCollisionEnter2D(Collision2D target){

        /*        
        if(target.gameObject.tag == "Ground"){
            Debug.Log("Collided with Ground");
        }
        */
    }

    void CheckIfGrounded(){
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f);

        if(isGrounded){
            if(isJumped){
                isJumped = false;
                anim.SetBool("Jump", false);
            }
        }

    }

    void PlayerJumo(){
        if(isGrounded){

            if(Input.GetKey(KeyCode.Space)){
                isJumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }

    // Update is called once per frame

}




























