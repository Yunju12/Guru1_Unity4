using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //¸ñ¼û(°è¶õ)
    public static int eggHp;

    public GameObject egg1;
    public GameObject egg2;
    public GameObject egg3;
    public GameObject egg4;
    public GameObject egg5;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (eggHp == 4)
            egg1.SetActive(false);
        else if (eggHp == 3)
            egg2.SetActive(false);
        else if (eggHp == 2)
            egg3.SetActive(false);
        else if (eggHp == 1)
            egg4.SetActive(false);
        else
            egg5.SetActive(false);
    }
}
