using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public int lives, score;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private Spawner[] spawners;
    private int level;

    void Start()
    {
        Load();
    }

    public void LoseLife()
    {
        if(lives > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if(score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        StartNewGame();
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        lives--;
        Instantiate(player.gameObject, playerSpawnPoint.position, Quaternion.identity);
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    private void CheckForLevelCompletion()
    {
        if(!FindObjectOfType<Enemy>())
        {
            foreach(Spawner spawner in spawners)
            {
                if(!spawner.completed)
                {
                    return;
                }
                CompleteLevel();
            }
        }
    }

    private void CompleteLevel()
    {

        Save();

        level++;
        if(SceneManager.GetSceneByBuildIndex(level) != null)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.Log("game over");
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Lives", lives);
    }

    private void Load()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        lives = PlayerPrefs.GetInt("Lives", 3);
    }

    void StartNewGame()
    {
        level = 0;
        SceneManager.LoadScene(level);
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Lives");
    }
}
