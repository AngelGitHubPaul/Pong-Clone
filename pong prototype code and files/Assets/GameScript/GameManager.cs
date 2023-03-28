using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //timer UI
    public float timeRemaining;
    public Text timeText;
    public bool timerIsRunning = false;
    
    public GameObject ball;
    public Text Scoreboard;
    private Rigidbody2D ballrb;
    public float speed = 30;

    //for the score UI
    private static int player1Score = 0;
    private static int player2Score = 0;


    // Start is called before the first frame update
    void Start()
    {
        timeText.enabled = false;
        // Timer Mode time start
        if (GameMode.gameMode == "Timer Mode")
        {
            timeText.enabled = true;
            timerIsRunning = true;
            string minutes = Mathf.Floor(timeRemaining / 60).ToString("00");
            string seconds = (timeRemaining % 60).ToString("00");
            timeText.text = minutes + ":" + seconds;
        }

        ball = GameObject.Find("Ball");
        ballrb = ball.GetComponent<Rigidbody2D>();
        StartCoroutine(Pause());
    }

    // Update is called once per frame
    void Update()
    {
    //-------------------SCORE CODE--------------------
        if (ball.transform.position.x >= 69f)
        {
            player1Score++;
            ball.transform.position = new Vector3(0f, 0f, 0f);
            StartCoroutine(Pause());
        }

        if (ball.transform.position.x <= -69f)
        {
            player2Score++;
            ball.transform.position = new Vector3(0f, 0f, 0f);
            StartCoroutine(Pause());
        }
        Scoreboard.text = player1Score.ToString() + "                        " + player2Score.ToString();

        //-------------------TIMER MODE-----------------------
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                string minutes = Mathf.Floor(timeRemaining / 60).ToString("00");         
                string seconds = (timeRemaining % 60).ToString("00");
                if (seconds == "60")
                {
                    seconds = "59";
                }
                timeText.text = minutes + ":" + seconds;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                if (player1Score > player2Score)
                {
                    player1Score = 0;
                    player2Score = 0;
                    SceneManager.LoadScene("player1Win");
                }
                else if (player2Score > player1Score)
                {
                    player1Score = 0;
                    player2Score = 0;
                    SceneManager.LoadScene("player2Win");
                }else if(player1Score == player2Score)
                {
                    player1Score = 0;
                    player2Score = 0;
                    SceneManager.LoadScene("Draw");
                }

            }

        }

            //------------- BEST OF 7 MODE---------------
            if (GameMode.gameMode == "Best of 7" && player1Score == 7)
            {
                player1Score = 0;
                player2Score = 0;
                SceneManager.LoadScene("player1Win");
            }

            if (GameMode.gameMode == "Best of 7" && player2Score == 7)
            {
                player1Score = 0;
                 player2Score = 0;
                SceneManager.LoadScene("player2Win");
            }

    }

    //---------- ball delay before start-----------
    IEnumerator Pause()
    {
        ballrb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(2);
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        ballrb.velocity = new Vector2(speed * x, speed * y);
    }

}
