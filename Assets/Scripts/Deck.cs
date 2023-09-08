using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Deck : MonoBehaviour
{
    public GameManager manager;
    public List<Card> cardList;
    public Hands player;
    public bool isgiving;
    public bool isready;

    void Awake()
    {
        transform.position = new Vector3(6.5f, 3, transform.position.z);
    }

    void Update()
    {

    }

    public void Suffle()
    {
        int n = cardList.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Card card = cardList[k];
            cardList[k] = cardList[n];
            cardList[n] = card;
        }
    }

    public void GiveCard(Hands user)
    {
        StartCoroutine(CoGiveCard(user));
    }

    public void GiveFirstTwoCard(Hands[] users)
    {
        Debug.Log("FirstTwoCard");
        StartCoroutine(CoGiveFirstTwoCard(users));
    }


    IEnumerator CoGiveFirstTwoCard(Hands[] users)
    {
        if (isgiving)
            yield break;

        
        isgiving = true;

        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < 2; i++)
        {
            foreach (Hands user in users)
            {
                Card lastCard = cardList[cardList.Count - 1];
                int frameCount = 0;
                while (frameCount < 30)
                {
                    frameCount++;
                    lastCard.transform.position = Vector3.Lerp(lastCard.transform.position, user.transform.position, 0.25f);
                    yield return null;
                }

                user.cards.Add(lastCard);
                lastCard.transform.SetParent(user.transform);

                user.totalValue += lastCard.value;
                if (lastCard.value == 11)
                    user.total11Ace++;

                lastCard = null;
                cardList = cardList.GetRange(0, cardList.Count - 1);

                yield return new WaitForSeconds(0.5f);

            }
        }

        isgiving = false;
    }

    IEnumerator CoGiveCard(Hands user)
    {
        if (isgiving)
            yield break;

        isgiving = true;

        Card lastCard = cardList[cardList.Count - 1];
        int frameCount = 0;
        while (frameCount < 30)
        {
            frameCount++;
            lastCard.transform.position = Vector3.Lerp(lastCard.transform.position, user.transform.position, 0.2f);
            yield return null;
        }

        user.cards.Add(lastCard);
        lastCard.transform.SetParent(user.transform);

        user.totalValue += lastCard.value;
        if (lastCard.value == 11)
            user.total11Ace++;

        lastCard = null;
        cardList = cardList.GetRange(0, cardList.Count - 1);

        yield return new WaitForSeconds(0.5f);
        isgiving = false;
    }
}
