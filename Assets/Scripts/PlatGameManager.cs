using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatGameManager : MonoBehaviour
{
    // ���� ��ü ����
    public int totalPoint;

    // �ش� �������� ����
    public int stagePoint;

    // �������� �ε���
    public int stageIndex;

    // �ִ� ü�� ����
    public int maxHp = 10;

    // platplayer HP
    public int Hp = 10;

    // �����̴� ��
    public Slider hpSlider;

    // player ����
    public PlatPlayerMove PlatPlayer;

    //�������� 1,2 �迭
    public GameObject[] Stages;

    // ��ź ������Ʈ
    public GameObject bomb;

    // ��ź ��� bool ����
    bool usingBomb;

    // UI : ���� ��, ���� ����Ʈ ���
    public Text UIPoint;
    public Text UIMAP;

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

    // �ִϸ��̼� ����
    Animator anim;


    // ����� �ҽ� ������Ʈ
    AudioSource audioSource;

    // ����� Ŭ�� ����
    public AudioClip ready;
    public AudioClip start;
    public AudioClip gameClear;
    public AudioClip gameOver;

    // UI �ؽ�Ʈ ����
    public Text stateLabel;

    //���� ��� UI
    public GameObject GameOverUI;
    public GameObject ClearUI;

    void Awake()
    {      
        audioSource = GetComponent<AudioSource>();
              
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // ü�� ���� �ʱ�ȭ
        Hp = maxHp;

        bomb.GetComponent<Rigidbody2D>().isKinematic = true;

        // ���� ���� �ڷ�ƾ �Լ��� �����Ѵ�.
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        // �ʱ� ���� ���´� �غ� ���·� �����Ѵ�.
        gState = GameState.Ready;
        
        // Ready...  ��� ������ ǥ���Ѵ�.
        stateLabel.text = "Ready...";
        audioSource.clip = ready;
        audioSource.Play();

        // Ready ������ ������ ��Ȳ������ ǥ���Ѵ�.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(2.0f);

        // Start! ��� ������ �����Ѵ�.
        stateLabel.text = "Start!";
        audioSource.clip = start;
        audioSource.Play();

        // 0.5�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(0.5f);

        // Start ������ �����.
        stateLabel.text = "";

        // ������ ���¸� �غ� ���¿��� ���� ���·� ��ȯ�Ѵ�.
        gState = GameState.Run;
    }


    void Update()
    {  
        // �����̴� ü�� ���� ����
        hpSlider.value = (float)Hp / (float)maxHp;

        // ��Ż ����Ʈ ȭ�� ǥ��
        UIPoint.text = (totalPoint + stagePoint).ToString();

        // S ��ư�� ������ ��ź ���
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (EggHp.bombCount == 0)
            {
                return;
            }
            else
            {
                bomb.SetActive(true);
                usingBomb = true;
            }
        }

        if (usingBomb == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                bomb.GetComponent<Rigidbody2D>().isKinematic = false;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        Vector3 speed = new Vector3(300, 300, 0);
        bomb.GetComponent<Rigidbody2D>().AddForce(speed);
    }

    // �� ��������
    public void NextStage()
    {
        // ��������(Plat���� �� ����) ���� : stageIndex�� ���� �������� Ȱ��ȭ ���� ����
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++; // �������� �ε��� �ø�
            Stages[stageIndex].SetActive(true);
            PlatPlayerReposition(); // player ��ġ ���������� �̵�

            UIMAP.text = "MAP" + (stageIndex + 1);
        }

        else  // �������� �÷��� ���� ��� (Ŭ����)
        {
            Board_PlayerMove.totalScore += (totalPoint + stagePoint);

            Time.timeScale = 0; // ���ֽ� �ð� ����

            Debug.Log("�������� �÷��� Ŭ����");

            //Ŭ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
            ClearUI.SetActive(true);

            // ���� ���¸� ���� Ŭ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameClear;
            audioSource.clip = gameClear;
            audioSource.Play();
        }

        // ��ü ������ �� ������������ ���� ���� �߰�
        totalPoint += stagePoint;
        stagePoint = 0; // ���ο� �������������� ���� 0���� �ʱ�ȭ
    }

    // Hp ������ ���
    public void HpDown()
    {
        // Hp 1�̻��� ���, Hp -1��Ŵ
        if (Hp >= 1)
            Hp--;

        // Hp 0�� ���
        else if(Hp == 0)
        {
            // Player Die ȣ��
            PlatPlayer.OnDie();

            // ��� �α� ���
            Debug.Log("�׾����ϴ�!");

            //���� ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
            GameOverUI.SetActive(true);

            // ���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameOver;
            audioSource.clip = gameOver;
            audioSource.Play();
        }

    }

    // ���Ͻ� Hp -1��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Hp�� 1�̻��� ���
            if (Hp > 1)
            {
                // ���Ͻ� player ��ġ ����
                PlatPlayerReposition();
            }

            // ü�� 1���Ҵµ� ������ ��� Hp 1���� �� ����ġ �ǵ����� �������� ����
            HpDown();
        }
    }

    // ���Ͻ� player ��ġ ����
    void PlatPlayerReposition()
    {
        PlatPlayer.transform.position = new Vector3(0, 0, 0); // (0,0,0) ��ġ�� �̵�
        PlatPlayer.VelocityZero(); // ���ϼӵ� 0����
    }

}
