using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Card[] cardPrefab;
    public List<Card> cards;
    [SerializeField]
    private int stage = 1;
    int[] cardIndex;
    int cardCount;

    void Start()
    {
        cardCount = (stage + 4) * 4;
        GameStart();
    }
    public void GameStart()
    {
        cards = new List<Card>();
        ShuffleCard();
        InitBoard();
    }

    void InitBoard()
    {
        for (int i = 0; i < cardCount; i++)
        {
            cards.Add(Instantiate(cardPrefab[cardIndex[i]], this.transform));
        }
    }
    void ShuffleCard()
    {
        cardIndex = new int[cardCount];
        int randIndex;
        int temp;

        for (int i = 0; i < cardCount; i++)
        {
            cardIndex[i] = cardPrefab[i / 2].id;
        }
        
        for(int i = 0; i < cardCount; i++)
        {
            randIndex = Random.Range(i, cardCount);
            temp = cardIndex[randIndex];
            cardIndex[randIndex] = cardIndex[i];
            cardIndex[i] = temp;
        }
    }
}
