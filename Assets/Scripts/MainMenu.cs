using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    enum ButtonStates
    {
        PLAY = 0,
        QUIT = 1,
        CREDITS = 2,
    };

    bool inCredits = false;

    bool axisInUse = false;
    bool creditsCamInUse = false;
    public TextMesh Play, Quit, Credits, CreditsText;
    GameObject mainCam;
    float axis;

    ButtonStates currentState;
	// Use this for initialization
	void Start () {
        currentState = ButtonStates.PLAY;
        mainCam = GameObject.Find("Main Camera");

    }
	
	// Update is called once per frame
	void Update () {

        axis = Input.GetAxisRaw("Vertical");

        if (axis != 0)
        {
            if (axisInUse == false)
            {
                if( axis > 0)
                {
                    if (currentState == ButtonStates.PLAY)
                    {
                        currentState = ButtonStates.CREDITS;
                    }
                    else
                        currentState--;
                }
                else if( axis < 0)
                {
                    if (currentState == ButtonStates.CREDITS)
                    {
                        currentState = ButtonStates.PLAY;
                    }
                    else
                        currentState++;
                }
                axisInUse = true;
            }
        }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            axisInUse = false;
        }

        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if(currentState == ButtonStates.PLAY)
            {
                SceneManager.LoadScene("SceneProg");
            }
            if(currentState == ButtonStates.QUIT)
            {
                Application.Quit();
            }
            if (currentState == ButtonStates.CREDITS)
            {
                inCredits = true;
            }
        }

        if (inCredits)
        {
            if(!creditsCamInUse)
            {
                mainCam.transform.position = new Vector3(41.3f,0, -6.23f);
                creditsCamInUse = true;
            }

            CreditsText.transform.Translate(0, 0.1f, 0);

            if(CreditsText.transform.position.y > 55)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        SetColor();
    }

    void SetColor()
    {
        if (currentState == ButtonStates.PLAY)
        {
            Play.color = Color.red;
        }
        else
        {
            Play.color = Color.white;
        }
        if (currentState == ButtonStates.QUIT)
        {
            Quit.color = Color.red;
        }
        else
        {
            Quit.color = Color.white;
        }
        if (currentState == ButtonStates.CREDITS)
        {
            Credits.color = Color.red;
        }
        else
        {
            Credits.color = Color.white;
        }
    }
}
