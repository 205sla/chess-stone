using System.Collections.Generic;
using UnityEngine;

public class CardMoveS : MonoBehaviour
{
    public GameObject TextMana, TextName, TextText;

    int HandLocationNum;
    public string CardName;
    int COAST;

    bool MoveStart = false;

    public GameObject B_bishop, B_horse, B_king, B_Pawn, B_queen, B_rock, W_bishop, W_horse, W_king, W_Pawn, W_queen, W_rock;

    bool ThisCardClick = false;

    public GameObject ChessMomP, CardChessM;

    Vector3 hideCardWhenotherCardControl = new Vector3(0, 0, 0);

    List<Vector3> SetPS;

    Vector3 TargetLocation, TargetScale, V1 = new Vector3(1, 1, 1);
    // Start is called before the first frame update



    void Start()
    {
        SetCard3D_M();
    }

    // Update is called once per frame
    void Update()
    {
        HandLocationNum = GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand1.IndexOf(CardName);
        if (MoveStart) SmoothMovement();
    }
    public void MoveME()
    {

        MoveStart = true;

    }


    void SetCard3D_M()
    {
        string n = (CardName.Substring(0, 4));
        if (GameObject.Find("CoinManager").GetComponent<CardManagerS>().MeWhite)
        {
            if (n == "Pawn")
            {
                W_Pawn.gameObject.SetActive(true);
            }
            else if (n == "Knig")
            {
                W_horse.gameObject.SetActive(true);
            }
            else if (n == "Bish")
            {
                W_bishop.gameObject.SetActive(true);
            }
            else if (n == "Rook")
            {
                W_rock.gameObject.SetActive(true);
            }
            else if (n == "Quee")
            {
                W_queen.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("����..");
            }
        }
        else
        {
            if (n == "Pawn")
            {
                B_Pawn.gameObject.SetActive(true);
            }
            else if (n == "Knig")
            {
                B_horse.gameObject.SetActive(true);
            }
            else if (n == "Bish")
            {
                B_bishop.gameObject.SetActive(true);
            }
            else if (n == "Rook")
            {
                B_rock.gameObject.SetActive(true);
            }
            else if (n == "Quee")
            {
                B_queen.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("����..");
            }
        }
        SetCOAST();
        SetText();
    }

    void SmoothMovement()
    {
        //�ٸ� ī�� �������̰ų� ī�带 �̴� ���̸� �ٸ� ī�� �Ʒ��� �����
        hideCardWhenotherCardControl = GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardControl || GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardDraw != 0 ? new Vector3(0, 1, 1) * -1.5f : new Vector3(0, 0, 0);
        if (HandLocationNum == MouseLocationNum())
        {
            TargetScale = V1 * 0.25f;
            TargetLocation = new Vector3(-1.0f + HandLocationNum * 1.8f + 1.9f, 1f, -7.91f);
        }
        else
        {
            TargetScale = V1 * 0.2f;
            if (MouseLocationNum() == -1)
            {
                TargetLocation = new Vector3(-1.0f + HandLocationNum * 2.3f, -0.76f, -7.91f);
            }
            else
            {

                if (HandLocationNum > MouseLocationNum())
                {
                    TargetLocation = new Vector3(-1.0f + HandLocationNum * 2.3f + 0.5f, -0.76f, -7.91f);
                }
                else
                {
                    TargetLocation = new Vector3(-1.0f + HandLocationNum * 2.3f - 0.5f, -0.76f, -7.91f);
                }
            }

        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, TargetLocation + hideCardWhenotherCardControl, Time.deltaTime * 10f);
        transform.localScale = Vector3.Lerp(transform.localScale, TargetScale, Time.deltaTime * 15f);
    }

