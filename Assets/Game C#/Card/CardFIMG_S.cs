using UnityEngine;

public class CardFIMG_S : MonoBehaviour
{
    public void SetMouseNumCardFIMG_S(bool IsClick)
    {
        transform.GetComponentInParent<CardMoveS>().SetMouseNumCard(IsClick);
    }

}
