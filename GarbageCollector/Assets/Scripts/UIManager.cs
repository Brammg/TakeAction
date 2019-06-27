using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject gameMenu;
    public bool menuActive;
    private bool settingScore;
    private bool settingTimer;

    public int frontScore;

    public Text scoreText;
    public Text timerText;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        menuActive = false;
        gameMenu.SetActive(false);
        frontScore = 0;
        GameManager.instance.countDownTimer = 63;
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameMenuActive == false)
        {
            CheckScore();
            CheckTimer();
        }

        CheckMenu();

        if(GameManager.instance.gameOver == true)
        {
            gameOver.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void CheckMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenu();
        }
    }

    public void SetMenu()
    {
        if (menuActive == false)
        {
            gameMenu.SetActive(true);
            menuActive = true;
        }
        else if (menuActive == true)
        {
            gameMenu.SetActive(false);
            menuActive = false;
        }

        GameManager.instance.gameMenuActive = menuActive;
    }

    private void CheckScore()
    {
        if (GameManager.instance.score > frontScore)
        {
            if (settingScore == false)
            {
                settingScore = true;
                StartCoroutine("SetScore");
            }
        }

        scoreText.text = "Score: " + frontScore.ToString();
    }

    private IEnumerator SetScore()
    {
        yield return new WaitForSeconds(0.15f);
        frontScore++;
        settingScore = false;
    }

    private void CheckTimer()
    {
        if (settingTimer == false)
        {
            settingTimer = true;
            StartCoroutine("SetTimer");
        }

        timerText.text = GameManager.instance.countDownTimer.ToString();
    }

    private IEnumerator SetTimer()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.countDownTimer--;
        settingTimer = false;
    }

    public void RetryButtonActivated()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MenuButtonActivated()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
