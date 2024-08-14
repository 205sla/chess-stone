using UnityEngine;

public class CardS : MonoBehaviour
{
    public GameObject Back, Front;

    // Start is called before the first frame update
    public void CardTurn()
    {
        Back.SetActive(false);
        Front.SetActive(true);
    }

    public void CardSeeEnd()
    {
        transform.GetComponentInParent<CardMoveS>().MoveME();
        GameObject.Find("CoinManager").GetComponent<CardManagerS>().DoingCardDraw--;
        //GameObject.Find("CardP(Clone)").GetComponent<CardMoveS>().MoveME();
        //Debug.Log("¿Ãµø«ÿ¡‡");
    }
}
