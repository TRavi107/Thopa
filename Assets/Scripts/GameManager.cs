using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RamailoGames;
using TMPro;

public enum ColorType
{
    white,
    black
}

[System.Serializable]
public struct ColorWithName
{
    public Color color;
    public ColorType type;
}

public class GameManager : MonoBehaviour
{
    #region Tmp Text

    public TMP_Text GameOverScoreText;
    public TMP_Text GameOverhighscoreText;
    public TMP_Text scoreText;
    public TMP_Text gamePlayhighscoreText;
    public TMP_Text pausescoreText;
    public TMP_Text gamePausehighscoreText;

    #endregion
    public static GameManager instance;
    public Transform spawnPos;
    public Transform deletePos;
    public Transform leftbound;
    public Transform rightbound;
    public GameObject colorCirclePrefab;
    public PlatfromController platfromController;

    public ColorWithName[] availableColors;

    [SerializeField] int score;
    [SerializeField] float maxSpeed;
    [SerializeField] float speedIncreaseRate;

    public float currentSpeed;
    [SerializeField] float speedIncreaseInterval;

    public bool paused;
    float startTime;

    float lastScoreCheckedTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //snakecontroller.SetMyColor(GetRandomColor());
        ScoreAPI.GameStart((bool s) => {
        });
        paused = false;
        startTime = Time.time;
        currentSpeed = 2;
        lastScoreCheckedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ColorWithName GenerateRandomColor()
    {
        int index = Random.Range(0, availableColors.Length);
        return availableColors[index];
    }

    public void GenerateColorCicles()
    {
        GameObject colorCircle = Instantiate(colorCirclePrefab, spawnPos.position, Quaternion.identity);
    }

    public void GameOver()
    {
        //PauseGame();
        Time.timeScale = 0;
        UIManager.instance.SwitchCanvas(UIPanelType.GameOver);
        UIManager.instance.SwitchCanvas(UIPanelType.GameOver);
        //fruitsCutText.text = "Fruits Cut :  " + fruitscut.ToString();
        GameOverScoreText.text = score.ToString();
        //MaxComboText.text = "Max Combo:  " + maxCombo.ToString();
        int playTime = (int)(Time.time - startTime);
        ScoreAPI.SubmitScore(score, playTime, (bool s, string msg) => { });
        GetHighScore();
    }
    public void PauseGame()
    {
        //UIManager.instance.DisableCombo();
        setHighScore(gamePausehighscoreText);
        //int min = (int)gameTimer / 60;
        //int sec = (int)gameTimer % 60;
        //pauseMenugameTimerText.text = min.ToString() + ":" + sec.ToString();
        pausescoreText.text = score.ToString();
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
    }

    void setHighScore(TMP_Text highscroreTextUI)
    {
        ScoreAPI.GetData((bool s, Data_RequestData d) => {
            if (s)
            {
                if (score >= d.high_score)
                {
                    highscroreTextUI.text = score.ToString();

                }
                else
                {
                    highscroreTextUI.text = d.high_score.ToString();
                }

            }
        });
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
        setHighScore(gamePlayhighscoreText);
        if ((Time.time - lastScoreCheckedTime) > speedIncreaseInterval)
        {
            currentSpeed += speedIncreaseRate;
            currentSpeed = Mathf.Clamp(currentSpeed, 2, maxSpeed);
            lastScoreCheckedTime = Time.time;
        }
    }


    void GetHighScore()
    {
        ScoreAPI.GetData((bool s, Data_RequestData d) => {
            if (s)
            {
                if (score >= d.high_score)
                {
                    GameOverhighscoreText.text = score.ToString();
                    //congratulationText.gameObject.SetActive(true);

                }
                else
                {
                    GameOverhighscoreText.text = d.high_score.ToString();
                }

            }
        });

    }
}
