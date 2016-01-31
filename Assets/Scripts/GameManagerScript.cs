using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{

    public float time;
    public float authorTime;

    public AudioClip[] Voices;

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

        if(SceneManager.GetActiveScene().buildIndex != 1)
            GetComponents<AudioSource>()[1].Play();
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
            GetComponents<AudioSource>()[2].Pause();
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex +1);
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
            SetStarted(true);
        ennemiesLeft--;

        if(Random.Range(0,3) == 0)
        {
            GetComponent<AudioSource>().clip = Voices[Random.Range(0, Voices.GetLength(0))];
            GetComponent<AudioSource>().Play();
        }
    }

    public void SetStarted( bool st )
    {
        started = st;
    }
}