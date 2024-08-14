using System.Collections.Generic;
using UnityEngine;

public class ChessMomP_S : MonoBehaviour
{
    public string CardName;
    bool set = true, MeWhite;
    float TargetZ1 = -2, TargetZ2 = 2;

    public bool IsMe;

    int MoveSpeed = 20;
    Vector3 objPos, mousePos;

    public int AIsetNum;

    GameObject[] ChessMomsss;

    float MoveAttack = 0;

    private void Start()
    {
        MeWhite = GameObject.Find("CoinManager").GetComponent<CardManagerS>().MeWhite;
        gameObject.name = "ChessMomP(" + CardName + "," + (IsMe ? "Me" : "You") + ")";
    }
    public void SetChessMomP(string color, string name)
    {
        transform.GetComponentInChildren<ChessObjectS>().SetFieldObjectModel(color, name);
        transform.GetComponentInChildren<HpBarS>().SetHP(name);

    }

    private void Update()
    {
        LocationInitialization();

        if (MoveAttack != 0)
        {
            if (MoveAttack > 1)
            {
                MoveAttack = 0;
            }
            else
            {
                MoveAttack += Time.deltaTime;
            }
        }
        
    }
    void LocationInitialization()
    {

        if (IsMe)//Me
        {
            if (set)
            {
                float distance = Camera.main.WorldToScreenPoint(transform.position).z;
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                objPos = Camera.main.ScreenToWorldPoint(mousePos);

                if (objPos.z < -8)
                {
                    objPos.z = -8;
                }
                else if (objPos.z > 0)
                {
                    objPos.z = 0;
                }

                if (objPos.x > 8)
                {
                    objPos.x = 8;
                }
                else if (objPos.x < -8)
                {
                    objPos.x = -8;
                }

                //objPos.z = -1;
                objPos.y = 2;

                if (!(Input.GetMouseButton(0)) && Mathf.Abs(TargetZ1 - transform.position.z) < 1)//³õ´Â °Å ¿Ï·á
                {
                    GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardControl = false;
                    MoveSpeed = 5;

                    set = false;
                    objPos.z = TargetZ1;
                    SetField();
                }
            }
            else
            {
                if (transform.localScale.x != 1)
                {
                    if (transform.localScale.x < 1)
                    {
                        transform.localScale = new Vector3(1, 1, 1) * (transform.localScale.x + Time.deltaTime / 3);
                    }
                    else if (transform.localScale.x > 1)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                
                objPos = SwitchCoordinates(FildIndexOf(), ReturnMyFild().Count);

            }
            transform.position = Vector3.Lerp(transform.position, objPos, Time.deltaTime * MoveSpeed);
        }
        else//AI
        {
            if (set)
            {
                MoveSpeed = 5;
                objPos.z = TargetZ2;
                AddFild(AIsetNum);
                set = false;
            }
            else
            {
                
                objPos = SwitchCoordinates(FildIndexOf(), ReturnMyFild().Count);
                transform.position = Vector3.Lerp(transform.position, objPos, Time.deltaTime * MoveSpeed);
            }


        }
    }

    void SetField()
    {
        float thisX = transform.position.x;
        if (ReturnMyFild().Count == 0)
        {
            AddFild(0);
        }
        else
        {
            bool ifIN = false;
            for (int i = 0; i < ReturnMyFild().Count; i++)
            {
                if (thisX < SwitchCoordinates(i, ReturnMyFild().Count).x)
                {
                    ifIN = true;
                    AddFild(i);
                    break;
                }
            }
            if (!ifIN)
            {
                AddFild(ReturnMyFild().Count);

            }
        }
    }

    Vector3 SwitchCoordinates(int Sequence, int all)
    {
        int newX = 1 - all + Sequence * 2;
        return new Vector3(newX, 2.0f, (IsMe ? TargetZ1 : TargetZ2) + Mathf.Sin(MoveAttack * Mathf.PI) * (IsMe ? 2 : -2));
    }

    List<string> ReturnMyFild()
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

    void AddFild(int LocationNumber)
    {
        if (IsMe)
        {
            GameObject.Find("CoinManager").GetComponent<CardManagerS>().field1.Insert(LocationNumber, CardName);
        }
        else
        {
            GameObject.Find("CoinManager").GetComponent<CardManagerS>().field2.Insert(LocationNumber, CardName);

        }
    }

    int FildIndexOf()
    {
        if (IsMe)
        {
            return GameObject.Find("CoinManager").GetComponent<CardManagerS>().field1.IndexOf(CardName);

        }
        else
        {
            return GameObject.Find("CoinManager").GetComponent<CardManagerS>().field2.IndexOf(CardName);

        }
    }

    public void ChessAttack()
    {

        //°ø°Ý
        int AttackPower = MyAttackPower();
        if (AttackPower == -1)//¸»
        {
            MoveAttack = 0.1f;
            GiveDamageAll(IsMe, 1);
        }
        else if (AttackPower == -2)//ºñ¼ó Èú
        {
            GameObject.Find("EffectManager").GetComponent<EffectManagerS>().EffectJen("Healing", objPos + new Vector3(0, 0, 0));
            GiveHealing(FildIndexOf(), IsMe);
        }
        else
        {
            MoveAttack = 0.1f;
            int MyAttackNum1 = 6 - ReturnMyFild().Count + 2 * FildIndexOf();
            int MyAttackNum2 = 6 - ReturnMyFild().Count + 2 * FildIndexOf() + 1;
            GiveDamage(MyAttackNum1, MyAttackNum2, IsMe, AttackPower);
        }
    }
    void GiveDamageAll(bool teamME, int Damage)
    {
        ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
        foreach (GameObject ChessMom in ChessMomsss)
        {
            ChessMom.GetComponent<ChessMomP_S>().GetDamageAll(!IsMe, Damage);
        }
        if (IsMe == MeWhite)
        {
            GameObject.Find("Black King").GetComponentInChildren<KingHpS>().GetDamagesKing(Damage);
        }
        else { GameObject.Find("White King").GetComponentInChildren<KingHpS>().GetDamagesKing(Damage); }
        string n = (CardName.Substring(0, 4));
        int Rand = Random.Range(1, 3);
        transform.GetComponentInChildren<ChessAniControl>().Ani_Attack(Rand);
    }
    void GiveDamage(int EnemyPositionNum1, int EnemyPositionNum2, bool teamME, int Damage)
    {
        ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
        int TargetCount = 0;
        foreach (GameObject ChessMom in ChessMomsss)
        {
            TargetCount += ChessMom.GetComponent<ChessMomP_S>().GetDamage(EnemyPositionNum1, EnemyPositionNum2, !IsMe, Damage);
        }
        if (TargetCount == 0)//¿Õ °ø°Ý
        {
            if (IsMe == MeWhite)
            {
                GameObject.Find("Black King").GetComponentInChildren<KingHpS>().GetDamagesKing(Damage);
            }
            else { GameObject.Find("White King").GetComponentInChildren<KingHpS>().GetDamagesKing(Damage); }
        }
        string n = (CardName.Substring(0, 4));
        int Rand = Random.Range(1, 3);
        transform.GetComponentInChildren<ChessAniControl>().Ani_Attack(Rand);
    }

    void GiveHealing(int HealPositionNum, bool teamME)
    {
        ChessMomsss = GameObject.FindGameObjectsWithTag("ChessMom");
        foreach (GameObject ChessMom in ChessMomsss)
        {
            ChessMom.GetComponent<ChessMomP_S>().GetHealing(HealPositionNum, IsMe);
        }
        transform.GetComponentInChildren<ChessAniControl>().Ani_Attack(0);
    }
    public void GetDamageAll(bool teamME, int Damage)
    {
        if (teamME == IsMe)
        {
            int Hp;
            Hp = transform.GetComponentInChildren<HpBarS>().HP - Damage;
            if (Hp <= 0)
            {
                Hp = 0;
                //Á×À½
            }
            transform.GetComponentInChildren<HpBarS>().HP = Hp;
        }
    }
    public int GetDamage(int EnemyPositionNum1, int EnemyPositionNum2, bool teamME, int Damage)
    {
        if (teamME == IsMe)
        {
            int MyAttackNum1 = 6 - ReturnMyFild().Count + 2 * FildIndexOf();
            int MyAttackNum2 = 6 - ReturnMyFild().Count + 2 * FildIndexOf() + 1;
            if (EnemyPositionNum1 == MyAttackNum1 || EnemyPositionNum1 == MyAttackNum2 || EnemyPositionNum2 == MyAttackNum1 || EnemyPositionNum2 == MyAttackNum2)
            {

                int Hp;
                Hp = transform.GetComponentInChildren<HpBarS>().HP - Damage;
                if (Hp <= 0)
                {
                    Hp = 0;
                    //Á×À½
                }
                transform.GetComponentInChildren<HpBarS>().HP = Hp;
                return 1;
            }
        }
        return 0;

    }

    public void GetHealing(int HealPositionNum, bool teamME)
    {
        if (teamME == IsMe)
        {
            if (Mathf.Abs(FildIndexOf() - HealPositionNum) == 1)
            {
                int Hp, MaxHp;
                Hp = transform.GetComponentInChildren<HpBarS>().HP + 2;
                MaxHp = transform.GetComponentInChildren<HpBarS>().MaxHP;
                if (Hp > MaxHp)
                {
                    Hp = MaxHp;
                }
                transform.GetComponentInChildren<HpBarS>().HP = Hp;


            }
        }

    }

    int MyAttackPower()
    {
        int Power = 0;
        string n = (CardName.Substring(0, 4));
        if (n == "Pawn")
        {
            Power = 1;
        }
        else if (n == "Knig")
        {
            Power = -1;
        }
        else if (n == "Bish")
        {
            Power = -2;
        }
        else if (n == "Rook")
        {
            Power = 2;
        }
        else if (n == "Quee")
        {
            Power = 4;
        }
        else
        {
            Power = 0;
            Debug.Log("¿À·ù..");
        }
        return Power;
    }


    public int DeathCheck()
    {
        if (transform.GetComponentInChildren<HpBarS>().HP <= 0)
        {
            //Á×À½ Ã³¸®

            int Rand = Random.Range(1, 3);
            transform.GetComponentInChildren<ChessAniControl>().Ani_die(Rand);
            return 1;

        }
        else
        {
            return 0;
        }
    }

    public void Kill(bool Team)
    {
        if(Team == IsMe)
        {
            int Rand = Random.Range(1, 3);
            transform.GetComponentInChildren<ChessAniControl>().Ani_die(Rand);
        }
    }

    public void DeleteChess()
    {
        //»èÁ¦
        if(transform.GetComponentInChildren<HpBarS>().HP <= 0)
        {
            GameObject.Find("EffectManager").GetComponent<EffectManagerS>().EffectJen("Die", objPos + new Vector3(0,1,0));
            if (IsMe)
            {
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().field1.Remove(CardName);
            }
            else
            {
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().field2.Remove(CardName);

            }
            Destroy(gameObject);

        }
        
    }
}
