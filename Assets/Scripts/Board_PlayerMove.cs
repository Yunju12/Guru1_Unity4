using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Board_PlayerMove : MonoBehaviour
{
    //전체 점수 변수
    public static int totalScore = 0;
    public Text score;

    //주사위 변수
    public GameObject dice;

    public GameObject dice1;
    public GameObject dice2;
    public GameObject dice3;
    public GameObject dice4;
    public GameObject dice5;
    public GameObject dice6;

    //랜덤 주사위
    int ran;

    //랜덤 박스
    int rBox;

    public GameObject randomEgg;
    public GameObject randomPotion;
    public GameObject randomBomb;

    //NPC
    public GameObject NPC_P;
    public GameObject NPC_E;
    public GameObject NPC_B;
    public GameObject NPC_E2;


    //좌표 이동 변수
    public static double posx = 0;
    Vector3 toPosX { get { return new Vector3((float)posx, 1.2f, 0); } }

    //집 좌표
    Vector3 toPosH { get { return new Vector3(55.75f, 1.2f, 0); } }

    //이동 좌표 변수
    public GameObject buttonS1P;
    public GameObject buttonS1S;
    public GameObject buttonS2P;
    public GameObject buttonS2S;
    public GameObject buttonS3P;
    public GameObject buttonS3S;
    public GameObject buttonH;

    // 이동을 위한 변수
    Rigidbody2D rigid;

    // 속력 변수
    public float moveSpeed = 7.0f;

    // 애니메이션 변수
    Animator ani;

    GameManager gm;

    // 오디오 소스 컴포넌트
    //private AudioSource audio;

    //플레이어 게임 오브젝트
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        
        this.gameObject.transform.position = new Vector3((float)posx, 1.2f, 0);

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        rigid = GetComponent<Rigidbody2D>();

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        ani = GetComponent<Animator>();

        //audio = GetComponent<AudioSource>();

        gm = GetComponent<GameManager>();

        //전체 점수 표시
        score.text = totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class ExampleClass : MonoBehaviour
    {

    }

    //주사위 굴리기
    public void DiceRandom()
    {
        //씬 이동 버튼 끄기
        buttonS1P.SetActive(false);
        buttonS1S.SetActive(false);
        buttonS2P.SetActive(false);
        buttonS2S.SetActive(false);
        buttonS3P.SetActive(false);
        buttonS3S.SetActive(false);
        buttonH.SetActive(false);

        dice1.SetActive(false);
        dice2.SetActive(false);
        dice3.SetActive(false);
        dice4.SetActive(false);
        dice5.SetActive(false);
        dice6.SetActive(false);

        randomEgg.SetActive(false);
        randomPotion.SetActive(false);
        randomBomb.SetActive(false);
        NPC_P.SetActive(false);
        NPC_E.SetActive(false);
        NPC_B.SetActive(false);
        NPC_E2.SetActive(false);


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
                    EggHp.potionCount++;
                    NPC_P.SetActive(true);
                }
                else if (posx - 13.38 <= 0.001f && posx - 13.38 >= -0.001f)
                {
                    buttonS1S.SetActive(true);
                }
                else if (posx - 15.61 <= 0.001f && posx - 15.61 >= -0.001f)
                {
                    EggHp.eggHp++;
                    NPC_E.SetActive(true);
                }
                else if (posx - 17.84 <= 0.001f && posx - 17.84 >= -0.001f)
                {
                    buttonS2P.SetActive(true);
                }
                else if (posx - 20.07 <= 0.001f && posx - 20.07 >= -0.001f)
                {
                    EggHp.eggHp++;
                    NPC_E.SetActive(true);
                }
                else if (posx - 24.53 <= 0.001f && posx - 24.53 >= -0.001f)
                {
                    RandomBox();
                }
                else if (posx - 28.99 <= 0.001f && posx - 28.99 >= -0.001f)
                {
                    buttonS2S.SetActive(true);
                }
                else if (posx - 31.22 <= 0.001f && posx - 31.22 >= -0.001f)
                {
                    EggHp.eggHp += 2;
                    NPC_E2.SetActive(true);
                }
                else if (posx - 37.91 <= 0.001f && posx - 37.91 >= -0.001f)
                {
                    buttonS3P.SetActive(true);
                }
                else if (posx - 42.37 <= 0.001f && posx - 42.37 >= -0.001f)
                {
                    EggHp.potionCount++;
                    NPC_P.SetActive(true);
                }
                else if (posx - 49.06 <= 0.001f && posx - 49.06 >= -0.001f)
                {
                    EggHp.eggHp++;
                    NPC_E.SetActive(true);
                }
                else if (posx - 51.29 <= 0.001f && posx - 51.29 >= -0.001f)
                {
                    RandomBox();
                }
                else if (posx - 53.52 <= 0.001f && posx - 53.52 >= -0.001f)
                {
                    buttonS3S.SetActive(true);
                }
                else if (posx >= 55)
                {
                    buttonH.SetActive(true);
                }
                break;
            }
            yield return null;
        }
    }

    public void SlP()
    {
        //씬 전환
        SceneManager.LoadScene("S1-Plat");
    }

    public void SlS()
    {
        //씬 전환
        SceneManager.LoadScene("S1-Shooting");
    }

    public void S2P()
    {
        //씬 전환
        SceneManager.LoadScene("S2-Plat");
    }

    public void S2S()
    {
        //씬 전환
        SceneManager.LoadScene("S2-Shooting");
    }

    public void S3P()
    {
        //씬 전환
        SceneManager.LoadScene("S3-Plat");
    }

    public void S3S()
    {
        //씬 전환
        SceneManager.LoadScene("S3-Shooting");
    }
    public void H()
    {
        //씬 전환
        SceneManager.LoadScene("UI_Ending");
    }

    public void RandomBox()
    {
        rBox = Random.Range(1, 4);
        
        if(rBox == 1)
        {
            EggHp.eggHp++;
            randomEgg.SetActive(true);
        }
        else if(rBox == 2)
        {
            EggHp.potionCount++;
            randomPotion.SetActive(true);
        }
        else if(rBox == 3)
        {
            EggHp.bombCount++;
            randomBomb.SetActive(true);
        }
    }

  


}
