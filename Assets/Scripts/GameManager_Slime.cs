using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Slime : MonoBehaviour
{
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
        // ���� �÷��̾��� hp�� 0 ���Ϸ� ��������
        if (PlayerMove_Slime.playerHp <= 0)
        {
            //audio.Stop();
            //audio.PlayOneShot(gameOver);

            player.GetComponent<PlayerMove>().Die();

            // ���� ���� ������ ����Ѵ�.
            stateLabel.text = "Game Over...";

            // ���� ���� ������ ������ ���������� �����Ѵ�.
            stateLabel.color = new Color32(255, 0, 0, 255);

            // ���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameOver;
        }

        // ���� ������ hp�� 0 ���Ϸ� ��������
        else if (BossMonster_Slime.bossHp <= 0)
        {
            //audio.Stop();
            //audio.PlayOneShot(gameClear);

            // ���� ������ Ǯ���Ѵ�.
            stateLabel.text = "Clear!";

            // Ŭ���� ������ ������ ��������� �����Ѵ�.
            stateLabel.color = new Color32(255, 255, 0, 255);

            // ���� ���¸� ���� Ŭ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameClear;
        }
    }
}