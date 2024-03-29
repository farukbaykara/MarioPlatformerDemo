using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    
    private Rigidbody2D myBody;
    private Animator anim;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;

    
    private GameObject birdEgg;
    private bool attacked;
    private bool canMove;
    private LayerMask birdLayer;

    void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 10f;

        movePosition = transform.position;
        movePosition.x -= 10f;

        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTheBird();
        
    }

    void MoveTheBird(){
        if(canMove){
            transform.Translate(moveDirection * Time.smoothDeltaTime);
            if(transform.position.x >= originPosition.x){
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);

            }
            else if(transform.position.x <= movePosition.x){
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }

    void ChangeDirection(float direction){
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

}
