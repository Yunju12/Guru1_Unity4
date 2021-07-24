using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총알공장
    public GameObject bulletFactory;

    // 발사
    public GameObject firePosition;

    void Start()
    {
        
    }

    void Update()
    {
        // 사용자가 발사버튼(Ctrl)을 누르면 총알 발사하기
        // 만약 사용자가 발사버튼을 누르면
        // 총알 공장에서 총알을 만들고, 총알을 발사(총구에 배치)하고 싶다.
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
