using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject fireBullet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet(){
        if(Input.GetKeyDown(KeyCode.J)){
            //Create a bullet object copy of the fireBullet at the Player position and rotation
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            
            bullet.GetComponent<FlameBullet>().Speed *= transform.localScale.x;
        }
    }
}
