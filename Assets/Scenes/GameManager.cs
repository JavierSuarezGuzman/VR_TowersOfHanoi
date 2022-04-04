using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    internal Logic bigCube;
    internal Logic mediumCube;
    internal Logic smallCube;
    private GameObject messageCanvas;
    private bool played;
    //private GameObject hands; //toda esta variable culiá crashea el código o deja el messageCanvas true desde Awake forever, forever, forever
    private int moves;
    private GameObject movesCanvas;
    private Text movesText;

    public GameObject UIHelpers { get; private set; }

    private void Awake()
    {
        bigCube = GameObject.Find("bigCube").GetComponent<Logic>();
        mediumCube = GameObject.Find("mediumCube").GetComponent<Logic>();
        smallCube = GameObject.Find("smallCube").GetComponent<Logic>();

        messageCanvas = GameObject.Find("MessageCanvas");

        //hands = GameObject.Find("Hands");
        UIHelpers = GameObject.Find("UIHelpers");

        //ToggleHands(false);

        movesCanvas = GameObject.Find("MovesCanvas");
        movesText = GameObject.Find("MovesText").GetComponent<Text>();

    }

    void Start()
    // Start is called before the first frame update
    {
        messageCanvas.SetActive(false);
        RestartGame();
    }

    void Update()
    // Update is called once per frame
    {
        if (!played)
        {
            if (bigCube.isDone && mediumCube.isDone && smallCube.isDone)
            {
                GetComponent<AudioSource>().Play();
                played = true;
                messageCanvas.SetActive(true);

                //ToggleHands(false);
                movesCanvas.SetActive(false);
            }
        }
    }

    public void RestartGame()
    {
        played = false;
        messageCanvas.SetActive(false);
        bigCube.Reset(1.4f);
        mediumCube.Reset(1.6f);
        smallCube.Reset(1.75f);

        //ToggleHands(true);

        movesCanvas.SetActive(true);
        moves = 0;
        movesText.text = "Movimientos: " + moves;
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    /*     public void ToggleHands(bool visible)
        {
            hands.SetActive(visible);
            UIHelpers.SetActive(!visible);
        } */

    internal void Moved()//espero algún día llegar a ser así de eficiente con los métodos;
    {
        moves++;
        movesText.text = "Movimientos: " + moves;
    }
}
