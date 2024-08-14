using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head_LOD0_S : MonoBehaviour
{


    float ThisW = 0, TargetW = 0;
    float Speed = 7;

    Color Y = Color.yellow, G = Color.green;
    float ColorChange = 1;
    public bool SetY = true;
    List<string> hand;
    int Mana;
    private void Awake()
    {

        var outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Y;
        outline.OutlineWidth = 0f;
    }
    /*
     * OutlineAll,
    OutlineVisible,&&
    OutlineHidden,
    OutlineAndSilhouette,
    SilhouetteOnly
     */

    private void Update()
    {
        SetY = CanMoreControl();
        OutlineWidth();
        OutlineColor();
    }

    bool CanMoreControl()
    {
        hand = GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand1;
        Mana = GameObject.Find("TurnManager").GetComponent<TurnManagerS>().ManaCoast1;
        bool Can = false;
        for (int i = 0; i < hand.Count; i++)
        {
            if (SetCOAST(hand[i]) <= Mana)
            {
                Can = true;
                break;
            }
        }

        return Can; 
    }
    void OutlineWidth()
    {
        if (Mathf.Abs(TargetW - ThisW) < 0.3f)
        {
            ThisW = TargetW;
        }
        else
        {
            if (TargetW > ThisW)
            {
                ThisW += Time.deltaTime * Speed;
            }
            else
            {
                ThisW -= Time.deltaTime * Speed;
            }

        }

        gameObject.GetComponent<Outline>().OutlineWidth = ThisW;
    }
    void OutlineColor()
    {
        if (SetY)
        {
            if (ColorChange < 1)
            {
                ColorChange += Time.deltaTime;
            }
            else
            {
                ColorChange = 1;
            }
        }
        else
        {
            if (ColorChange > 0)
            {
                ColorChange -= Time.deltaTime;
            }
            else
            {
                ColorChange = 0;
            }
        }
        gameObject.GetComponent<Outline>().OutlineColor = ColorChange * Y + (1 - ColorChange) * G;
    }

    public void headOutLine(bool Draw)
    {
        TargetW = Draw ? 5 : 0;
        //DrawingOutline = Draw;
        //outline.OutlineWidth = DrawingOutline ? 5f : 0f;
    }
    int SetCOAST(string CardName)
    {

        string n = (CardName.Substring(0, 4));
        if (n == "Pawn")
        {
            return 1;
        }
        else if (n == "Knig")
        {
            return 4;
        }
        else if (n == "Bish")
        {
            return 3;
        }
        else if (n == "Rook")
        {
            return 3;
        }
        else if (n == "Quee")
        {
            return 7;
        }
        else
        {
            return 205;
            
        }
    }
}
