using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCountS : MonoBehaviour
{
    public int ManaCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManaCount = GameObject.Find("TurnManager").GetComponent<TurnManagerS>().SeeCoast;
    }
}
