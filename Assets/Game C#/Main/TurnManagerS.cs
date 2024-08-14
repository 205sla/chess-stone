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
                    GameObject.Find("White King").GetComponent<KingAniControl>().SetKing(MeWhite);//왕 이동
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
            else if (Turn == 1)//내턴 시작   //이펙트 추가
            {
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(-30);

                MaxManaCoast1++;
                MaxManaCoast1 = MaxManaCoast1 > 10 ? 10 : MaxManaCoast1;//최대 10
                ManaCoast1 = MaxManaCoast1;
                SeeCoast = ManaCoast1;
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().DrawCardPLZ(true);


                Turn = 2;
            }
            else if (Turn == 2)//카드 뽑기
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
            else if (Turn == 2.5f)//카드 낼 수 있음
            {
                SeeCoast = ManaCoast1;
                GameObject.Find("head_LOD0").GetComponent<head_LOD0_S>().headOutLine(true);
                CanCardControl = true;


                //턴 종료 버튼위에 마우스 + 마우스 클릭 + 카드 조작 안하고 있으면 -> 턴 종료 하기
                if (GameObject.Find("TurnButton").GetComponent<TurnButtonS>().MouseOnturnButton && !GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardControl)
                {
                    Turn = 3;
                }
            }
            else if (Turn == 3)//턴 종료 버튼 누름
            {
                GameObject.Find("head_LOD0").GetComponent<head_LOD0_S>().headOutLine(false);
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(0);
                CanCardControl = false;


                TimeD = 0;
                Turn = 4;
                AttackNum = 0;
            }
            else if (Turn == 4)//공격
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
            else if (Turn == 5)//죽음 확인 시작
            {
                NumberOfDeaths = 0;
                ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
                foreach (GameObject ChessMom in ChessMomsss)
                {
                    NumberOfDeaths += ChessMom.GetComponent<ChessMomP_S>().DeathCheck();
                }

                Turn = NumberOfDeaths > 0 ? 5.5f : 5.9f;
            }
            else if (Turn == 5.5f)//죽은게 있을 때만
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
            else if (Turn == 6)//상대턴 시작
            {
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(30);

                MaxManaCoast2++;
                MaxManaCoast2 = MaxManaCoast2 > 10 ? 10 : MaxManaCoast2;//최대 10
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
            else if (Turn == 7.1f)//카드 내기
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
                            Debug.Log("퀸");
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
                            Debug.Log("퀸내고 싶어서 동전");
                            AiCanUseCoin = false;
                            GameObject.Find("CoinB").GetComponent<CoinBS>().AI_UseCoin();
                            ManaCoast2++;
                        }else if(ManaCoast2 >= 4 && AI_Hand[1] != 0 && GetField(true).Count>3)
                        {
                            Debug.Log("기사(적 4인 이상)");
                            AI_card = "Knight";
                            AI_LocationNum = Random.Range(0, 2) * GetField(false).Count;
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if (ManaCoast2 == 3 && AI_Hand[1] != 0 && GetField(true).Count > 3 &&AiCanUseCoin)
                        {
                            Debug.Log("기사(적 4인 이상) 내고 싶어서 동전");
                            AiCanUseCoin = false;
                            GameObject.Find("CoinB").GetComponent<CoinBS>().AI_UseCoin();
                            ManaCoast2++;
                        }else if (ManaCoast2 >= 3 && AI_Hand[2] != 0 && GetField(false).Count > 1)
                        {
                            Debug.Log("비숍(아군 2인 이상)");
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
                            Debug.Log("룩");
                            AI_card = "Rook";
                            AI_LocationNum = Random.Range(0, GetField(false).Count);
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if(ManaCoast2 >= 4 && AI_Hand[1] != 0)
                        {
                            Debug.Log("기사");
                            AI_card = "Knight";
                            AI_LocationNum = Random.Range(0, 2) * GetField(false).Count;
                            UseCardAI(AI_card, AI_LocationNum);
                        }else if (ManaCoast2 >= 3 && AI_Hand[2] != 0)
                        {
                            Debug.Log("비숍");
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
                            Debug.Log("폰2개 이상이고 마나도 2이상이라 무지성");
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
                            Debug.Log("일단 2마나 이상임");
                            if (GetField(true).Count > GetField(false).Count)
                            {
                                Debug.Log("적이 더 많음");
                                if (GetField(false).Count == 0)
                                {
                                    Debug.Log("아군 없어서 폰");
                                    AI_card = "Pawn";
                                    AI_LocationNum = 0;
                                    UseCardAI(AI_card, AI_LocationNum);
                                }
                                else
                                {
                                    Debug.Log("아군 있어서 넘김");
                                    Turn = 8;
                                }
                            }else if(GetField(false).Count %2 == GetField(true).Count % 2)
                            {
                                Debug.Log("짝수 홀수 여부 같음 폰");
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
                                Debug.Log("적이 더 많지도 않고 짝수 홀수 여부도 달라서 넘김");
                                Turn = 8;
                            }
                        }
                        else
                        {
                            Debug.Log("뭔가 오류로 넘김");
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


                //GameObject.Find("CoinB").GetComponent<CoinBS>().AI_UseCoin();     //그냥 코인 사라지게 하는 코드 임
                // 실제 마나는 따로 늘려야 함 코인이 없거나 플레이어 소유의 코인인 경우 오류 발생 가능


            }
            else if (Turn == 8)//턴 종료 버튼
            {
                GameObject.Find("TurnButton").GetComponent<TurnButtonS>().TurnButtonRotation(0);
                TimeD = 0;
                Turn = 9;
                AttackNum = 0;
            }
            else if (Turn == 9)//공격
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
            else if (Turn == 10)//죽음 확인
            {
                NumberOfDeaths = 0;
                ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
                foreach (GameObject ChessMom in ChessMomsss)
                {
                    NumberOfDeaths += ChessMom.GetComponent<ChessMomP_S>().DeathCheck();
                }



                Turn = NumberOfDeaths > 0 ? 10.5f : 10.9f;
            }
            else if (Turn == 10.5f) //죽은게 있을 때만
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
                    //검은 왕 패
                }
                else
                {
                    GameObject.Find("White King").GetComponent<KingAniControl>().Ani_King(4);
                    //흰 왕 패
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
                Debug.Log("오류..");
            }
        }

        return SortHand;
    }

    void UseCardAI(string Name, int SetNum)//SetNum = 위치번호
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
            Debug.Log("오류..");
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
