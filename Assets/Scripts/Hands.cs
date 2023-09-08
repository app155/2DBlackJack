using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public int totalValue;
    public int total11Ace;
    public List<Card> cards;
    public GameManager manager;
    public bool ischecking;


    void Update()
    {
        ShowHand();
    }

    void LateUpdate()
    {
        BurstCheck();
    }

    protected void ShowHand()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = transform.position + new Vector3(1, 0, -1) * 0.3f * i;
            cards[i].transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
    }

    void BurstCheck()
    {
        if (ischecking)
            return;


        if (totalValue <= 21)
            return;

        if (total11Ace > 0)
        {
            total11Ace--;
            totalValue -= 10;
            return;
        }

        ischecking = true;

        StartCoroutine(CoWait());

        Burst();
        ischecking = false;
    }

    public virtual void Burst()
    {
        manager.Bursted(this);
    }

    public void Clear()
    {
        foreach (Card card in cards)
        {
            card.transform.position = manager.deck.transform.position;
            card.transform.rotation = Quaternion.Euler(0, 0, 0);
            manager.deck.cardList.Add(card);
            card.transform.SetParent(manager.deck.transform);
        }

        cards.Clear();
        totalValue = 0;
        total11Ace = 0;
    }

    IEnumerator CoWait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
