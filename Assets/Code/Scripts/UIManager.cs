using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void GameStartButton()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void GameEndButton()
    {
        PlayerDataManager.instance.SavePlayerData();
        Application.Quit();
    }
}