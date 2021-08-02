using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
            Debug.Log("explosion"); 
        }
    }
}
