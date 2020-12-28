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
    public bool isGameEnd;
    public int currentLevel = 1;
    int MaxLevelNumber = 20;
    public bool isGameStarted;
    public GameObject Confetties;
    public GameObject Particle;

    #region UI Elements
    public GameObject WinPanel, LosePanel, InGamePanel;
    public Button VibrationButton, TapToStartButton;
    public Sprite on, off;
    public Text LevelText;
    public GameObject Tutorial1, Tutorial2, Tutorial3;
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
        if (PlayerPrefs.GetInt("FromMenu") == 1)
        {
            PlayerPrefs.SetInt("FromMenu", 0);
        }
        else
        {
            isGameStarted = true;
            switch (currentLevel)
            {
                case 2:
                    Tutorial2.SetActive(true);
                    break;
                case 3:
                    Tutorial3.SetActive(true);
                    break;
                default:
                    break;
            }
            TapToStartButton.gameObject.SetActive(false);
        }

    }

    float counter;
    public GameObject ContinueTutorial;
    private void Update()
    {
        if (isGameEnd || !isGameStarted)
        {
            return;
        }
        if (cubeManager.CubesWillBeDestroyed.Count == 0)
        {
            isGameEnd = true;
            cubeManager.ColorAllCubes();
            switch (currentLevel)
            {
                case 1:
                    Tutorial1.SetActive(false);
                    break;
                case 2:
                    Tutorial2.SetActive(false);
                    break;
                case 3:
                    Tutorial3.SetActive(false);
                    break;
                default:
                    break;
            }
            StartCoroutine(WaitAndGameWin());
            Debug.Log("<color=green/>Game Win</color>");
        }
        else if (live <= 0)
        {
            isGameEnd = true;
            switch (currentLevel)
            {
                case 1:
                    Tutorial1.SetActive(false);
                    break;
                case 2:
                    Tutorial2.SetActive(false);
                    break;
                case 3:
                    Tutorial3.SetActive(false);
                    break;
                default:
                    break;
            }
            StartCoroutine(WaitAndGameLose());
        }
        if (Input.GetMouseButton(0))
        {
            if (ContinueTutorial.activeSelf)
            {
                counter = 0;
                ContinueTutorial.SetActive(false);
            }
        }
        else
        {
            counter += Time.deltaTime;
            if (counter >= 5f)
            {
                if (!ContinueTutorial.activeSelf)
                {
                    ContinueTutorial.SetActive(true); 
                }
            }
        }
    }

    IEnumerator WaitAndGameWin()
    {
        Confetties.SetActive(true);
        cubeManager.PlaceFinalObject();
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.playSound(SoundManager.GameSounds.Win);
        yield return new WaitForSeconds(1);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
        currentLevel++;
        if (Particle != null)
        {
            Particle.SetActive(true);
        }
        PlayerPrefs.SetInt("LevelId", currentLevel);
        cubeManager.CloseAllCubeScripts();
        WinPanel.SetActive(true);
        InGamePanel.SetActive(false);
    }

    IEnumerator WaitAndGameLose()
    {
        //SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.playSound(SoundManager.GameSounds.Lose);
        yield return new WaitForSeconds(.5f);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
        cubeManager.CloseAllCubeScripts();
        LosePanel.SetActive(true);
        //InGamePanel.SetActive(false);
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
            int rand = Random.Range(1, MaxLevelNumber);
            if (rand == PlayerPrefs.GetInt("LastRandomLevel"))
            {
                rand = Random.Range(1, MaxLevelNumber);
            }
            else
            {
                PlayerPrefs.SetInt("LastRandomLevel", rand);
            }
            SceneManager.LoadScene("Level" + rand);
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

    public void TapToStartButtonClick()
    {
        isGameStarted = true;
        switch (currentLevel)
        {
            case 1:
                Tutorial1.SetActive(true);
                break;
            case 2:
                Tutorial2.SetActive(true);
                break;
            case 3:
                Tutorial3.SetActive(true);
                break;
            default:
                break;
        }
    }
}
