using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    Animator anim;
    public GameObject explosion;
    //Rigidbody2D rigid;

    void Start()
    {
        //rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
        }

    }
}
