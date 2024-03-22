using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] Image backImage;
    [SerializeField] GameObject title;
    [SerializeField] GameObject startButton;
    void Start()
    {
        backImage.DOFade(1f, 1.5f).OnComplete(() => { SetActiveTure(); });
    }
    void SetActiveTure()
    {
        title.SetActive(true);
        startButton.SetActive(true);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
