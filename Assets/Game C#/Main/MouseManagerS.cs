using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseManagerS : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    bool TurnButton = false;
    // Update is called once per frame
    void Update()
    {
        TurnButton = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider != null)
            {
                if (hit.collider.name == "CardlFrontIMG")
                {
                    hit.collider.GetComponent<CardFIMG_S>().SetMouseNumCardFIMG_S(Input.GetMouseButtonDown(0));
                }
                else
                {
                    if (hit.collider.name == "TurnButton")
                    {
                        TurnButton = true;
                    }
                    else if (hit.collider.name == "CoinB")
                    {
                        GameObject.Find("CoinB").GetComponent<CoinBS>().CoinBClick(Input.GetMouseButtonDown(0));
                    }
                    else if (hit.collider.name == "Rebutton" && Input.GetMouseButtonDown(0))
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                    GameObject.Find("CoinManager").GetComponent<CardManagerS>().MouseNULL();
                }
            }
            else
            {
                GameObject.Find("CoinManager").GetComponent<CardManagerS>().MouseNULL();
            }

        }
        else
        {
            GameObject.Find("CoinManager").GetComponent<CardManagerS>().MouseNULL();
        }


        if (Input.GetMouseButton(0))
        {
            GameObject.Find("TurnButton").GetComponent<TurnButtonS>().MouseOnturnButton = TurnButton;
        }
        else
        {
            GameObject.Find("TurnButton").GetComponent<TurnButtonS>().MouseOnturnButton = false;
        }
    }
}
