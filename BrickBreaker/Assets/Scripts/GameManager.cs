using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefact;
    public GameObject PlayerPrefact;
    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelComplete;
    public GameObject panelGameOver;
    public GameObject[] levels;



    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ballText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI highScoreText;
    

    public static GameManager Instance { get; private set; }    
    public enum State { MENU,INIT,PLAY,LEVELCOMPLETED,LOADLEVEL,GAMEOVER}
    State state;
    GameObject _currentBall;
    GameObject _currentLevel;
     bool _isSwitchingState;

    private int _score;
    public int Score
    {
        get { return _score; }
        set { _score = value; scoreText.text = "SCORE: " + _score; }
        
    }

    private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value;levelText.text = "LEVEL: " + _level; }
       
    }
    private int _balls;
    public int Balls
    {
        get { return _balls; }
        set { _balls = value;
            ballText.text = "BALLS: " + Balls;
        }
    }

    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }
    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);   
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(_currentBall == null ) { if (Balls > 0) {

                       _currentBall= Instantiate(ballPrefact);
                    } else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                if (_currentLevel !=null && _currentLevel.transform.childCount==0 && !_isSwitchingState)
                {
                    SwitchState(State.LEVELCOMPLETED);
                }
                break;
            case State.LEVELCOMPLETED:

                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if (Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;

        }

    }

   public void SwitchState(State newState, float delay=0)
    {

        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        state = newState;
        BeginState(newState);
        _isSwitchingState = false;    
    }
     void BeginState(State newState)
    {
        switch (newState)
        { 
           case State.MENU:
                Cursor.visible = true;
                panelMenu.SetActive(true);
                highScoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                break;
                case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                if(_currentLevel != null)
                {
                    Destroy(_currentLevel);
                }
                Instantiate(PlayerPrefact);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                Destroy(_currentBall);
                Destroy(_currentLevel);
                Level++;
                panelLevelComplete.SetActive(true);
                SwitchState(State.LOADLEVEL,2f);

              break;
            case State.LOADLEVEL:
                if(Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }
                break;
            case State.GAMEOVER:
                if (Score > PlayerPrefs.GetInt("Highscore"))
                {
                    PlayerPrefs.GetInt("Highscore",Score);
                }
                panelGameOver.SetActive(true);
                
                break;  

        }
    }
    void EndState()
    {
        switch (state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelComplete.SetActive(false);

                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);

                break;

        }

    }

}
