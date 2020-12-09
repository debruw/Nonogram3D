using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CubeManager cubeManager;
    public int live = 3;
    public Image[] LiveImages;

    #region UI Elements
    public GameObject WinPanel, LosePanel, InGamePanel;
    #endregion

    private void Update()
    {
        if (cubeManager.CubesWillBeDestroyed.Count == 0 && !cubeManager.isWrongCubeDestroyed)
        {
            cubeManager.ColorAllCubes();
            GameWin();
            Debug.Log("<color=green/>Game Win</color>");
        }
        else if (/*cubeManager.isWrongCubeDestroyed*/ live == 0)
        {
            GameLose();
        }
    }

    public void GameWin()
    {
        cubeManager.CloseAllCubes();
        WinPanel.SetActive(true);
        InGamePanel.SetActive(false);
        //Camera.main.transform.LookAt(cubeManager.transform,Vector3.up);
    }

    public void GameLose()
    {
        cubeManager.CloseAllCubes();
        LosePanel.SetActive(true);
        InGamePanel.SetActive(false);
    }

    public void LoseLive()
    {
        live--;
        switch (live)
        {
            case 2:
                LiveImages[2].enabled = false;
                break;
            case 1:
                LiveImages[1].enabled = false;
                LiveImages[2].enabled = false;
                break;
            case 0:
                LiveImages[0].enabled = false;
                LiveImages[1].enabled = false;
                LiveImages[2].enabled = false;
                break;
            default:
                break;
        }
    }
}
