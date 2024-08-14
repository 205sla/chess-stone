using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHpS : MonoBehaviour
{
    public GameObject[] KingHearts;
    public int KingHpNum = 20, ShowHP = 0;
    public int ShowStart = -2;
    public static float DelayTime = 0.7f;
    float TimeD = 0;

    public bool White;

    private void Update()
    {
        if (ShowStart == 20)
        {
            if (KingHpNum != ShowHP)
            {
                TimeD += Time.deltaTime;
                if (TimeD > DelayTime)
                {
                    ShowHP = KingHpNum;
                    TimeD = 0;
                }
            }
        }else if(ShowStart >= 0)
        {
            TimeD += Time.deltaTime;
            if(TimeD > 0.1f)
            {
                TimeD = 0;
                
                ShowHP = ShowStart-1;
                
                KingHearts[ShowStart].gameObject.SetActive(true);
                KingHearts[ShowStart].GetComponent<KingHpHeartS>().heartNum = ShowStart + 1;
                ShowStart++;
            }
        }
    }

    public void ShowHpStart()
    {
        //if(GameObject.Find("CoinManager").GetComponent<CardManagerS>().MeWhite != White)//ÀûÀÌ¸י
        //{
        //    

        //}
        ShowStart = 0;
    }

    public void GetDamagesKing(int Damage)
    {
        KingHpNum-=Damage;
    }



}
