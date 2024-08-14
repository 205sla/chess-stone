using UnityEngine;

public class KingHpHeartS : MonoBehaviour
{
    public Sprite h1, h2;
    SpriteRenderer thisImg;
    public int heartNum = 0;

    public int KingHeartNum;
    // Start is called before the first frame update
    void Start()
    {
        thisImg = GetComponent<SpriteRenderer>();
        thisImg.sprite = h1;
    }

    private void Update()
    {
        if (transform.GetComponentInParent<KingHpS>().ShowHP >= heartNum)
        {
            thisImg.sprite = h1;
        }
        else
        {
            thisImg.sprite = h2;
        }
    }



}
