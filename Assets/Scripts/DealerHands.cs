using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class DealerHands : Hands
{
    public bool isturn;
    bool getcard;

    void Update()
    {
        ShowHand();

        if (!isturn)
            return;

        if (totalValue > manager.playerHands.totalValue && totalValue > 17)
        {
            if (totalValue > 21)
                return;

            EndTurn();
        }

        else
            GetCard();
    }

    void GetCard()
    {
        if (getcard)
            return;

        getcard = true;

        manager.deck.GiveCard(this);


        getcard = false;
    }

    void EndTurn()
    {
        isturn = false;
        manager.EndPhase();
    }

    public override void Burst()
    {
        isturn = false;
        base.Burst();
    }
}
