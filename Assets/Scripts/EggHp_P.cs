using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EggHp_P : MonoBehaviour
{
    //���(���)
    public GameObject egg1;
    public Text eggText;

    //��ź
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
        if (EggHp.eggHp < 0)
        {
            //�� ��ȯ
            SceneManager.LoadScene("BadEnding_2");
        }
        else
        {
            egg1.SetActive(true);
            eggText.text = "X" + EggHp.eggHp;
        }

        //��ź UI
        if (EggHp.bombCount == 0)
        {
            bomb1.SetActive(false);
            bomb2.SetActive(false);
        }
        else if (EggHp.bombCount == 1)
        {
            bomb1.SetActive(true);
            bomb2.SetActive(false);
        }
        else if (EggHp.bombCount == 2)
        {
            bomb1.SetActive(true);
            bomb2.SetActive(true);
        }
    }
}
