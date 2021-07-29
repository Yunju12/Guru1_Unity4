using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Board_PlayerMove : MonoBehaviour
{
    //�ֻ��� ����
    public GameObject dice;

    public GameObject dice1;
    public GameObject dice2;
    public GameObject dice3;
    public GameObject dice4;
    public GameObject dice5;
    public GameObject dice6;

    int ran;

    //��ǥ �̵� ����
    public static double posx = 0;
    Vector3 toPosX { get { return new Vector3((float)posx, 1.2f, 0); } }

    //�� ��ǥ
    Vector3 toPosH { get { return new Vector3(55.75f, 1.2f, 0); } }

    //�̵� ��ǥ ����
    public GameObject buttonS1P;

    // �̵��� ���� ����
    Rigidbody2D rigid;

    // �ӷ� ����
    public float moveSpeed = 7.0f;

    // �ִϸ��̼� ����
    Animator ani;

    GameManager gm;

    // ����� �ҽ� ������Ʈ
    private AudioSource audio;

    //�÷��̾� ���� ������Ʈ
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        rigid = GetComponent<Rigidbody2D>();

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();

        gm = GetComponent<GameManager>();

        audio = GetComponent<AudioSource>();

        //�� �̵� ��ư ����
        buttonS1P.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class ExampleClass : MonoBehaviour
    {

    }

    //�ֻ��� ������
    public void DiceRandom()
    {
        //�� �̵� ��ư ����
        buttonS1P.SetActive(false);

        dice1.SetActive(false);
        dice2.SetActive(false);
        dice3.SetActive(false);
        dice4.SetActive(false);
        dice5.SetActive(false);
        dice6.SetActive(false);

        //ran = Random.Range(1, 7);
        //print(ran);
        ran = 1;

        if (ran == 1)
        {
            dice1.SetActive(true);
            posx += 2.23;
            if(posx >= 55.75)
            {
                StartCoroutine(MoveTo(player, toPosH));
            }
            else
            {
                StartCoroutine(MoveTo(player, toPosX));
            }
        }
        else if (ran == 2)
        {
            dice2.SetActive(true);
            posx += 4.46;
            if (posx >= 55.75)
            {
                StartCoroutine(MoveTo(player, toPosH));
            }
            else
            {
                StartCoroutine(MoveTo(player, toPosX));
            }
        }
        else if (ran == 3)
        {
            dice3.SetActive(true);
            posx += 6.69;
            if (posx >= 55.75)
            {
                StartCoroutine(MoveTo(player, toPosH));
            }
            else
            {
                StartCoroutine(MoveTo(player, toPosX));
            }
        }
        else if (ran == 4)
        {
            dice4.SetActive(true);
            posx += 8.92;
            if (posx >= 55.75)
            {
                StartCoroutine(MoveTo(player, toPosH));
            }
            else
            {
                StartCoroutine(MoveTo(player, toPosX));
            }
        }
        else if (ran == 5)
        {
            dice5.SetActive(true);
            posx += 11.15;
            if (posx >= 55.75)
            {
                StartCoroutine(MoveTo(player, toPosH));
            }
            else
            {
                StartCoroutine(MoveTo(player, toPosX));
            }
        }
        else if (ran == 6)
        {
            dice6.SetActive(true);
            posx += 13.38;
            if (posx >= 55.75)
            {
                StartCoroutine(MoveTo(player, toPosH));
            }
            else
            {
                StartCoroutine(MoveTo(player, toPosX));
            }
        }

        dice.SetActive(true);
    }

    IEnumerator MoveTo(GameObject player, Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = player.transform.position;
        while (true)
        {
            count += Time.deltaTime;
            player.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                player.transform.position = toPos;

                if (posx - 4.46 <= 0.001f && posx - 4.46 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 8.92 <= 0.001f && posx - 8.92 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 13.38 <= 0.001f && posx - 13.38 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 15.61 <= 0.001f && posx - 15.61 >= -0.001f)
                {
                    EggHp.eggHp++;
                }
                else if (posx - 17.84 <= 0.001f && posx - 17.84 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 20.07 <= 0.001f && posx - 20.07 >= -0.001f)
                {
                    EggHp.eggHp++;
                }
                else if (posx - 24.53 <= 0.001f && posx - 24.53 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 28.99 <= 0.001f && posx - 28.99 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 31.22 <= 0.001f && posx - 31.22 >= -0.001f)
                {
                    EggHp.eggHp += 2;
                }
                else if (posx - 37.91 <= 0.001f && posx - 37.91 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 42.37 <= 0.001f && posx - 42.37 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 49.06 <= 0.001f && posx - 49.06 >= -0.001f)
                {
                    EggHp.eggHp++;
                }
                else if (posx - 51.29 <= 0.001f && posx - 51.29 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx - 53.52 <= 0.001f && posx - 53.52 >= -0.001f)
                {
                    buttonS1P.SetActive(true);
                }
                else if (posx >= 55)
                {
                    buttonS1P.SetActive(true);
                }
                break;
            }
            yield return null;
        }
    }

    public void SlP()
    {
        //�� ��ȯ
        SceneManager.LoadScene("S1-Plat");
    }
}
