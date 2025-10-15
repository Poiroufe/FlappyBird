using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }

}
