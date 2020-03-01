using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

   
public static GameManager instance; //make this a singleton/static var

    //highscore text
    string highScoreText = "no high scores!";
    Text highScoreTextObject;

    //timer
    public float timer = 30.0f;
    private float timerAmt = 0f;
    public Text timerText;

    //score
    private int score = 0;
    private int highScore = 0;

    

    private const string FILE_HS = "/codelab1-s2020-hw3-highscores";

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            if (score > highScore)
            {
                HighScore = score;

            }
        }
    }

    public int HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;
            File.AppendAllText(Application.dataPath + FILE_HS, highScore + ",");

        }
    
    }






void Awake ()
{
    if (instance == null){ // if there isn't one already
            
        instance = this;
        DontDestroyOnLoad(gameObject); //make this the instance
        
        }else{
        Destroy(gameObject); //if there is one already : DESTROY THIS ONE
        }

        GetComponent<AudioSource>().Play();

        if (File.Exists(Application.dataPath + FILE_HS)) //checking to see if it exists
        {
            string hsString = File.ReadAllText(Application.dataPath + FILE_HS);
            
            string[] splitString = hsString.Split(',');

            highScoreText = hsString;
            //highScore = int.Parse(splitString[0]);
            int lastArraySlot = splitString.Length - 2;
            string highestScore = splitString[lastArraySlot];
            highScore = int.Parse(highestScore);
            //the last spot in the array for highest score
        }


        //putting the highscore text into the object
        GameObject textOBJ = GameObject.Find("HighScoreTEXT");
        highScoreTextObject = textOBJ.GetComponent<Text>();
        highScoreTextObject.text = highScoreText;

}

    void Start()
    {
        
    }

    private void Update()
    {
        if (timerAmt <= 0)
        {
            //stop timer
            timerAmt = 0;

            //turn on asset that says game over
            timerText.text = "GAME OVER!";

            // stop player movement
            PlayerController.canMove = false;

        }
        if (timerAmt > 0)
        {
            PlayerController.canMove = true;

            if (Input.GetKey("space"))
            {
                MainMenu();
            }
        }
        
       
    }


    public void startGame()
    {
        //sets the timerA to the timer
        timerAmt = timer;

        //loads the main game scene
        SceneManager.LoadScene("GameShop");
        Debug.Log ("I am loading the Game");
        timerText.gameObject.SetActive(true);
       
        InvokeRepeating("UpdateTimerText", 1, 1); //invoke this every one second
    }

    public void MainMenu()
    {
        //turn off the timer object
        timerText.gameObject.SetActive(false);
        

        SceneManager.LoadScene("MainMenu");
        //loads the main menu scene
        Debug.Log("I am loading the menu");

    }

    public void QuitGame()
    {
        Application.Quit();
        //quit the game
        Debug.Log("I have quit the game");
    }

    public void UpdateTimerText()
    {
        timerAmt--;
        timerText.text = ("TIME LEFT:\n" + timerAmt); //take a second away and update the text
    }
}
