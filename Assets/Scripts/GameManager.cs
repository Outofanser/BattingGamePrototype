using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject playerBat;
    [SerializeField] private int swingsLeft = 3;
    [SerializeField] private float ballSpeed = 40f;
    [SerializeField] private Vector3 ballSpawnPosition;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ballsText;
    [SerializeField] private GameObject gameOverScreen;
    public int score;
    private bool isBallInPlay = false;
    private bool isBallLaunching = false;
    private bool isFirstRound = false;

    private int gemsNum = 3;
    private IEnumerator waitToFire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballSpawnPosition = gameObject.transform.position;
        isFirstRound = true;
        isBallInPlay = false;
        isBallLaunching = false;
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = "Score: " + score;
        ballsText.text = "Balls: " + swingsLeft;

        if (swingsLeft < 0)
        {
            return; // End Game
        }

        CheckBallInPlay();




    }

    void StartRound()
    {
        if (swingsLeft <= 0)
        {
            gameOverScreen.gameObject.SetActive(true);
            return;

        }
        gemsNum = 2 + score / 10;
        if (isFirstRound)
        {
            gemsNum += 1;
            isFirstRound = false;
        }


        for (int i = 0; i < gemsNum; i++)
        {
            GameObject pooledGems = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledGems != null)
            {
                pooledGems.SetActive(true); // activate it
                pooledGems.transform.position = GenerateRandomPosition(); // position it
            }
        }

        playerBat.GetComponent<BatController>().Reset();
        waitToFire = WaitToFire(1f);
        isBallLaunching = true;
        StartCoroutine(waitToFire);

    }

    IEnumerator WaitToFire(float time)
    {
        yield return new WaitForSeconds(time);
        isBallInPlay = true;
        isBallLaunching = false;
        LaunchBall();
    }

    void LaunchBall()
    {
        swingsLeft--;
        ballPrefab.GetComponent<BallLaunch>().ballSpeed = ballSpeed;
        Instantiate(ballPrefab, ballSpawnPosition, ballPrefab.transform.rotation);
    }

    void CheckBallInPlay()
    {
        if (!isBallInPlay && !isBallLaunching)
        {
            StartRound();
            return;
        }

        GameObject[] ballsInPlay = GameObject.FindGameObjectsWithTag("Ball");

        //Debug.Log("ballsInPlay: " + ballsInPlay.Length);

        foreach (GameObject ball in ballsInPlay)
        {
            if (ball.transform.position.y < -10f)
            {
                Destroy(ball);
            }
        }
        if (ballsInPlay.Length == 0 && isBallInPlay)
        {
            isBallInPlay = false;
        }
    }

    Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(-0.3f, 2.5f), Random.Range(8f, 10f), 12f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
