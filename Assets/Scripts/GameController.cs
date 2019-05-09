using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;
    public Text timerText;
    public Text restartText;
    public Text quitText;
    public Text levelCompletedText;
    public Text gameCompletedText;
    public Text levelDescriptionText;
    public Text levelText;

    public int timeLimitMinutes;
    public int timeLimitSeconds;

    private int score;
    private int pickUpNumber;
    private bool gameOver;
    private bool restart;
    private bool quit;
    private bool gameCompleted;

    private string ready;
    private string go;

    // Delay for the level description's messages
    public float waitBeforeLoadScene;
    public float waitNextText;
    public float waitForStart;
    public float waitForLevelDisplay;

    private GameObject timerObject;
    private GameTimer timer;
    private GameObject playerObject;
    private PlayerController playerController;
    private GameObject levelCompletedObject;


    // Start is called before the first frame update
    void Start()
    {
        timerObject = GameObject.FindGameObjectWithTag("GameTimer");
        if (timerObject != null)
        {
            timer = timerObject.GetComponent<GameTimer>();
        }
        if (timer == null)
        {
            Debug.Log("Cannot find 'GameTimer' script");
        }
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
        score = 0;
        // get the number of the cubes on the scene
        pickUpNumber = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        scoreText.text = "Score : " + score.ToString();
        gameOverText.text = "";
        restartText.text = "";
        quitText.text = "";
        levelCompletedText.text = "";
        gameCompletedText.text = "";
        levelDescriptionText.text = "";
        ready = "Ready.";
        go = "Go.";
        timer.SetTimerText(timerText);
        gameOver = false;
        restart = false;
        quit = false;
        gameCompleted = false;
        // Display the current level at the beginning
        StartCoroutine("DisplayLevel");
        // Display the level description messages before the game start
        StartCoroutine("DisplayLevelDescription");
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelCompleted())
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene.Contains("3"))
            {
                gameCompletedText.text = "You Completed the game!";
                GameCompleted();
                QuitOrRestart();
            } else if (currentScene.Contains("2"))
            {
                StartCoroutine("LoadNextScene", "RollingBallLvl3");
            } else
            {
                StartCoroutine("LoadNextScene", "RollingBallLvl2");
            }
        }
        if (gameOver)
        {
            QuitOrRestart();
        }
    }

    private IEnumerator DisplayLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("1"))
        {
            levelText.text = "Level 1";
            yield return new WaitForSeconds(waitForLevelDisplay);
            levelText.text = "";
        } else if(sceneName.Contains("2"))
        {
            levelText.text = "Level 2";
            yield return new WaitForSeconds(waitForLevelDisplay);
            levelText.text = "";
        } else
        { 
            levelText.text = "Level 3";
            yield return new WaitForSeconds(waitForLevelDisplay);
            levelText.text = "";
        }
    }

    // Delay the the loading of the next scene
    IEnumerator LoadNextScene(string sceneName)
    {
        yield return new WaitForSeconds(waitBeforeLoadScene);
        SceneManager.LoadScene(sceneName);   
    }

    IEnumerator DisplayLevelDescription()
    {
        yield return new WaitForSeconds(waitNextText);
        if (timeLimitMinutes == 0)
        {
            levelDescriptionText.text = "Your have " + timeLimitSeconds + " seconds to pick all the cubes on the ground.";
            yield return new WaitForSeconds(waitNextText);
            levelDescriptionText.text = ready;
            yield return new WaitForSeconds(waitNextText);
            levelDescriptionText.text = go;
            yield return new WaitForSeconds(waitForStart);
        } else
        {
            if(timeLimitSeconds == 0)
            {
                levelDescriptionText.text = "Your have " + timeLimitMinutes + " minute to pick all the cubes on the ground.";
                yield return new WaitForSeconds(waitNextText);
                levelDescriptionText.text = ready;
                yield return new WaitForSeconds(waitNextText);
                levelDescriptionText.text = go;
                yield return new WaitForSeconds(waitForStart);
            } else
            {
                levelDescriptionText.text = "Your have " + timeLimitMinutes + " minute and " + timeLimitSeconds + " seconds to pick all the cubes on the ground.";
                yield return new WaitForSeconds(waitNextText);
                levelDescriptionText.text = ready;
                yield return new WaitForSeconds(waitNextText);
                levelDescriptionText.text = go;
                yield return new WaitForSeconds(waitForStart);

            }
        }
        levelDescriptionText.text = "";
        timer.StartTimer();
        playerController.AllowMotion = true;
    }

    private void QuitOrRestart()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (gameCompleted)
                {
                    SceneManager.LoadScene("RollingBallLvl1");
                } else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
        if (quit)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    private void GameCompleted()
    {
        gameCompleted = true;
    }
    
    private bool LevelCompleted ()
    {
        // If the time limit has mintues and seconds or only seconds
        if (timer.Minutes >= timeLimitMinutes)
        {
            // Check if the timer's second are lower than the limit
            if (timer.Seconds < timeLimitSeconds)
            {
                if (score >= (pickUpNumber * PickUpContact.scoreValue))
                {
                    StartCoroutine("DisplayLevelCompletedText");
                    return true;
                }
            } else
            {
                if (score < (pickUpNumber * PickUpContact.scoreValue))
                {
                    timer.StopTimer();
                    GameOver();
                    Restart();
                    Quit();
                }
            }
        } else   // If the limit only have minutes and no seconds then see code below.
        {
            if (score >= (pickUpNumber * PickUpContact.scoreValue))
            {
                StartCoroutine("DisplayLevelCompletedText");
                return true;
            }
        }
        return false;
    }


    // Messages displayed when the player complete a level
    IEnumerator DisplayLevelCompletedText()
    {
        levelCompletedText.text = "Level Completed.";
        timer.StopTimer();
        yield return new WaitForSeconds(waitNextText);
        levelCompletedObject = GameObject.FindGameObjectWithTag("Level Completed Text");
        if (levelCompletedObject != null)
        {
            levelCompletedObject.SetActive(false);
        }
 
        if (SceneManager.GetActiveScene().name.Contains("3"))
        {
            Restart();
            Quit();
        }
        yield return new WaitForSeconds(waitNextText);
    }
    
    private void Restart()
    {
        restartText.text = "Press 'R' to restart";
        restart = true;
    }

    private void Quit()
    {
        quitText.text = "Press 'Esc' to quit";
        quit = true;
    }

    private void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
        PickUpContact.TimesUp();
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = "Score : " + score.ToString();
    }

}
