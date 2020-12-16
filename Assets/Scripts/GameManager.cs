using System.Collections;
using System.Collections.Generic;
using TapticPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CubeManager cubeManager;
    public int live = 3;
    public Image[] LiveImages;
    bool isGameEnd;
    public int currentLevel = 1;
    int MaxLevelNumber = 8;

    #region UI Elements
    public GameObject WinPanel, LosePanel, InGamePanel;
    public Button VibrationButton;
    public Sprite on, off;
    public Text LevelText;
    #endregion

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("VIBRATION"))
        {
            PlayerPrefs.SetInt("VIBRATION", 1);
            VibrationButton.GetComponent<Image>().sprite = on;
        }
        else
        {
            if (PlayerPrefs.GetInt("VIBRATION") == 1)
            {
                VibrationButton.GetComponent<Image>().sprite = on;
            }
            else
            {
                VibrationButton.GetComponent<Image>().sprite = off;
            }
        }
        currentLevel = PlayerPrefs.GetInt("LevelId");
        LevelText.text = "Level " + currentLevel;
    }

    private void Update()
    {
        if (isGameEnd)
        {
            return;
        }
        if (cubeManager.CubesWillBeDestroyed.Count == 0)
        {
            isGameEnd = true;
            cubeManager.ColorAllCubes();
            StartCoroutine(WaitAndGameWin());
            Debug.Log("<color=green/>Game Win</color>");
        }
        else if (live == 0)
        {
            isGameEnd = true;
            GameLose();
        }
    }

    IEnumerator WaitAndGameWin()
    {
        cubeManager.PlaceFinalObject();
        yield return new WaitForSeconds(1);
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.playSound(SoundManager.GameSounds.Win);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
        currentLevel++;
        PlayerPrefs.SetInt("LevelId", currentLevel);
        cubeManager.CloseAllCubes();
        WinPanel.SetActive(true);
        InGamePanel.SetActive(false);
    }

    public void GameLose()
    {
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.playSound(SoundManager.GameSounds.Lose);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
        cubeManager.CloseAllCubes();
        LosePanel.SetActive(true);
        InGamePanel.SetActive(false);
    }

    public void LoseLive()
    {
        live--;
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.playSound(SoundManager.GameSounds.LiveLose);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
        switch (live)
        {
            case 2:
                LiveImages[0].gameObject.SetActive(true);
                break;
            case 1:
                LiveImages[1].gameObject.SetActive(true);
                break;
            case 0:
                LiveImages[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void TapToNextButtonClick()
    {
        if (currentLevel > MaxLevelNumber)
        {
            currentLevel = 1;
            PlayerPrefs.SetInt("LevelId", currentLevel);
            SceneManager.LoadScene("Level" + currentLevel);
            //int rand = Random.Range(1, MaxLevelNumber);
            //if (rand == PlayerPrefs.GetInt("LastRandomLevel"))
            //{
            //    rand = Random.Range(1, MaxLevelNumber);
            //}
            //else
            //{
            //    PlayerPrefs.SetInt("LastRandomLevel", rand);
            //}
            //SceneManager.LoadScene("Level" + rand);
        }
        else
        {
            SceneManager.LoadScene("Level" + currentLevel);
        }
    }

    public void TapToTryAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VibrateButtonClick()
    {
        if (PlayerPrefs.GetInt("VIBRATION").Equals(1))
        {//Vibration is on
            PlayerPrefs.SetInt("VIBRATION", 0);
            VibrationButton.GetComponent<Image>().sprite = off;
        }
        else
        {//Vibration is off
            PlayerPrefs.SetInt("VIBRATION", 1);
            VibrationButton.GetComponent<Image>().sprite = on;
        }

        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
    }
}
