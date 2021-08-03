using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EggHp_S : MonoBehaviour
{
    //���(���)
    public GameObject egg1;
    public Text eggText;

    //����
    public GameObject potion1;
    public Text potionText;

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

        //���� UI
        potion1.SetActive(true);
        potionText.text = "X" + EggHp.potionCount;
    }
}