    public void SetMouseNumCard(bool IsClick = false)
    {
        if (MoveStart && !GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardControl)
        {

            GameObject.Find("CoinManager").GetComponent<CardManagerS>().MouseNum = HandLocationNum;
            ThisCardClick = IsClick;

            if (IsClick && CanUseCard())
            {
                GameObject.Find("TurnManager").GetComponent<TurnManagerS>().ManaCoast1 -= COAST;
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardControl = true;

                SetPS = CardChessM.GetComponent<CardChessM_S>().ReturnWorldLocation();
                Vector3 P = SetPS[0];
                Vector3 S = SetPS[1];


                GameObject myInstance = Instantiate(ChessMomP);
                if (GameObject.Find("CoinManager").GetComponent<CardManagerS>().MeWhite)
                {
                    myInstance.GetComponent<ChessMomP_S>().SetChessMomP("White", CardName);

                }
                else
                {
                    myInstance.GetComponent<ChessMomP_S>().SetChessMomP("Black", CardName);

                }
                myInstance.GetComponent<ChessMomP_S>().CardName = CardName;
                myInstance.GetComponent<ChessMomP_S>().IsMe = true;
                myInstance.transform.localPosition = P;
                myInstance.transform.localScale = S;
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().hand1.Remove(CardName);

                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }


    }

    int MouseLocationNum()
    {
        return GameObject.Find("CoinManager").GetComponent<CardManagerS>().MouseNum;
    }

    bool CanUseCard()
    {
        if (!GameObject.Find("TurnManager").GetComponent<TurnManagerS>().CanCardControl)
        {
            return false;
        }
        else
        {
            int Mana = GameObject.Find("TurnManager").GetComponent<TurnManagerS>().ManaCoast1;


            if (Mana >= COAST)
            {
                if (GameObject.Find("CoinManager").GetComponent<CardManagerS>().field1.Count <= 7)
                {
                    return true;

                }
                else
                {
                    //����Ʈ �߰�
                    GameObject.Find("EffectManager").GetComponent<EffectManagerS>().EffectJen("FullField", MouseP_Vector());
                    Debug.Log("�ʵ� ����");
                    return false;
                }
            }
            else
            {
                //����Ʈ �߰�
                GameObject.Find("EffectManager").GetComponent<EffectManagerS>().EffectJen("NoMana", MouseP_Vector());
                Debug.Log("��� ����");
                return false;
            }
        }
    }

    void SetCOAST()
    {

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
    }

    Vector3 MouseP_Vector()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void SetText()
    {
        string n = (CardName.Substring(0, 4));
        string TextCardName, TextCardText;
        if (n == "Pawn")
        {
            TextCardName = "Pawn";
            TextCardText = "���� ����ɶ�,\n�տ� �ִ� ����\n<color=red>����(1)</color> �մϴ�.";
        }
        else if (n == "Knig")
        {
            TextCardName = "Knight";
            TextCardText = "���� ����ɶ�,\n��� ����\n<color=red>����(1)</color> �մϴ�.";
        }
        else if (n == "Bish")
        {
            TextCardName = "Bishop";
            TextCardText = "���� ����ɶ�,\n�翷�� �Ʊ�����\n<color=blue>ġ��(2)</color> �մϴ�.";
        }
        else if (n == "Rook")
        {
            TextCardName = "Rook";
            TextCardText = "���� ����ɶ�,\n�տ� �ִ� ����\n<color=red>����(2)</color> �մϴ�.";
        }
        else if (n == "Quee")
        {
            TextCardName = "Queen";
            TextCardText = "���� ����ɶ�,\n�տ� �ִ� ����\n<color=red>����(4)</color> �մϴ�.";
        }
        else
        {
            TextCardName = "�̷�! ����...";
            TextCardText = "��� ���� ����\n������";
            Debug.LogError("����!");
        }
        TextMana.GetComponent<SET_TEXT>().T = COAST.ToString();
        TextName.GetComponent<SET_TEXT>().T = (TextCardName);
        TextText.GetComponent<SET_TEXT>().T = (TextCardText);
    }

}
