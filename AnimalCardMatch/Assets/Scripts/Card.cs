using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite animalSprite;
    [SerializeField] private Sprite backSprite;
    public int id;
    private Image cardImage;
    Vector3 originalScale;
    Vector3 targetScale;

    private bool isFliping = false;
    private bool isFliped = false;
    private bool isMatched = false;

    private void Awake()
    {
        originalScale = transform.localScale;
        targetScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);
        cardImage = this.gameObject.GetComponent<Image>();
    }
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        isFliping = true;

        transform.DOScaleX(targetScale.x, 0.2f).OnComplete(() =>
        {
            cardImage.sprite = animalSprite;
            transform.DOScaleX(originalScale.x, 0.2f).OnComplete(() => { });
            
            transform.DOScaleX(targetScale.x, 0.2f).OnComplete(() =>
            {
                cardImage.sprite = backSprite;
                transform.DOScaleX(originalScale.x, 0.2f);

            }).SetDelay(2f);
        }).SetDelay(0.3f);

        isFliping = false;
    }
    public void FlipCard()
    {
        isFliping = true;
        transform.DOScaleX(targetScale.x, 0.2f).OnComplete(() => 
        {
            isFliped = !isFliped;

            if (isFliped)
            {
                cardImage.sprite = animalSprite;
                transform.DOScaleX(originalScale.x, 0.2f).OnComplete(() =>
                {
                    isFliping = false;
                    GameManager.Instance.MatchCard(this);
                });
            }
            else
            {
                cardImage.sprite = backSprite;
                transform.DOScaleX(originalScale.x, 0.2f).OnComplete(() =>
                {
                    isFliping = false;
                });
            }
        });
    }
    public void ClickCard()
    {
        if(!isFliping && !isFliped && !isMatched && !GameManager.Instance.isGameOver)
        {
            FlipCard();
        }
    }
    public void CardCloaking()
    {
        isMatched = true;
        Color color = cardImage.color;
        color.a = 0f;
        cardImage.color = color;
    }
}
