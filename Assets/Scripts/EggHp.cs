using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHp : MonoBehaviour
{
    //¸ñ¼û(°è¶õ)
    public static int eggHp = 5;

    public GameObject egg1;
    public GameObject egg2;
    public GameObject egg3;
    public GameObject egg4;
    public GameObject egg5;
    public GameObject egg6;
    public GameObject egg7;
    public GameObject egg8;
    public GameObject egg9;
    public GameObject egg10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (eggHp == 10)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(true);
            egg6.SetActive(true);
            egg7.SetActive(true);
            egg8.SetActive(true);
            egg9.SetActive(true);
            egg10.SetActive(true);
        }
        else if (eggHp == 9)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(true);
            egg6.SetActive(true);
            egg7.SetActive(true);
            egg8.SetActive(true);
            egg9.SetActive(true);
            egg10.SetActive(false);
        }
        else if (eggHp == 8)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(true);
            egg6.SetActive(true);
            egg7.SetActive(true);
            egg8.SetActive(true);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 7)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(true);
            egg6.SetActive(true);
            egg7.SetActive(true);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 6)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(true);
            egg6.SetActive(true);
            egg7.SetActive(false);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 5)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(true);
            egg6.SetActive(false);
            egg7.SetActive(false);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 4)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(true);
            egg5.SetActive(false);
            egg6.SetActive(false);
            egg7.SetActive(false);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 3)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            egg4.SetActive(false);
            egg5.SetActive(false);
            egg6.SetActive(false);
            egg7.SetActive(false);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 2)
        {
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(false);
            egg4.SetActive(false);
            egg5.SetActive(false);
            egg6.SetActive(false);
            egg7.SetActive(false);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 1)
        {
            egg1.SetActive(true);
            egg2.SetActive(false);
            egg3.SetActive(false);
            egg4.SetActive(false);
            egg5.SetActive(false);
            egg6.SetActive(false);
            egg7.SetActive(false);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
        else if (eggHp == 0)
        {
            egg1.SetActive(false);
            egg2.SetActive(false);
            egg3.SetActive(false);
            egg4.SetActive(false);
            egg5.SetActive(false);
            egg6.SetActive(false);
            egg7.SetActive(false);
            egg8.SetActive(false);
            egg9.SetActive(false);
            egg10.SetActive(false);
        }
    }
}
