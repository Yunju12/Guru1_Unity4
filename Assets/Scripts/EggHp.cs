using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EggHp : MonoBehaviour
{
    //���(���)
    public static int eggHp = 5;

    public GameObject egg1;
    public Text eggText;

    //����
    public static int potionCount = 0;

    public GameObject potion1;
    public GameObject potion2;
    public GameObject potion3;
    public GameObject potion4;

    //��ź
    public static int bombCount = 0;

    public GameObject bomb1;
    public GameObject bomb2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //��� UI
        if (eggHp < 0)
        {
            //�� ��ȯ
            SceneManager.LoadScene("BadEnding_2");
        }
        else
        {
            egg1.SetActive(true);
            eggText.text = "X" + eggHp;
        }

        //���� UI
        if (potionCount == 0)
        {
            potion1.SetActive(false);
            potion2.SetActive(false);
            potion3.SetActive(false);
            potion4.SetActive(false);
        }
        if (potionCount == 1)
        {
            potion1.SetActive(true);
            potion2.SetActive(false);
            potion3.SetActive(false);
            potion4.SetActive(false);
        }
        else if (potionCount == 2)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
            potion3.SetActive(false);
            potion4.SetActive(false);
        }
        else if (potionCount == 3)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
            potion3.SetActive(true);
            potion4.SetActive(false);
        }
        else if (potionCount == 4)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
            potion3.SetActive(true);
            potion4.SetActive(true);
        }

        //��ź UI
        if (bombCount == 0)
        {
            bomb1.SetActive(false);
            bomb2.SetActive(false);
        }
        else if (bombCount == 1)
        {
            bomb1.SetActive(true);
            bomb2.SetActive(false);
        }
        else if (bombCount == 2)
        {
            bomb1.SetActive(true);
            bomb2.SetActive(true);
        }
    }
}
