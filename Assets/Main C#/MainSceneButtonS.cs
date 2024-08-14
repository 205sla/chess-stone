using UnityEngine;
using UnityEngine.UI;

public class MainSceneButtonS : MonoBehaviour
{
    public int buttonNUM;
    Image thisImg;
    public Sprite IMG_OUT, IMG_IN;

    private Animator animator;

    bool ThisSee = false;
    // Start is called before the first frame update
    void Start()
    {
        thisImg = GetComponent<Image>();

        if (buttonNUM != 4)
        {

            animator = GetComponent<Animator>();
        }
    }
    private void Update()
    {
        if (!ThisSee)
        {
            if (GameObject.Find("MainSceneManager").GetComponent<MainSceneManagerS>().CanControlButton)
            {
                ThisSee = true;
                thisImg.sprite = IMG_OUT;
            }
        }
    }

    // Update is called once per frame
    public void MouseIN()
    {
        if (ThisSee)
        {

            thisImg.sprite = IMG_IN;
        }

    }
    public void MouseOUT()
    {
        if (ThisSee)
        {

            thisImg.sprite = IMG_OUT;
        }
    }

    public void SetButton(bool See)
    {
        if (buttonNUM != 4)
        {
            animator.SetBool("IsSee", See);
        }


    }



}
