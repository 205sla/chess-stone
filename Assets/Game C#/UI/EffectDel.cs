using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDel : MonoBehaviour
{
    public float DelTime;
    // Start is called before the first frame update
    float T = 0;

    private void Start()
    {
        if(DelTime == 0)
        {
            DelTime = 1.1f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        T+=Time.deltaTime;
        if (T > DelTime)
        {
            Destroy(gameObject);
        }
    }
}
