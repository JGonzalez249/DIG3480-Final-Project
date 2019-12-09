using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    //Text for UI
    public Text scoreText;
    public Text restartText;
    public Text winText;
    public Text gameOverText;
    public Text timeAttackText;
    public Text timerText;

    //Timer for Time Attack
    private float timer;

    //Boolean States
    private bool timeAttack;
    private bool gameOver;
    private bool restart;

    //Score for game
    private int score;

    //Win and Lose Music
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;


    private PlayerController playerController;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }



        timeAttack = false;
        gameOver = false;
        restart = false;
        restartText.text = "";
        winText.text = "";
        gameOverText.text = "";
        timeAttackText.text = "";
        timerText.text = "";

        timer = 30.0f;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene("Main");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                SceneManager.LoadScene("Main");
                timeAttack = true;
            }

           

            if (timeAttack)
            {
                spawnWait = 0.2f;
                timer -= Time.deltaTime;
                timerText.text = "Time Left: " + timer;
                     
                 if(timer < 0)
                 {
                   restart = true;
                 }
               

            }
        }


        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards [Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);


            if (gameOver)
            {
             
                restartText.text = "Press 'G' for Restart";
                timeAttackText.text = "Press 'B' for Time Attack";
                restart = true;
                timeAttack = true;
                break;
            }
        }
    }

   



    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win!" +
                "\n"+
                "Game Created by Jonathan Gonzalez";
            gameOver = true;
            restart = true;
            timeAttack = true;


            Destroy(playerController.GetComponent<MeshCollider>());
            
            //Might use this for final project
            //Destroy(playerController);            
                    
        }

        //Stop BG music and switch to Game Over Music
        if (gameOver)
        {
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = true;


            if (score >= 100)
            {
                musicSource.Stop();
                musicSource.PlayOneShot(musicClipOne, 0.7F);
                musicSource.loop = true;

            }
        }
    }

   public void GameOver()
    {
        gameOverText.text = "Game Over!" + 
            "\n" +
            "Game Created by Jonathan Gonzalez";
        gameOver = true;
    }

    public void AttackOver()
    {
        timeAttack = true;
    }
}