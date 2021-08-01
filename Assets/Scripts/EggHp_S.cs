using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EggHp_S : MonoBehaviour
{
    //목숨(계란)
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
    public GameObject egg11;
    public GameObject egg12;

    //포션
    public GameObject potion1;
    public GameObject potion2;
    public GameObject potion3;
    public GameObject potion4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //계란 UI
        if (EggHp.eggHp == 12)
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
            egg11.SetActive(true);
            egg12.SetActive(true);
        }
        else if (EggHp.eggHp == 11)
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
            egg11.SetActive(true);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 10)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 9)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 8)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 7)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 6)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 5)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 4)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 3)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 2)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 1)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp == 0)
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
            egg11.SetActive(false);
            egg12.SetActive(false);
        }
        else if (EggHp.eggHp < 0)
        {
            //씬 전환
            SceneManager.LoadScene("BadEnding_2");
        }

        //포션 UI
        if (EggHp.potionCount == 0)
        {
            potion1.SetActive(false);
            potion2.SetActive(false);
            potion3.SetActive(false);
            potion4.SetActive(false);
        }
        if (EggHp.potionCount == 1)
        {
            potion1.SetActive(true);
            potion2.SetActive(false);
            potion3.SetActive(false);
            potion4.SetActive(false);
        }
        else if (EggHp.potionCount == 2)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
            potion3.SetActive(false);
            potion4.SetActive(false);
        }
        else if (EggHp.potionCount == 3)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
            potion3.SetActive(true);
            potion4.SetActive(false);
        }
        else if (EggHp.potionCount == 4)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
            potion3.SetActive(true);
            potion4.SetActive(true);
        }
    }
}
