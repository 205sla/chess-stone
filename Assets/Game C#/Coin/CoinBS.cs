using UnityEngine;

public class CoinBS : MonoBehaviour
{
    Material mat;
    bool MeWhite;
    float T = 0;
    bool UseCoin = false;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        //mat.SetFloat("_DissolveAmount", 0.5f);
        MeWhite = GameObject.Find("CoinManager").GetComponent<CardManagerS>().MeWhite;
        if (!MeWhite)
        {
            //transform.position = new Vector3
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localPosition = new Vector3(-8f, -3.13f, -8.7f);
        }
        else
        {
            //transform.position = new Vector3(8f, -3.13f, 8.7f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(-8f, -3.13f, 8.7f);
        }
    }

    private void Update()
    {
        if (!UseCoin)
        {
            if (T < 3)
            {

                T += Time.deltaTime;
                mat.SetFloat("_DissolveAmount", 0.5f - T / 3);

            }
            else { T = 3; }
        }
        else
        {
            if (T > 0)
            {
                T -= Time.deltaTime;
                mat.SetFloat("_DissolveAmount", 1f - T / 3);
            }
            else
            {
                Destroy(gameObject);
            }

        }

    }

    public void CoinBClick(bool Click)
    {
        if (!MeWhite && !UseCoin)
        {
            if (!Click)
            {
                //마우스가 코인 위에 있음
                Debug.Log("coin");
            }
            else
            {
                //클릭
                if (GameObject.Find("TurnManager").GetComponent<TurnManagerS>().Turn == 2.5f)
                {
                    //내턴이면
                    UseCoin = true;
                    GameObject.Find("TurnManager").GetComponent<TurnManagerS>().ManaCoast1++;
                }
            }
        }

    }

    public void AI_UseCoin()
    {
        UseCoin = true;
    }

}
