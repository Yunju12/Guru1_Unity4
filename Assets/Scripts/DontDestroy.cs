using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //¸ñ¼û(°è¶õ)
    public static int eggHp = 5;

    public GameObject egg1;
    public GameObject egg2;
    public GameObject egg3;
    public GameObject egg4;
    public GameObject egg5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (eggHp == 5)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(true);
        }
        else if (eggHp == 4)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(false);
        }
        else if (eggHp == 3)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(false);
            egg5.SetActive(false);
        }
        else if (eggHp == 2)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(false);
            egg4.SetActive(false);
            egg5.SetActive(false);
        }
        else if (eggHp == 1)
        {
            egg1.SetActive(true);
            egg2.SetActive(false);
            egg3.SetActive(false);
            egg4.SetActive(false);
            egg5.SetActive(false);
        }
        else
        {
            egg1.SetActive(false);
            egg2.SetActive(false);
            egg3.SetActive(false);
            egg4.SetActive(false);
            egg5.SetActive(false);

            DontDestroyOnLoad(gameObject);
        }
    }
}
