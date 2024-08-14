using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarS : MonoBehaviour
{
    public int HP, MaxHP, ShowHP;
    public static float DelayTime = 0.7f;
    float TimeD = 0;

    public GameObject h1, h2, h3, h4, h5, h6;

    private void Update()
    {
        if (HP != ShowHP){
            TimeD += Time.deltaTime;
            if (TimeD > DelayTime) {
                ShowHP = HP;
                TimeD = 0;
            
            }
        }

    }
    public void SetHP(string name)
    {
        string n = (name.Substring(0, 4));
        if(n == "Knig")//말 이면
        {
            MaxHP = 3;
        }
        else
        {
            MaxHP = 6;
        }
        HP = MaxHP;
        ShowHP = HP;
        if (MaxHP == 6)
        {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(true);
            h5.gameObject.SetActive(true);
            h6.gameObject.SetActive(true);
        }
        else if (MaxHP == 3)
        {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("예외처리 해야 함");
        }
    }
}
