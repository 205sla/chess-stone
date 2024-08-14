using System.Collections.Generic;
using UnityEngine;

public class TurnManagerS : MonoBehaviour
{
    bool MeWin;

    bool MeWhite;
    public float Turn = 0;
    float TimeD = 0;

    public bool IsGameStart = false;

    int MaxManaCoast1, MaxManaCoast2;
    public int ManaCoast1, ManaCoast2;
    public bool CanCardControl = false;
    public int SeeCoast = 0;


    public static float AttackTtime = 2.5f;
    static float DeathCheckTime = 7.5f;
    int AttackNum;

    public GameObject ChessMomP;
    GameObject[] ChessMomsss;
    // Start is called before the first frame update

    int NumberOfDeaths = 0;

    public GameObject Result, Win, Lose, Rebutton;

    bool AiCanUseCoin = false;
    

    public void TurnStart()
    {
        IsGameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        TurnControl();
    }

    void TurnControl()
    {
        if (IsGameStart)
        {
            TimeD += Time.deltaTime;
            if (Turn == 0)
            {
                if (TimeD > 5.5f)
                {
                    MeWhite = GameObject.Find("CoinManager").GetComponent<CardManagerS>().MeWhite;
                    GameObject.Find("White King").GetComponent<KingAniControl>().SetKing(MeWhite);//�� �̵�
                    GameObject.Find("Black King").GetComponent<KingAniControl>().SetKing(!MeWhite);
                    Turn = 0.1f;
                    TimeD = 0;
                    AiCanUseCoin = MeWhite;
                }


            }
            else if (Turn == 0.1f)
            {
                if (TimeD > 1)
                {

                    Turn = 0.2f;
                    TimeD = 0;
                }
            }
            else if (Turn == 0.2f)
            {
                if (TimeD > 5)
                {
                    Turn = 0.3f;
                    TimeD = 0;
                }
            }
            else if (Turn == 0.3f)
            {
                if (TimeD > 0)
                {
                    Turn = 0.4f;
                    TimeD = 0;
                }
            }
            else if (Turn == 0.4f)
            {
                Turn = MeWhite ? 1 : 6;
                TimeD = 0;
            }
            else if (Turn == 1)//���� ����   //����Ʈ �߰�
            {
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(-30);

                MaxManaCoast1++;
                MaxManaCoast1 = MaxManaCoast1 > 10 ? 10 : MaxManaCoast1;//�ִ� 10
                ManaCoast1 = MaxManaCoast1;
                SeeCoast = ManaCoast1;
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().DrawCardPLZ(true);


                Turn = 2;
            }
            else if (Turn == 2)//ī�� �̱�
            {
                SeeCoast = ManaCoast1;
                if (GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardDraw == 0)
                {
                    if (!MeWhite)
                    {
                        if (GameObject.Find("Black King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                        {
                            MeWin = false;
                            Turn = 20;
                        }
                        else
                        {
                            Turn = 2.5f;
                        }
                    }
                    else
                    {
                        if (GameObject.Find("White King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                        {
                            MeWin = false;
                            Turn = 20;
                        }
                        else
                        {
                            Turn = 2.5f;
                        }
                    }
                    
                }


            }
            else if (Turn == 2.5f)//ī�� �� �� ����
            {
                SeeCoast = ManaCoast1;
                GameObject.Find("head_LOD0").GetComponent<head_LOD0_S>().headOutLine(true);
                CanCardControl = true;


                //�� ���� ��ư���� ���콺 + ���콺 Ŭ�� + ī�� ���� ���ϰ� ������ -> �� ���� �ϱ�
                if (GameObject.Find("TurnButton").GetComponent<TurnButtonS>().MouseOnturnButton && !GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardControl)
                {
                    Turn = 3;
                }
            }
            else if (Turn == 3)//�� ���� ��ư ����
            {
                GameObject.Find("head_LOD0").GetComponent<head_LOD0_S>().headOutLine(false);
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(0);
                CanCardControl = false;


                TimeD = 0;
                Turn = 4;
                AttackNum = 0;
            }
            else if (Turn == 4)//����
            {
                TimeD += Time.deltaTime;
                if (TimeD > AttackTtime)
                {
                    TimeD = 0;
                    if (AttackNum < GetField(true).Count)
                    {
                        Attack(true, GetField(true)[AttackNum]);
                        AttackNum++;
                    }
                    else
                    {
                        Turn = 5;
                    }
                }
            }
            else if (Turn == 5)//���� Ȯ�� ����
            {
                NumberOfDeaths = 0;
                ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
                foreach (GameObject ChessMom in ChessMomsss)
                {
                    NumberOfDeaths += ChessMom.GetComponent<ChessMomP_S>().DeathCheck();
                }

                Turn = NumberOfDeaths > 0 ? 5.5f : 5.9f;
            }
            else if (Turn == 5.5f)//������ ���� ����
            {
                TimeD += Time.deltaTime;
                if (TimeD > DeathCheckTime)
                {
                    foreach (GameObject ChessMom in ChessMomsss)
                    {
                        ChessMom.GetComponent<ChessMomP_S>().DeleteChess();
                    }
                    TimeD = 0;
                    Turn = 5.9f;
                }
            }
            else if (Turn == 5.9f)
            {
                
                if (MeWhite)
                {
                    if (GameObject.Find("Black King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                    {
                        MeWin = true;
                        Turn = 20;
                    }
                    else
                    {
                        Turn = 6f;
                    }
                }
                else
                {
                    if (GameObject.Find("White King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                    {
                        MeWin = true;
                        Turn = 20;
                    }
                    else
                    {
                        Turn = 6f;
                    }
                }
            }
            else if (Turn == 6)//����� ����
            {
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(30);

                MaxManaCoast2++;
                MaxManaCoast2 = MaxManaCoast2 > 10 ? 10 : MaxManaCoast2;//�ִ� 10
                ManaCoast2 = MaxManaCoast2;
                SeeCoast = ManaCoast2;
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().DrawCardPLZ(false);
                Turn = 7;
                TimeD = 0;
            }
            else if (Turn == 7)
            {
                TimeD += Time.deltaTime;
                if (TimeD > 3.5f)
                {

                    if (MeWhite)
                    {
                        if (GameObject.Find("Black King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                        {
                            MeWin = true;
                            Turn = 20;
                        }
                        else
                        {

                            Turn = 7.1f;
                        }
                    }
                    else
                    {
                        if (GameObject.Find("White King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                        {
                            MeWin = true;
                            Turn = 20;
                        }
                        else
                        {

                            Turn = 7.1f;
                        }
                    }
                }
            }
            else if (Turn == 7.1f)//ī�� ����
            {
                
                SeeCoast = ManaCoast2;
                TimeD += Time.deltaTime;

                string AI_card="";
                int AI_LocationNum =0;

                if (TimeD > 1.5f)
                {
                    if (GetField(false).Count >= 7)
                    {
                        TimeD = 0;
                        Turn = 8;
                        SeeCoast = ManaCoast2;
                    }
                    else
                    {
                        List<int> AI_Hand = AI_SortHand();
                        if (ManaCoast2 >= 7 && AI_Hand[4] != 0)
                        {
                            Debug.Log("��");
                            AI_card = "Queen";
                            if (GetField(true).Count < GetField(false).Count + 1)
                            {
                                AI_LocationNum = Random.Range(0, 2) * GetField(false).Count;
                            }
                            else
                            {
                                AI_LocationNum = Random.Range(1, GetField(false).Count - 1);
                            }
                            UseCardAI(AI_card, AI_LocationNum);
                        }
                        else if(ManaCoast2 == 6 && AI_Hand[4] != 0 && AiCanUseCoin)
                        {
                            Debug.Log("������ �; ����");
                            AiCanUseCoin = false;
                            GameObject.Find("CoinB").GetComponent<CoinBS>().AI_UseCoin();
                            ManaCoast2++;
                        }else if(ManaCoast2 >= 4 && AI_Hand[1] != 0 && GetField(true).Count>3)
                        {
                            Debug.Log("���(�� 4�� �̻�)");
                            AI_card = "Knight";
                            AI_LocationNum = Random.Range(0, 2) * GetField(false).Count;
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if (ManaCoast2 == 3 && AI_Hand[1] != 0 && GetField(true).Count > 3 &&AiCanUseCoin)
                        {
                            Debug.Log("���(�� 4�� �̻�) ���� �; ����");
                            AiCanUseCoin = false;
                            GameObject.Find("CoinB").GetComponent<CoinBS>().AI_UseCoin();
                            ManaCoast2++;
                        }else if (ManaCoast2 >= 3 && AI_Hand[2] != 0 && GetField(false).Count > 1)
                        {
                            Debug.Log("���(�Ʊ� 2�� �̻�)");
                            AI_card = "Bishop";
                            if(GetField(false).Count % 2 == 0)
                            {
                                AI_LocationNum = GetField(false).Count / 2;
                            }
                            else
                            {
                                AI_LocationNum = (GetField(false).Count-1) / 2 + Random.Range(0, 2);
                            }
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if(ManaCoast2 >= 3 && AI_Hand[3] != 0)
                        {
                            Debug.Log("��");
                            AI_card = "Rook";
                            AI_LocationNum = Random.Range(0, GetField(false).Count);
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if(ManaCoast2 >= 4 && AI_Hand[1] != 0)
                        {
                            Debug.Log("���");
                            AI_card = "Knight";
                            AI_LocationNum = Random.Range(0, 2) * GetField(false).Count;
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if (ManaCoast2 >= 3 && AI_Hand[2] != 0)
                        {
                            Debug.Log("���");
                            AI_card = "Bishop";
                            if (GetField(false).Count % 2 == 0)
                            {
                                AI_LocationNum = GetField(false).Count / 2;
                            }
                            else
                            {
                                AI_LocationNum = (GetField(false).Count - 1) / 2 + Random.Range(0, 2);
                            }
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if(ManaCoast2 >= 2 && AI_Hand[0] >= 2)
                        {
                            Debug.Log("��2�� �̻��̰� ������ 2�̻��̶� ������");
                            AI_card = "Pawn";
                            int R = Random.Range(0,4);
                            if(R == 0)
                            {
                                AI_LocationNum = Random.Range(0, 2) * GetField(false).Count;
                            }
                            else if(R == 1)
                            {
                                if (GetField(false).Count % 2 == 0)
                                {
                                    AI_LocationNum = GetField(false).Count / 2;
                                }
                                else
                                {
                                    AI_LocationNum = (GetField(false).Count - 1) / 2 + Random.Range(0, 2);
                                }
                            }
                            else if(R == 2)
                            {
                                AI_LocationNum = Random.Range(1, GetField(false).Count - 1);
                            }
                            else
                            {
                                AI_LocationNum = Random.Range(0, GetField(false).Count);
                            }
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if(ManaCoast2 >= 1 && AI_Hand[0] !=0)
                        {
                            Debug.Log("�ϴ� 2���� �̻���");
                            if (GetField(true).Count > GetField(false).Count)
                            {
                                Debug.Log("���� �� ����");
                                if (GetField(false).Count == 0)
                                {
                                    Debug.Log("�Ʊ� ��� ��");
                                    AI_card = "Pawn";
                                    AI_LocationNum = 0;
                                    UseCardAI(AI_card, AI_LocationNum);
                                }
                                else
                                {
                                    Debug.Log("�Ʊ� �־ �ѱ�");
                                    Turn = 8;
                                }
                            }else if(GetField(false).Count %2 == GetField(true).Count % 2)
                            {
                                Debug.Log("¦�� Ȧ�� ���� ���� ��");
                                AI_card = "Pawn";
                                int R = Random.Range(0, 4);
                                if (R == 0)
                                {
                                    AI_LocationNum = Random.Range(0, 2) * GetField(false).Count;
                                }
                                else if (R == 1)
                                {
                                    if (GetField(false).Count % 2 == 0)
                                    {
                                        AI_LocationNum = GetField(false).Count / 2;
                                    }
                                    else
                                    {
                                        AI_LocationNum = (GetField(false).Count - 1) / 2 + Random.Range(0, 2);
                                    }
                                }
                                else if (R == 2)
                                {
                                    AI_LocationNum = Random.Range(1, GetField(false).Count - 1);
                                }
                                else
                                {
                                    AI_LocationNum = Random.Range(0, GetField(false).Count);
                                }
                                UseCardAI(AI_card, AI_LocationNum);
                            }
                            else
                            {
                                Debug.Log("���� �� ������ �ʰ� ¦�� Ȧ�� ���ε� �޶� �ѱ�");
                                Turn = 8;
                            }
                        }
                        else
                        {
                            Debug.Log("���� ������ �ѱ�");
                            Turn = 8;
                        }



                        bool CanMore = false;

                        for (int i = 0; i < GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand2.Count; i++)
                        {
                            if (SetCOAST(GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand2[i]) <= ManaCoast2)
                            {
                                CanMore = true;
                                break;
                            }
                        }

                        TimeD = 0;
                        SeeCoast = ManaCoast2;
                        if(!CanMore)
                        {
                            Turn = 8;
                        }


                    }
                   

                }

                





                
                //test


                //GameObject.Find("CoinB").GetComponent<CoinBS>().AI_UseCoin();     //�׳� ���� ������� �ϴ� �ڵ� ��
                // ���� ������ ���� �÷��� �� ������ ���ų� �÷��̾� ������ ������ ��� ���� �߻� ����


            }
            else if (Turn == 8)//�� ���� ��ư
            {
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(0);
                TimeD = 0;
                Turn = 9;
                AttackNum = 0;
            }
            else if (Turn == 9)//����
            {
                TimeD += Time.deltaTime;
                if (TimeD > AttackTtime)
                {
                    TimeD = 0;
                    if (AttackNum < GetField(false).Count)
                    {
                        Attack(false, GetField(false)[AttackNum]);
                        AttackNum++;
                    }
                    else
                    {
                        Turn = 10;
                    }
                }
            }
            else if (Turn == 10)//���� Ȯ��
            {
                NumberOfDeaths = 0;
                ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
                foreach (GameObject ChessMom in ChessMomsss)
                {
                    NumberOfDeaths += ChessMom.GetComponent<ChessMomP_S>().DeathCheck();
                }



                Turn = NumberOfDeaths > 0 ? 10.5f : 10.9f;
            }
            else if (Turn == 10.5f) //������ ���� ����
            {
                TimeD += Time.deltaTime;
                if (TimeD > DeathCheckTime)
                {
                    foreach (GameObject ChessMom in ChessMomsss)
                    {
                        ChessMom.GetComponent<ChessMomP_S>().DeleteChess();
                    }
                    TimeD = 0;
                    Turn = 10.9f;
                }
            }
            else if (Turn == 10.9f)
            {
                TimeD = 0;
                if (!MeWhite)
                {
                    if (GameObject.Find("Black King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                    {
                        MeWin = false;
                        Turn = 20;
                    }
                    else
                    {
                        Turn = 1f;
                    }
                }
                else
                {
                    if (GameObject.Find("White King").GetComponentInChildren<KingHpS>().KingHpNum <= 0)
                    {
                        MeWin = false;
                        Turn = 20;
                    }
                    else
                    {
                        Turn = 1f;
                    }
                }
            }
            else if (Turn == 20f)
            {
                if (MeWhite == MeWin)
                {
                    GameObject.Find("Black King").GetComponent<KingAniControl>().Ani_King(4);
                    //���� �� ��
                }
                else
                {
                    GameObject.Find("White King").GetComponent<KingAniControl>().Ani_King(4);
                    //�� �� ��
                }
                Turn = 21f;
                TimeD = 0;
            }
            else if (Turn == 21f)
            {
                TimeD += Time.deltaTime;
                if (TimeD > 6.0f)
                {
                    ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
                    foreach (GameObject ChessMom in ChessMomsss)
                    {
                        ChessMom.GetComponent<ChessMomP_S>().Kill(!MeWin);
                    }

                    TimeD = 0;
                    Turn = 22;
                }
            }
            else if (Turn == 22)
            {
                TimeD += Time.deltaTime;
                if (TimeD > 3.0f)
                {
                    GameObject.Find("EffectManager").GetComponent<EffectManagerS>().FirecrackerNum = Random.Range(20, 30);
                    TimeD = 0;
                    Turn = 23;
                }
            }
            else if (Turn == 23)
            {
                TimeD += Time.deltaTime;
                if (TimeD > 3.0f)
                {
                    TimeD = 0;
                    Result.gameObject.SetActive(true);
                    if (MeWin)
                    {
                        Win.gameObject.SetActive(true);
                    }
                    else
                    {
                        Lose.gameObject.SetActive(true);
                    }
                    Turn = 24;
                }
            }
            else if(Turn == 24)
            {
                TimeD += Time.deltaTime;
                if (TimeD > 3.0f)
                {
                    TimeD = 0;
                    Turn = 25;
                    Rebutton.gameObject.SetActive(true);
                }
            }
        }
    }
    List<int> AI_SortHand()
    {
        List<string> AI_Hand = GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand2;
        List<int> SortHand = new List<int> { 0, 0, 0, 0, 0 };
        for(int i=0; i < AI_Hand.Count; i++)
        {
            string n = (AI_Hand[i].Substring(0, 4));
            if (n == "Pawn")
            {
                SortHand[0]++;
            }
            else if (n == "Knig")
            {
                SortHand[1]++;
            }
            else if (n == "Bish")
            {
                SortHand[2]++;
            }
            else if (n == "Rook")
            {
                SortHand[3]++;
            }
            else if (n == "Quee")
            {
                SortHand[4]++;
            }
            else
            {                
                Debug.Log("����..");
            }
        }

        return SortHand;
    }

    void UseCardAI(string Name, int SetNum)//SetNum = ��ġ��ȣ
    {
        SetNum = SetNum > GetField(false).Count ? Random.Range(0, GetField(false).Count) : SetNum;
        List<string> AI_Hand = GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand2;
        string CardName = "";
        for (int i = 0; i < AI_Hand.Count; i++)
        {
            string n = (AI_Hand[i].Substring(0, 4));
            if(n == Name.Substring(0, 4))
            {
                CardName = AI_Hand[i];
                break;
            }
        }            


        GameObject.Find("TurnManager").GetComponent<TurnManagerS>().ManaCoast2 -= SetCOAST(CardName);

        GameObject myInstance = Instantiate(ChessMomP);
        if (!GameObject.Find("CoinManager").GetComponent<CardManagerS>().MeWhite)
        {
            myInstance.GetComponent<ChessMomP_S>().SetChessMomP("White", CardName);

        }
        else
        {
            myInstance.GetComponent<ChessMomP_S>().SetChessMomP("Black", CardName);

        }
        myInstance.GetComponent<ChessMomP_S>().CardName = CardName;
        myInstance.GetComponent<ChessMomP_S>().IsMe = false;
        myInstance.GetComponent<ChessMomP_S>().AIsetNum = SetNum;
        myInstance.transform.localPosition = new Vector3(0, 2, 12);
        myInstance.transform.localRotation = Quaternion.Euler(0, 180, 0);

        GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand2.Remove(CardName);



    }


    int SetCOAST(string CardName)
    {
        int COAST;
        string n = (CardName.Substring(0, 4));
        if (n == "Pawn")
        {
            COAST = 1;
        }
        else if (n == "Knig")
        {
            COAST = 4;
        }
        else if (n == "Bish")
        {
            COAST = 3;
        }
        else if (n == "Rook")
        {
            COAST = 3;
        }
        else if (n == "Quee")
        {
            COAST = 7;
        }
        else
        {
            COAST = 205;
            Debug.Log("����..");
        }
        return COAST;
    }

    List<string> GetField(bool IsMe)
    {
        if (IsMe)
        {
            return GameObject.Find("CoinManager").GetComponent<CardManagerS>().field1;
        }
        else
        {
            return GameObject.Find("CoinManager").GetComponent<CardManagerS>().field2;
        }
    }

    void Attack(bool IsMe, string CardName)
    {
        GameObject[] ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
        string Name = "ChessMomP(" + CardName + "," + (IsMe ? "Me" : "You") + ")";
        foreach (GameObject ChessMom in ChessMomsss)
        {
            if (ChessMom.name == Name)
            {
                ChessMom.GetComponent<ChessMomP_S>().ChessAttack();
            }
            else
            {

            }
        }



    }

}
