using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
public class LogicScript : MonoBehaviour
{
    // Inisiasi tampilan skor 
    public int playerScore;
    public Text scoreText;
    // Refrensi gameOverScreen
    public GameObject gameOverScreen;

    // Saat burung berhasil melewati pipa, sistem akan memberikan 1 poin yang ditampilkan pada layar ke pada player
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }
    // Ketika fungsi restartGame() dijalankan, maka game akan mengulang kembali permainan serta progress skor player dari awal
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Ketika fungsi gameOver(), dijalankan, akan menampilkan tampilan game over yang mengindikasikan game telah usai
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

}
