using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_UI : MonoBehaviour
{
    //�ɼ� �޴� UI ������Ʈ
    public GameObject optionUI;

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

    //�������� ���� ������(����)
    public void Clear()
    {
        //�� ��ȯ
        SceneManager.LoadScene("BoardGame");
    }

    //���� ���� �����(�ɼ�)
    public void Restart()
    {
        //�ð��� ������� ������
        Time.timeScale = 1.0f;

        //�ɼ� �޴� â�� ����
        optionUI.SetActive(false);

        //�� �ϳ� ����
        EggHp.eggHp--;

        //���� �� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //���� ���� �� ����
    public void GoNotice1()
    {
        //�� ��ȯ
        SceneManager.LoadScene("Notice1");
    }

    public void GoNotice2()
    {
        //�� ��ȯ
        SceneManager.LoadScene("Notice2");
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

    public void SlP()
    {
        //�� ��ȯ
        SceneManager.LoadScene("S1-Plat");
    }

    public void SlS()
    {
        //�� ��ȯ
        SceneManager.LoadScene("S1-Shooting");
    }

    public void S2P()
    {
        //�� ��ȯ
        SceneManager.LoadScene("S2-Plat");
    }

    public void S2S()
    {
        //�� ��ȯ
        SceneManager.LoadScene("S2-Shooting");
    }

    public void S3P()
    {
        //�� ��ȯ
        SceneManager.LoadScene("S3-Plat");
    }

    public void S3S()
    {
        //�� ��ȯ
        SceneManager.LoadScene("S3-Shooting");
    }
}
