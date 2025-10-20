using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject GameOverScreen;
    public GameObject LeaderboardScreen;

    public TMP_Text leaderboardText;
    private const int maxScores = 5;

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore = playerScore + 1;
        scoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public void ShowLeaderboard()
    {
        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(false);
        }

        if (LeaderboardScreen != null)
        {
            LeaderboardScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("LeaderboardScreen reference is missing!");
        }
    }

    public void ReturnToGameOver()
    {
        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }

        if (LeaderboardScreen != null)
        {
            LeaderboardScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameOverScreen reference is missing!");
        }
    }

    public void gameOver()
    {
        GameOverScreen.SetActive(true);
        SaveScore(playerScore);
        DisplayLeaderboard();
    }

    // --- LEADERBOARD LOGIC ---

    void SaveScore(int newScore)
    {
        // Récupère la liste actuelle
        List<int> scores = LoadScores();

        // Ajoute le score actuel
        scores.Add(newScore);

        // Trie les scores (du plus grand au plus petit)
        scores.Sort();      // trie du plus petit au plus grand
        scores.Reverse();   // inverse la liste  du plus grand au plus petit

        // Ne garde que les 5 meilleurs
        if (scores.Count > maxScores)
            scores = scores.GetRange(0, maxScores);

        // Sauvegarde dans PlayerPrefs
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt("Score" + i, scores[i]);
        }

        // En cas de suppression d'anciens scores
        PlayerPrefs.SetInt("ScoreCount", scores.Count);
        PlayerPrefs.Save();
    }

    List<int> LoadScores()
    {
        List<int> scores = new List<int>();
        int count = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < count; i++)
        {
            scores.Add(PlayerPrefs.GetInt("Score" + i, 0));
        }

        return scores;
    }

    void DisplayLeaderboard()
    {
        if (leaderboardText == null) return;

        List<int> scores = LoadScores();
        leaderboardText.text = " Top Scores \n";

        for (int i = 0; i < scores.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {scores[i]}\n";
        }
    }

}
