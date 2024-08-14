using UnityEngine;

public class CardM_S : MonoBehaviour
{


    public void yourCardDrawEnd()
    {
        transform.parent.GetComponent<YouCardP_S>().delThisCard();
    }
}
