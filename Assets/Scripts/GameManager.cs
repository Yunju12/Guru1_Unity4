using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
    PlayerMove pm;

    // ���� ���� ������Ʈ ����
    BossMonster bm;

    // �̱���
    public static GameManager gm;

    // �ִϸ��̼� ����
    Animator ani;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
        // �ʱ� ���� ���´� �غ� ���·� �����Ѵ�.
        gState = GameState.Ready;

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();

        // ���� ���� �ڷ�ƾ �Լ��� �����Ѵ�.
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        // Ready...  ��� ������ ǥ���Ѵ�.
        stateLabel.text = "Ready...";

        // Ready ������ ������ ��Ȳ������ ǥ���Ѵ�.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(2.0f);

        // Start! ��� ������ �����Ѵ�.
        stateLabel.text = "Start!";

        // 0.5�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(0.5f);

        // Start ������ �����.
        stateLabel.text = "";

        // ������ ���¸� �غ� ���¿��� ���� ���·� ��ȯ�Ѵ�.
        gState = GameState.Run;
    }

    void Update()
    {
        // �÷��̾� ������Ʈ�� �˻�
        PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();

        // �������� ������Ʈ�� �˻�
        BossMonster bm = GameObject.Find("BossMonster").GetComponent<BossMonster>();

        // ���� �÷��̾��� hp�� 0 ���Ϸ� ��������
        if (PlayerMove.playerHp <= 0)
        {
            // ���� ���� ������ ����Ѵ�.
            stateLabel.text = "Game Over...";

            // ���� ���� ������ ������ ���������� �����Ѵ�.
            stateLabel.color = new Color32(255, 0, 0, 255);

            // ���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�.
            //gState = GameState.GameOver;
        }

        // ���� ������ hp�� 0 ���Ϸ� ��������
        else if (bm.bossHp <= 0)
        {
            // ���� ������ Ǯ���Ѵ�.
            stateLabel.text = "Clear!";

            // Ŭ���� ������ ������ ��������� �����Ѵ�.
            stateLabel.color = new Color32(255, 255, 0, 255);

            // ���� ���¸� ���� Ŭ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameClear;
        }
    }
}
