using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBullet : MonoBehaviour
{

    private float speed = 7f;
    private Animator anim;

    private bool canMove;

    void Awake()
    {
        anim = GetComponent<Animator>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){

        if(canMove){
            Vector3 tempPos = transform.position;
            tempPos.x += speed * Time.deltaTime;
            transform.position = tempPos;
        }
    }

    public float Speed{
        get{
            return speed;
        }
        set{
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer){
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.ObjectTags.SNAIL_TAG || target.tag == MyTags.ObjectTags.BETTLE_TAG){
            //gameObject.SetActive(false);
            anim.Play("Explode");
            canMove = false;
            StartCoroutine(DisableBullet(0.2f));
        }
    }

}
