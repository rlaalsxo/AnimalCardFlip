using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Board board;

    private Card[] flipedCardId = new Card[2];
    private bool isPlaying = false;
    private bool isEnd = false;

    [SerializeField] Slider timeSlider;
    float timeLimit = 60f;
    float currentTime;

    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI overText, reText;
    public bool isGameOver = false;
    private void Awake()
    {
        Instance = this;
        currentTime = timeLimit;
        
    }
    public void MatchCard(Card id)
    {
        if (isPlaying)
        {
            flipedCardId[1] = id;
            isEnd = true;
        }
        else
        {
            flipedCardId[0] = id;
            isPlaying = true;
        }

        if (isEnd)
        {
            if (flipedCardId[0].id == flipedCardId[1].id)
            {
                for (int i = 0; i < flipedCardId.Length; i++)
                {
                    flipedCardId[i].CardCloaking();
                    board.cards.Remove(flipedCardId[i]);
                }
                if (board.cards.Count == 0)
                {
                    GameOver(true);
                }
            }
            else
            {
                for (int i = 0; i < flipedCardId.Length; i++)
                {
                    flipedCardId[i].FlipCard();
                }
                currentTime -= 2f;
            }

            isPlaying = false;
            isEnd = false;
        }
    }
    void Update()
    {
        currentTime -= Time.deltaTime;
        timeSlider.value = currentTime / timeLimit;

        if (currentTime <= 0f)
        {
            GameOver(false);
        }
    }
    void GameOver(bool success)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            GameOverPanel.SetActive(true);
            if (success)
            {
                overText.text = "Victory";
                reText.text = "ReStart";
            }
            else
            {
                overText.text = "Game Over";
                reText.text = "Retry";
            }
            Time.timeScale = 0f;
        }
    }
    public void ReButton()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
       
    }
}
