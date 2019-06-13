using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public delegate void GameStart();
    public event GameStart gameStartEvent;

    public delegate void GameOver();
    public event GameOver gameOverEvent;

    public UnityEngine.UI.Button gameStartBtn;
    public UnityEngine.UI.Button gameRetryBtn;

    public bool isPlaying;

    public float distance;
    public Text distanceTxt;

    public TextMeshProUGUI winTxt;
    public TextMeshProUGUI winTxt2;
    public TextMeshProUGUI winTxt3;
    public TextMeshProUGUI trueWinTxt;
    public TextMeshProUGUI trueWinTxt2;

    public SurfaceEffector2D se;

    public bool won = false;
    public float goal = 1000f;

    private void Awake()
    {
        //Check if instance already exists
        if (_instance == null)
        {
            //if not, set instance to this
            _instance = this;
        }

        //If instance already exists and it's not this:
        else if (_instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        se.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            distance += Time.deltaTime * 10f;
            distanceTxt.text = System.String.Format("{0:0.##}/{1} ft", distance, goal);
            if(distance >= goal && !won)
            {
                winTxt.gameObject.SetActive(true);
                winTxt2.gameObject.SetActive(true);
                winTxt3.gameObject.SetActive(true);
                Invoke("HideWinScreen", 10f);
                won = true;
                goal = 3000f;
            }
            if(distance >= goal && won)
            {
                //true ending
                trueWinTxt.gameObject.SetActive(true);
                trueWinTxt2.gameObject.SetActive(true);
                isPlaying = false;
            }
        }
    }

    void HideWinScreen()
    {
        winTxt.gameObject.SetActive(false);
        winTxt2.gameObject.SetActive(false);
        winTxt3.gameObject.SetActive(false);
    }

    public void PlayerDied()
    {
        isPlaying = false;
        gameOverEvent();
        //show retry button
        gameRetryBtn.gameObject.SetActive(true);
        se.enabled = false;
    }

    public void TryAgain()
    {
        gameRetryBtn.gameObject.SetActive(false);
        gameStartBtn.gameObject.SetActive(false);
        isPlaying = true;
        distance = 0f;
        se.enabled = true;
        gameStartEvent();
    }


}
