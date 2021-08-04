using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Slime : MonoBehaviour
{
    // ���� ��ü ����
    public int totalPoint;

    public int bossPoint = 500;

    public Text UIPoint;

    // ���� ���� ���
    public enum GameState
    {
        Ready,
        Run,
        GameOver,
        GameClear
    }

    // ���� ���� ����
    public GameState gState;

    // UI �ؽ�Ʈ ����
    public Text stateLabel;

    // �÷��̾� ���� ������Ʈ ����
    PlayerMove_Slime pms;

    // ���� ���� ������Ʈ ����
    BossMonster_Slime bms;

    // �̱���
    public static GameManager_Slime gm;

    // �ִϸ��̼� ����
    Animator ani;

    // ����� �ҽ� ������Ʈ
    AudioSource audio_gms;

    // ����� Ŭ�� ����
    public AudioClip ready;

    // ����� Ŭ�� ����
    public AudioClip start;

    public AudioClip enemyBGM;

    public AudioClip bossBGM;

    public AudioClip gameClear;

    public AudioClip gameOver;

    public GameObject player;

    public bool isDelay;
    public float delayTime = 3.0f;
    public float currentTime;

    //���� ��� UI
    public GameObject GameOverUI;
    public GameObject ClearUI;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }

        audio_gms = GetComponent<AudioSource>();
    }

    void Start()
    {
        // �ʱ� ���� ���´� �غ� ���·� �����Ѵ�.
        gState = GameState.Ready;

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();

        pms = GetComponent<PlayerMove_Slime>();

        //�������� ������Ʈ�� �˻�
        bms = GetComponent<BossMonster_Slime>();

        // ���� ���� �ڷ�ƾ �Լ��� �����Ѵ�.
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        // Ready...  ��� ������ ǥ���Ѵ�.
        stateLabel.text = "Ready...";
        audio_gms.PlayOneShot(ready);

        // Ready ������ ������ ��Ȳ������ ǥ���Ѵ�.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(2.0f);

        // Start! ��� ������ �����Ѵ�.
        stateLabel.text = "Start!";
        audio_gms.PlayOneShot(start);

        // 0.5�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(0.5f);

        // Start ������ �����.
        stateLabel.text = "";

        // ������ ���¸� �غ� ���¿��� ���� ���·� ��ȯ�Ѵ�.
        gState = GameState.Run;
        audio_gms.clip = enemyBGM;
        audio_gms.Play();
    }

    void Update()
    {
        UIPoint.text = (totalPoint).ToString();

        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ�
        if (gm.gState != GameState.Run)
        {
            return;
        }

        // ���� �÷��̾��� hp�� 0 ���Ϸ� ��������
        if (PlayerMove_Slime.playerHp_s <= 0)
        {
            if (EggHp.eggHp <= 0)
            {
                //�� ��ȯ
                SceneManager.LoadScene("BadEnding_2");
            }
            else
            {
                //audio.Stop();
                //audio.PlayOneShot(gameOver);

                player.GetComponent<PlayerMove_Slime>().Die();

                Enemy.enemyDeath = 0;

                //���� ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
                GameOverUI.SetActive(true);

                // ���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�.
                gState = GameState.GameOver;
            }
        }

        // ���� ������ hp�� 0 ���Ϸ� ��������
        else if (BossMonster_Slime.bossHp <= 0)
        {
            totalPoint += bossPoint;
            Board_PlayerMove.totalScore += totalPoint;

            Enemy.enemyDeath = 0;

            //Ŭ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
            ClearUI.SetActive(true);

            // ���� ���¸� ���� Ŭ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameClear;
        }

        if (isDelay)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= delayTime)
            {
                currentTime = 0;
                isDelay = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (EggHp.potionCount > 0)
            {
                if (isDelay == true || PlayerMove_Slime.playerHp_s == 10)
                {
                    return;
                }
                else
                {
                    // isDelay�� true�� ��ȯ �� ü�� ȸ��
                    isDelay = true;
                    EggHp.potionCount--;
                    Heal();
                }
            }
        }
    }

    void Heal()
    {
        PlayerMove_Slime.playerHp_s += 5;

        if (PlayerMove_Slime.playerHp_s > player.GetComponent<PlayerMove_Slime>().maxHp)
        {
            PlayerMove_Slime.playerHp_s = player.GetComponent<PlayerMove_Slime>().maxHp;
        }
    }
}