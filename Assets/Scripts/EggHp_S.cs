using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EggHp_S : MonoBehaviour
{
    //목숨(계란)
    public GameObject egg1;
    public Text eggText;

    //포션
    public GameObject potion1;
    public Text potionText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //계란 UI
        if (EggHp.eggHp < 0)
        {
            //씬 전환
            SceneManager.LoadScene("BadEnding_2");
        }
        else
        {
            egg1.SetActive(true);
            eggText.text = "X" + EggHp.eggHp;
        }

        //포션 UI
        potion1.SetActive(true);
        potionText.text = "X" + EggHp.potionCount;
    }
}
