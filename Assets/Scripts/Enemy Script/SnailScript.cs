using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 0.5f;

    public LayerMask playerLayer;    

    private Rigidbody2D myBody;
    private Animator anim;

    private bool isMoveLeft;

    private bool canMove;
    private bool stunned;

    public Transform leftCollision, rightCollision, topCollision, downCollision;
    public Vector3 leftCollisionVector, rightCollisionVector;


    void Awake() {

        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftCollisionVector = leftCollision.position;
        rightCollisionVector = rightCollision.position;


    }

    void Start()
    {
        isMoveLeft = true;
        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            if(isMoveLeft){
                myBody.velocity = new Vector2(-movementSpeed, myBody.velocity.y);
            }
            else{
                myBody.velocity = new Vector2(movementSpeed, myBody.velocity.y);
            }
        }

        CheckCollison();
        
    }

    void CheckCollison(){

        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerLayer);
        //RaycastHit2D topHit = Physics2D.Raycast(topCollision.position, Vector2.up, 0.1f, playerLayer);

        Collider2D topHitCollider = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

        if(topHitCollider != null){
            if(topHitCollider.gameObject.tag == MyTags.ObjectTags.PLAYER_TAG){
                if(!stunned){
                    topHitCollider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHitCollider.gameObject.GetComponent<Rigidbody2D>().velocity.x, 2f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    stunned = true;
                    
                    if(gameObject.tag == MyTags.ObjectTags.SNAIL_TAG){
                        anim.Play(MyTags.AnimTags.SnailTags.STUNNED_TAG);
                        StartCoroutine(Dead(3f));
                    }

                    //Bettle Code
                    if(gameObject.tag == MyTags.ObjectTags.BETTLE_TAG){

                        anim.Play("BettleStunned");

                        StartCoroutine(Dead(0.5f));
                    }


                }
            }
        }

        if(leftHit){
            if(leftHit.collider.gameObject.tag == MyTags.ObjectTags.PLAYER_TAG){

                if(!stunned){
                   //apply damage to player
                }
                else{

                    if(gameObject.tag != MyTags.ObjectTags.BETTLE_TAG){
                        //Move stunned body to right
                        //myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if(rightHit){
            if(rightHit.collider.gameObject.tag == MyTags.ObjectTags.PLAYER_TAG){

                if(!stunned){
                   //apply damage to player
                }
                else{
                    if(gameObject.tag != MyTags.ObjectTags.BETTLE_TAG){
                        //Move stunned body to right
                        //myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                
                }
            }
        }

        //If we do not detect collision any more then we need to change direction
        if(!Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f)){

            Console.Out.WriteLine("No Collision Detected");
            //isMoveLeft = !isMoveLeft;
            ChangeDirection();
        
        }
    }

    void ChangeDirection(){

        isMoveLeft = !isMoveLeft;


        Vector3 tempScale = transform.localScale;

        if(isMoveLeft){
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollision.position = leftCollisionVector;
            rightCollision.position = rightCollisionVector;            
        }
        else{
            tempScale.x = -Mathf.Abs(tempScale.x);

            leftCollision.position = rightCollisionVector;
            rightCollision.position = leftCollisionVector;
        }

        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer){
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == MyTags.ObjectTags.BULLET_TAG){
            
            if(tag == MyTags.ObjectTags.BETTLE_TAG){
                anim.Play("BettleStunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.5f));
            }

            if(tag == MyTags.ObjectTags.SNAIL_TAG){
                if(!stunned){
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    stunned = true;
                    anim.Play(MyTags.AnimTags.SnailTags.STUNNED_TAG);
                
                }
                else{
                    gameObject.SetActive(false);
                }
            }

        }
    }


}

