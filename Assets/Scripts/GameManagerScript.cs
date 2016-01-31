using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{

    public float time;
    public float authorTime;

    private bool isFinish = false;
    private bool started = false;
    private int ennemiesLeft = 0;

    private float timeLeft;
    private Text text;

    private GameObject MVP;
    private GameObject victory;
    private GameObject tryAgain;

    // Use this for initialization
    void Start()
    {
        timeLeft = time;// * 1000;
        text = findText("Timer");
        text.text = timeLeft.ToString();

        ennemiesLeft = GameObject.FindGameObjectsWithTag("ennemy").Length;

        MVP = GameObject.Find("Reward_MVP");
        victory = GameObject.Find("Reward_Victory");
        tryAgain = GameObject.Find("tryAgain");
        MVP.SetActive(false);
        victory.SetActive(false);
        tryAgain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinish)
        {
            if(started)
                timeLeft -= Time.deltaTime;

            //Next niveau
            if (ennemiesLeft <= 0 || timeLeft <= 0)
            {
                isFinish = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().setActive(false);
                findText("Restart").color = Color.black; //Methode pirate


                if (ennemiesLeft <= 0)
                    findText("Next").color = Color.black;

                if(timeLeft > authorTime)
                {
                    MVP.SetActive(true);
                }
                else if( timeLeft > 0)
                {
                    victory.SetActive(true);
                }
                else
                {
                    tryAgain.SetActive(true);
                    timeLeft = 0;
                }
            }

            text.text = timeLeft.ToString();
        }
        else //ENDSTATE
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                //SceneManager.LoadScene( (int.Parse(SceneManager.GetActiveScene().name)+1).ToString());
            }
        }

        findText("Number").text = ennemiesLeft.ToString();

    }

    private Text findText(string text)
    {
        Text[] texts = GameObject.Find("Canvas").GetComponentsInChildren<Text>();
        for(int i = 0; i < texts.Length; i++)
        {
            if(texts[i].name == text)
            {
                return texts[i]; //Ne pas faire
            }
        }
        return null;
    }

    public void removeEnnemy()
    {
        if (!started)
            started = true;
        ennemiesLeft--;
    }

    public void SetStarted( bool st )
    {
        started = st;
    }
}