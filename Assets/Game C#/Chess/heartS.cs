using UnityEngine;

public class heartS : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite h1, h2;
    SpriteRenderer thisImg;

    public int heartNum;

    private void Start()
    {

        thisImg = GetComponent<SpriteRenderer>();
        thisImg.sprite = h1;


    }

    private void Update()
    {
        if (transform.parent.GetComponent<HpBarS>().ShowHP >= heartNum)
        {
            thisImg.sprite = h1;
        }
        else
        {
            thisImg.sprite = h2;
        }

    }
}
