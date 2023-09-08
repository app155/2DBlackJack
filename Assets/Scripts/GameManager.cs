using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Deck deck;
    public Hands[] players;
    public Hands playerHands;
    public DealerHands dealerHands;

    public Button hitButton;
    public Button standButton;
    public Text burstText;
    public Text resultText;


    void Awake()
    {
        playerHands.manager = this;
        dealerHands.manager = this;
        deck.manager = this;

        hitButton.enabled = false;
        standButton.enabled = false;

        StartPhase();
    }

    void Update()
    {

    }

    void StartPhase()
    {
        dealerHands.isturn = false;

        deck.Suffle();

        deck.GiveFirstTwoCard(players);

        PlayerPhase();
    }


    public void PlayerPhase()
    {
        hitButton.enabled = true;
        standButton.enabled = true;
    }

    public void DealerPhase()
    {
        hitButton.enabled = false;
        standButton.enabled = false;
        dealerHands.isturn = true;
    }

    public void Bursted(Hands user)
    {
        StartCoroutine(BurstRoutine(user));
    }

    IEnumerator BurstRoutine(Hands user)
    {
        burstText.text = $"{user.name.Substring(0, 6)} Bursted";
        burstText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        burstText.gameObject.SetActive(false);

        foreach (Hands p in players)
            p.Clear();

        yield return new WaitForSeconds(1.5f);

        StartPhase();
    }

    public void EndPhase()
    {
        StartCoroutine(CoEndPhase());
    }

    IEnumerator CoEndPhase()
    {
        if (playerHands.totalValue > dealerHands.totalValue)
            resultText.text = "Player Win";

        else if (dealerHands.totalValue > playerHands.totalValue)
            resultText.text = "Dealer Win";

        else
            resultText.text = "Draw";

        resultText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        foreach (Hands p in players)
            p.Clear();

        resultText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        StartPhase();
    }

}


