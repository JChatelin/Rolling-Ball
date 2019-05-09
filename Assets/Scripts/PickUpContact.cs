using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpContact : MonoBehaviour
{
    private GameObject gameControllerObject;
    private GameController gameController;

    public int score;
    // I created a static scoreValue to simplify the code. If I just directly use the score, I will have to instantiate this class and I dont want to do that.
    public static int scoreValue;

    private static bool timesUp;

    private void Start()
    {
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        scoreValue = score;
        timesUp = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Remove the current GameObject from the scene when it collide with the player then thincrement the player's score
        if (!timesUp)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                gameController.AddScore(scoreValue);
            }
        }
    }

    public static void TimesUp()
    {
        timesUp = true;
    }
}
