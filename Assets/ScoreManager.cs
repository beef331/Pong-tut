using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int leftScore;
    public static int rightScore;
    public static int maxScore = 5;

    [SerializeField]
    Text leftScoreText;
    [SerializeField]
    Text rightScoreText;
    [SerializeField]
    Text endText;
    [SerializeField]
    GameObject endMenu;

    private Scene currentScene;
    private bool menuOpened;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
        if(leftScore >= maxScore || rightScore >= maxScore && !menuOpened)
        {
            Time.timeScale = 0f;
            menuOpened = true;
            if(leftScore > rightScore)
            {
                endText.text = "You win!";
            }
            else
            {
                endText.text = "You lost.";
            }
            endMenu.SetActive(true);
        }

	}
    public void NewGame()
    {
        Time.timeScale = 1;
        leftScore = 0;
        rightScore = 0;
        SceneManager.LoadScene("Level");
    }
}
