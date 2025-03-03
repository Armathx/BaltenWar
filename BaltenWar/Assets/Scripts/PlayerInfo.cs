using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    private int nbLives = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void TakeDamage()
    {
        if (--nbLives <= 0)
        {
            Debug.Log("Lose");
            SceneManager.LoadScene(0);
        }
        Debug.Log("Number of lives remaining : " + nbLives);
    }

    public void Win()
    {
        Debug.Log("Win");
    }
}