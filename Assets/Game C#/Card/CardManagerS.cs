using System.Collections.Generic;
using UnityEngine;

public class CardManagerS : MonoBehaviour
{

    public List<string> cards1 = new List<string>(); //me
    public List<string> cards2 = new List<string>();

    public List<string> hand1 = new List<string>(); //me
    public List<string> hand2 = new List<string>();

    public List<string> field1 = new List<string>(); //me
    public List<string> field2 = new List<string>();

    int exhaustionDamage1 = 1, exhaustionDamage2 = 1;
    public bool MeWhite;
    public GameObject CoinB;
    public GameObject CardPrefab, YourCardPrefab;

    int DrawCardNum1 = 0, DrawCardNum2 = 0;
    float DrawCardTime1 = 0, DrawCardTime2 = 0;

    public int MouseNum;

    public bool DoingCardControl = false;

    public int DoingCardDraw = 0;


    bool AIadvantage = true;
    // Start is called before the first frame update
    void Start()
    {
        AddCards();
        cards1 = ShuffleList(cards1);
        cards2 = ShuffleList(cards2);
        if (AIadvantage)
        {
            hand2.Add("Pawn0");
        }

    }

    // Update is called once per frame
    void Update()
    {
        DrowManager();
    }

    public void DrawCardPLZ(bool Me)
    {
        if (Me)
        {
            DrawCardNum1++;
        }
        else
        {
            DrawCardNum2++;
        }
    }

    void DrowManager()
    {
        DrawCardTime1 -= Time.deltaTime;
        DrawCardTime2 -= Time.deltaTime;
        if (DrawCardTime1 < 0 && DrawCardNum1 > 0)
        {

            DrawCardTime1 = 1.0f;
            DrawCardNum1 -= 1;
            DrawCard("Me");
        }
        if (DrawCardTime2 < 0 && DrawCardNum2 > 0)
        {
            DrawCardTime2 = 1.0f;
            DrawCardNum2 -= 1;
            DrawCard("You");
        }
    }

    void DrawCard(string Player)
    {
        if (Player == "Me")
        {
            if (cards1.Count > 0)
            {
                DoingCardDraw++;
                GameObject.Find("EffectManager").GetComponent<EffectManagerS>().JENDrawingEffect(true);
                GameObject myInstance = Instantiate(CardPrefab);
                myInstance.GetComponent<CardMoveS>().CardName = cards1[0];
                hand1.Add(cards1[0]);
                cards1.Remove(cards1[0]);
            }
            else
            {
                GameObject.Find("EffectManager").GetComponent<EffectManagerS>().JENDrawingEffect(true, false);
                if (!MeWhite)
                {
                    GameObject.Find("Black King").GetComponentInChildren<KingHpS>().GetDamagesKing(exhaustionDamage1++);
                }
                else { GameObject.Find("White King").GetComponentInChildren<KingHpS>().GetDamagesKing(exhaustionDamage1++); }
            }

            //Debug.Log(cards1);

        }
        else
        {
            if (cards2.Count > 0)
            {
                GameObject.Find("EffectManager").GetComponent<EffectManagerS>().JENDrawingEffect(false);
                GameObject myInstance = Instantiate(YourCardPrefab);
                hand2.Add(cards2[0]);
                cards2.Remove(cards2[0]);
            }
            else
            {
                GameObject.Find("EffectManager").GetComponent<EffectManagerS>().JENDrawingEffect(false, false);
                if (MeWhite)
                {
                    GameObject.Find("Black King").GetComponentInChildren<KingHpS>().GetDamagesKing(exhaustionDamage2++);
                }
                else { GameObject.Find("White King").GetComponentInChildren<KingHpS>().GetDamagesKing(exhaustionDamage2++); }
            }

        }

    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }
    void AddCards()
    {
        for (int i = 1; i <= 8; i++)
        {
            cards1.Add("Pawn" + i);
            cards2.Add("Pawn" + i);
        }
        for (int i = 1; i <= 2; i++)
        {
            cards1.Add("Knight" + i);
            cards1.Add("Bishop" + i);
            cards1.Add("Rook" + i);
            cards2.Add("Knight" + i);
            cards2.Add("Bishop" + i);
            cards2.Add("Rook" + i);
        }
        cards1.Add("Queen1");
        cards2.Add("Queen1");
    }
    public void CoinIsEnd()
    {
        GameObject.Find("PirateCoin").GetComponent<DissolveSphere>().CoinHide();
        int num = GameObject.Find("CoinCheck").GetComponent<CoinCheckS>().CoinSideNum;
        if (num == 1)
        {
            MeWhite = false;
            DrawCardNum1 = 4;
            DrawCardNum2 = 3;
        }
        else if (num == 2)
        {
            MeWhite = true;
            DrawCardNum1 = 3;
            DrawCardNum2 = 4;
        }
        else
        {
            Debug.Log("¿À·ù");
        }
        CoinB.gameObject.SetActive(true);
        GameObject.Find("TurnManager").GetComponent<TurnManagerS>().TurnStart();


    }

    public void MouseNULL()
    {
        MouseNum = -1;
    }
}
