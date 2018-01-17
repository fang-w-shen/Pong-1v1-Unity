using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public Rigidbody ball;
    public Rigidbody player1Paddle;
    public Rigidbody player2Paddle;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public GameObject restartText;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject outOfBoundsPanel;


    private bool gameOver = false;
    private bool restart = false;
    private bool outOfBounds = false;
    private float height = 4;

    private float velocity;
    private float speed = 6f;
    private int player1Score = 0;
    private int player2Score = 0;
    private float originalXSpeed = 0;
    private float originalYSpeed = 0;
    void Start()
    {
        StartCoroutine(IncreaseSpeed());

    }

    void Update()
    {
        player1ScoreText.text = "P-1: " + player1Score;
        player2ScoreText.text = "P-2: " + player2Score;

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                speed = 6f;
                player1Score = 0;
                player2Score = 0;
                EndGame();
                PlayBall();
                gameOver = false;
                restart = false;
            }
        }
        // CHECKING POSITION OF BALL AND PADDLE1 TO SEE IF BALL CAN BE HELD
        if ((player1Paddle.position.x-ball.position.x<0.45) && ((player1Paddle.position.y - ball.position.y) > -1.5) && ((player1Paddle.position.y - ball.position.y) < 1.5)) {
            if (originalXSpeed == 0 && originalYSpeed == 0)
            {
                originalXSpeed = ball.velocity.x;
                originalYSpeed = ball.velocity.y;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ball.velocity = new Vector3(0, player1Paddle.velocity.y, 0);
                ball.position = new Vector3
                (
                    ball.position.x,
                    Mathf.Clamp(ball.position.y, -height, height),
                    0.0f
                );
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                ball.velocity = new Vector3(-originalXSpeed, originalYSpeed, 0);

                originalXSpeed = 0;
                originalYSpeed = 0;
            }
        }
        // CHECKING POSITION OF BALL AND PADDLE2 TO SEE IF BALL CAN BE HELD
        if ((player2Paddle.position.x - ball.position.x > -0.45) && ((player2Paddle.position.y - ball.position.y) > -1.5) && ((player2Paddle.position.y - ball.position.y) < 1.5))
        {
            if (originalXSpeed == 0 && originalYSpeed == 0)
            {
                originalXSpeed = ball.velocity.x;
                originalYSpeed = ball.velocity.y;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ball.velocity = new Vector3(0, player2Paddle.velocity.y, 0);
                ball.position = new Vector3
                (
                    ball.position.x,
                    Mathf.Clamp(ball.position.y, -height, height),
                    0.0f
                );
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                ball.velocity = new Vector3(-originalXSpeed, originalYSpeed, 0);

                originalXSpeed = 0;
                originalYSpeed = 0;
            }
        }
        if (ball.position.x > 17 || ball.position.x < -17 || ball.position.y > 7 || ball.position.y <-7)
        {
            outOfBounds = true;
            StartCoroutine(OutOfBounds());

        }
    }
    void FixedUpdate()
    {



    }
    /// <summary>
    /// Sets ball spawn point at 0,0,0 and sets a random velocity
    /// </summary>
    public void PlayBall()
    {
        ball.velocity = new Vector3(0, 0, 0);
        ball.position = new Vector3(0, 0, 0);
        if (gameOver)
        {
            gameOver = false;
            restart = false;
            gameOverPanel.SetActive(false);
            restartText.SetActive(false);
            player1Score = 0;
            player2Score = 0;
            player1Paddle.position = new Vector3(player1Paddle.position.x, 0, 0);
            player2Paddle.position = new Vector3(player2Paddle.position.x, 0, 0);
        }
        
        float randomXDirection = Random.Range(0, 2);
        if (randomXDirection == 0)
        {
            ball.velocity = new Vector3(-speed, Random.Range(-3f, 3f), 0f);
        }
        else
        {
            ball.velocity = new Vector3(speed, Random.Range(-3f, 3f), 0f);
        }
    }
    /// <summary>
    /// Incrementally Increasing Ball Speed Every 10 Seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator IncreaseSpeed()
    {
        while (!gameOver){
            yield return new WaitForSeconds(10);
            if (ball.velocity.magnitude < 22)
            {
                ball.velocity = new Vector3(ball.velocity.x * 4 / 3, ball.velocity.y, 0);
            }
        }
    }
    IEnumerator OutOfBounds()
    {
        if (outOfBounds)
        {
            outOfBoundsPanel.SetActive(true);
            PlayBall();
            yield return new WaitForSeconds(2);
            outOfBoundsPanel.SetActive(false);
            outOfBounds = false;

        }

    }
    /// <summary>
    /// Blinking Restart Key
    /// </summary>
    /// <returns></returns>
    IEnumerator Blink()
    {

        Time.timeScale = 1f;
        while (gameOver)
        {
            if (!gameOver)
            {
                break;
            }
            else
            {
                yield return new WaitForSeconds(3/4f);
                if (!gameOver)
                {
                    break;
                }
                restartText.SetActive(false);
                yield return new WaitForSeconds(3/4f);
                if (!gameOver)
                {
                    break;
                }
                restartText.SetActive(true);
            }
        }

    }
    public void ReverseVerticalSpeed()
    {
        float reverseVerticalSpeed;
        reverseVerticalSpeed = -ball.velocity.y;
        ball.velocity = new Vector3(ball.velocity.x, reverseVerticalSpeed, 0f);
    }

    public void Player1AddScore()
    {
        player1Score += 1;
    }
    public void Player2AddScore()
    {
        player2Score += 1;
    }
    public void ResetBall()
    {
        if (player1Score < 5 && player2Score < 5)
        {
            PlayBall();
        }
        else
        {
            ball.velocity = new Vector3(0,0,0);
            EndGame();
        }
    }
    public void EndGame()
    {

        Time.timeScale = 0f;

        if (player1Score == 5)
        {
            gameOverText.text ="Game Over.\nPlayer 1 wins!!!";
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverText.text = "Game Over.\nPlayer 2 wins!!!";
            gameOverPanel.SetActive(true);
        }
        gameOver = true;
        restart = true;
        restartText.SetActive(true);
        StartCoroutine(Blink());
    }

}
