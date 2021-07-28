using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Vector3 toPosZ { get { return new Vector3((float)posx, 1.2f, -0.1f); } }

    //�̵� ��ǥ ����
    Vector3 S1F { get { return new Vector3(6.69f, 1.2f, 0); } }
    public GameObject buttonS1F;

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

    public enum PlayerState
    {
        Idle,
        Move
    }

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        rigid = GetComponent<Rigidbody2D>();

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();

        gm = GetComponent<GameManager>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // * �̵�
        // 1. �̵� ����(�¿�)�� �����Ѵ�.
        float h = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(h, 0, 0);
        dir.Normalize();

        // 2. �̵� ����(�¿�)���� �÷��̾ �̵���Ų��.
        transform.position += (dir * moveSpeed * Time.deltaTime);

        // 3. �����̸� IdleToMove, �������� ���߸� MoveToIdle �� �����Ѵ�.
        if (Input.GetButtonDown("Horizontal"))
        {
            ani.SetTrigger("IdleToMove");
        }
        else
        {
            ani.SetTrigger("MoveToIdle");
        }
    }

    public class ExampleClass : MonoBehaviour
    {
        public Collider coll;
        void Start()
        {
            coll = GetComponent<Collider>();
            coll.isTrigger = true;
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody)
                other.attachedRigidbody.useGravity = false;

        }
    }

    //�ֻ��� ������
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
            StartCoroutine(MoveTo(player, toPosZ));
            posx += 2.23;
            StartCoroutine(MoveTo(player, toPosX));
        }
        else if (ran == 2)
        {
            dice2.SetActive(true);
            StartCoroutine(MoveTo(player, toPosZ));
            posx += 4.46;
            StartCoroutine(MoveTo(player, toPosX));
        }
        else if (ran == 3)
        {
            dice3.SetActive(true);
            StartCoroutine(MoveTo(player, toPosZ));
            posx += 6.69;
            StartCoroutine(MoveTo(player, toPosX));
            print(player.gameObject.transform.position);
        }
        else if (ran == 4)
        {
            dice4.SetActive(true);
            StartCoroutine(MoveTo(player, toPosZ));
            posx += 8.92;
            StartCoroutine(MoveTo(player, toPosX));
        }
        else if (ran == 5)
        {
            dice5.SetActive(true);
            StartCoroutine(MoveTo(player, toPosZ));
            posx += 11.15;
            StartCoroutine(MoveTo(player, toPosX));
        }
        else if (ran == 6)
        {
            dice6.SetActive(true);
            StartCoroutine(MoveTo(player, toPosZ));
            posx += 13.38;
            StartCoroutine(MoveTo(player, toPosX));
        }

        if (player.gameObject.transform.position == S1F)
        {
            buttonS1F.SetActive(true);
        }
    }

    IEnumerator MoveTo(GameObject player, Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = player.transform.position;
        while(true)
        {
            count += Time.deltaTime;
            player.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if(count >= 1)
            {
                player.transform.position = toPos; ;
                break;
            }
            yield return null;
        }
    }
}
