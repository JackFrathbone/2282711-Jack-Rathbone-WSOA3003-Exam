using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public GameObject winUI, loseUI, pauseUI;
    public TextMeshProUGUI winUIGrade;

    public void PauseOn()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void PlayerWin()
    {
        winUI.SetActive(true);
        winUIGrade.text = GetComponent<Grade_Manager>().GiveGrade();
        Time.timeScale = 0;
    }
    public void PlayerLose()
    {
        loseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadScene(int i)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(i);
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

}
