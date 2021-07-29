using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_UI : MonoBehaviour
{
    //�ɼ� �޴� UI ������Ʈ
    public GameObject optionUI;

    //���� ���� UI ������Ʈ
    public GameObject startUI;

    //Ŭ���� ���� �ɼ� UI ������Ʈ
    public GameObject reOptionUI;

    //���(���)
    //public static int eggHp = 5;

    /*private void Awake()
    {
        if(gmU == null)
        {
            gmU = this;
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        //�÷��̾� ������Ʈ�� �˻�
        //player = GameObject.Find("Player");

        //playerM = Player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //���� �÷��̾��� hp�� 0 ���Ϸ� �������ٸ�
        /*if(PlayerM.hp <= 0)
        {
            //���� ���� ������ ����Ѵ�
            stateLabel.text = "Lose";

            //���� ���� ������ ������ ���������� �����Ѵ�
            stateLabel.color = new Color32(255, 0, 0, 255);

            //���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�
            gState = GameState.Lose;
        }*/
    }

    //�ɼ� �޴� �ѱ�
    public void OpenOptionWindow()
    {
        //�ð��� �����
        Time.timeScale = 0;

        //�ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
        optionUI.SetActive(true);
    }

    //�ɼ� �޴� ����
    public void CloseOptionWindow()
    {
        //�ð��� ������� ������
        Time.timeScale = 1.0f;

        //�ɼ� �޴� â�� ����
        optionUI.SetActive(false);
    }

    //�������� ���� ������
    public void Lose()
    {
        //�� �ϳ� ����
        EggHp.eggHp--;

        //�� ��ȯ
        SceneManager.LoadScene("BoardGame");
    }

    //���� ���� �����(�ɼ�)
    public void Restart()
    {
        //�� �ϳ� ����
        EggHp.eggHp--;

        //���� �� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //��ü ���� ����
    public void StartGame()
    {
        //�� ��ȯ
        SceneManager.LoadScene("BoardGame");
    }

    //��ü ���� ����
    public void Quit()
    {
        //���� ����
        Application.Quit();
    }

    //���� ���� �����(����)
    public void RestartGame()
    {
        //�� �ϳ� ����
        EggHp.eggHp--;

        //���� �� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //��ü ���� �����
    public void GameReStart()
    {
        //�� ��ȯ
        SceneManager.LoadScene("Prologue");
    }
}
