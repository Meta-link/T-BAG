using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public float time;
    public float authorTime;

    private bool isFinish = false;
    private bool cleared = false;

    private float timeLeft;
    private Text text;

    // Use this for initialization
    void Start()
    {
        timeLeft = time;// * 1000;
        text = findText("Timer");
        text.text = timeLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinish)
        {
            timeLeft -= Time.deltaTime;
            //END OF TIME
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                isFinish = true;
                findText("Restart").color = Color.black; //Methode pirate
            }
            text.text = timeLeft.ToString();
            //Next niveau
            /*
            if(fin)
            {
                findText("Next").color = Color.black;
            }
            */
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
}