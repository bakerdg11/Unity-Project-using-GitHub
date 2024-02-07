using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    public Vector2 initialVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if (lifeTime <= 0)
        {
            lifeTime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = initialVelocity;
        Destroy(gameObject, lifeTime);
    }


    //void Update()
    //{
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    if (rb.velocity.magnitude < 0.8f)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D wall)
    {

        if (wall.gameObject.tag == "Wall")
        {
            Destroy(gameObject);

        }


    }









}