using UnityEngine;

public class CoinCheckS : MonoBehaviour
{
    Vector3 diceVelocity;
    public int CoinSideNum = 0;
    bool CoinIsDone = false;
    float T;

    public bool CoinSpinStart = false;
    // Update is called once per frame
    private void Update()
    {
        if (CoinSpinStart)
        {

            T += Time.deltaTime;
        }

    }
    void FixedUpdate()
    {
        diceVelocity = CoinS.diceVelocity;
    }

    void OnTriggerStay(Collider col)
    {
        if (T > 3 && diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f && (!CoinIsDone))
        {
            //Debug.Log(col.gameObject.name);
            switch (col.gameObject.name)
            {
                case "side 1":
                    CoinSideNum = 1;
                    CoinIsDone = true;
                    GameObject.Find("CoinManager").GetComponent<CardManagerS>().CoinIsEnd();
                    break;
                case "side 2":
                    CoinSideNum = 2;
                    CoinIsDone = true;
                    GameObject.Find("CoinManager").GetComponent<CardManagerS>().CoinIsEnd();

                    break;

            }
        }
    }
}
