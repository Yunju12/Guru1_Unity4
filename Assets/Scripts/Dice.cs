using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    //주사위 변수
    public GameObject dice;

    public GameObject dice1;
    public GameObject dice2;
    public GameObject dice3;
    public GameObject dice4;
    public GameObject dice5;
    public GameObject dice6;

    int ran;

    // Start is called before the first frame update
    void Start()
    {
        dice.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //주사위 굴리기
    public void DiceRandom()
    {
        dice.SetActive(false);

        dice1.SetActive(false);
        dice2.SetActive(false);
        dice3.SetActive(false);
        dice4.SetActive(false);
        dice5.SetActive(false);
        dice6.SetActive(false);

        ran = Random.Range(1, 7);
        print(ran);

        if (ran == 1)
        {
            dice1.SetActive(true);
        }
        else if (ran == 2)
        {
            dice2.SetActive(true);
        }
        else if (ran == 3)
        {
            dice3.SetActive(true);
        }
        else if (ran == 4)
        {
            dice4.SetActive(true);
        }
        else if (ran == 5)
        {
            dice5.SetActive(true);
        }
        else if (ran == 6)
        {
            dice6.SetActive(true);
        }
    }
}
