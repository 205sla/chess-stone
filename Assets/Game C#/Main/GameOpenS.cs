using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameOpenS : MonoBehaviour
{
   
    public CinemachineVirtualCamera MainCam, OpeningCam, RCam, LCam, C_Cam;
    public GameObject King1, King2;
    float OPNtime = 0;
    int OPNnum = 0;
    bool spin = true;

    
    void Start()
    {
        OpeningCam.Priority = 2;
    }

    

    // Update is called once per frame
    void Update()
    {
        OPNtime += Time.deltaTime;
        

        if (OPNnum == 0)
        {
            if (OPNtime > 1.5f)
            {
                
                KingAni(1, 1);
                KingAni(2, 1);
                OPNtime = 0;
                OPNnum = 1;
            }
           
        }else if(OPNnum == 1)
        {
            if (OPNtime > 1.0f)
            {
                LCam.Priority = 2;
                OPNtime = 0;
                OPNnum = 2;
            }
        }
        else if (OPNnum == 2)
        {
            if (OPNtime > 0.8f)
            {
                KingAni(1, 2);
                OPNtime = 0;
                OPNnum = 3;
            }
        }
        else if (OPNnum == 3)
        {
            if (OPNtime > 0.3f)
            {
                KingAni(1, 3);
                OPNtime = 0;
                OPNnum = 4;
            }
        }
        else if (OPNnum == 4)
        {
            if (OPNtime > 0.5f)
            {
                RCam.Priority = 3;
                OPNtime = 0;
                OPNnum = 5;
            }
        }
        else if (OPNnum == 5)
        {
            if (OPNtime > 0.8f)
            {
                KingAni(2, 2);
                OPNtime = 0;
                OPNnum = 6;
            }
        }
        else if (OPNnum == 6)
        {
            if (OPNtime > 0.3f)
            {
                KingAni(2, 3);
                OPNtime = 0;
                OPNnum = 7;
            }
        }
        else if (OPNnum == 7)
        {
            if (OPNtime > 1.9f)
            {
                King1.GetComponent<KingAniControl>().BackKing(1);
                King2.GetComponent<KingAniControl>().BackKing(2);
                OpeningCam.Priority = 4;
                OPNtime = 0;
                OPNnum = 8;
            }
        }else if(OPNnum == 8)
        {
            if(OPNtime > 0.5f)
            {

                C_Cam.Priority = 20;
                OPNtime = 0;
                OPNnum = 9;
            }
        }else if(OPNnum == 9)
        {
            if (OPNtime > 1.6f)
            {
                spin = false;
                GameObject.Find("CoinMon").GetComponent<CoinS>().CoineSpinStart();
                OPNtime = 0;
                OPNnum = 10;
            }
        }else if(OPNnum == 10)
        {
            if (GameObject.Find("TurnManager").GetComponent<TurnManagerS>().IsGameStart)
            {
                MainCam.Priority = 21;
                OPNtime = 0;
                OPNnum = 11;
            }

            if (OPNtime > 10f)//10초후에 동전 결과 안 나왔으면 다시 던지기
            {
                OPNnum = 9;
                
            }
        }else if(OPNnum == 11)
        {

        }




        if (OPNtime > 12.0f && spin)
        {
            spin = false;
            GameObject.Find("CoinMon").GetComponent<CoinS>().CoineSpinStart();
        }
    }




    void KingAni(int KingNum, int AniNum)
    {
        if (KingNum == 1)
        {
            King1.GetComponent<KingAniControl>().Ani_King(AniNum);
        }
        else if(KingNum == 2)
        {
            King2.GetComponent <KingAniControl>().Ani_King(AniNum);
        }
        else
        {
            Debug.LogError("파라미터 오류");
        }
    }

    
}
