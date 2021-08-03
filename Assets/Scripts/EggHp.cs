using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EggHp : MonoBehaviour
{
    //¸ñ¼û(°è¶õ)
    public static int eggHp = 5;

    public GameObject egg1;
    public Text eggText;

    //Æ÷¼Ç
    public static int potionCount = 0;

    public GameObject potion1;
    public Text potionText;

    //ÆøÅº
    public static int bombCount = 0;

    public GameObject bomb1;
    public Text bombText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //°è¶õ UI
        if (eggHp < 0)
        {
            //¾À ÀüÈ¯
            SceneManager.LoadScene("BadEnding_2");
        }
        else
        {
            egg1.SetActive(true);
            eggText.text = "X" + eggHp;
        }

        //Æ÷¼Ç UI
        potion1.SetActive(true);
        potionText.text = "X" + potionCount;

        //ÆøÅº UI
        bomb1.SetActive(true);
        bombText.text = "X" + bombCount;
    }
}
