using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMainMenu : MonoBehaviour
{
    public void GameOver()
    {
        SceneManager.LoadSceneAsync(0);
    }

}

