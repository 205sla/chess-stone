using UnityEngine;

public class KingAniControl : MonoBehaviour
{
    private Animator animator;
    float TimeD = 0, TimeBack = 0, KingSetTime = 0;
    bool movement1 = false, movement2 = false, movement3 = false, KingBack = false;
    int KingNum, KingSetNum = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        KingMovement();
        KingBackMove();
        KingSetMove();
    }

    void KingSetMove()
    {
        if (KingSetNum != 0)
        {
            KingSetTime += Time.deltaTime;
            if (KingSetNum == 1)
            {
                if (KingSetTime < 1)
                {
                    transform.rotation = Quaternion.Euler(0, 90 - KingSetTime * 90, 0);
                }
                else if (KingSetTime < 2)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.position = new Vector3(-5f, 1.730997f, (KingSetTime - 1) * 7);
                }
                else if (KingSetTime < 3)
                {
                    transform.position = new Vector3(-5f, 1.730997f, 7);
                    transform.rotation = Quaternion.Euler(0, (KingSetTime - 2) * 90, 0);
                }
                else if (KingSetTime < 4)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    transform.position = new Vector3(-5f + (KingSetTime - 3) * 5, 1.730997f, 7);
                }
                else if (KingSetTime < 5)
                {
                    transform.position = new Vector3(0, 1.730997f, 7);
                    transform.rotation = Quaternion.Euler(0, 90 + (KingSetTime - 4) * 90, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    KingSetNum = 0;
                    transform.GetComponentInChildren<KingHpS>().ShowHpStart();
                }
            }
            else if (KingSetNum == 2)
            {
                if (KingSetTime < 1)
                {
                    transform.rotation = Quaternion.Euler(0, 90 + KingSetTime * 90, 0);
                }
                else if (KingSetTime < 2)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.position = new Vector3(-5f, 1.730997f, (KingSetTime - 1) * -6);
                }
                else if (KingSetTime < 3)
                {
                    transform.position = new Vector3(-5f, 1.730997f, -6);
                    transform.rotation = Quaternion.Euler(0, (KingSetTime - 2) * -90 + 180, 0);
                }
                else if (KingSetTime < 4)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    transform.position = new Vector3(-5f + (KingSetTime - 3) * 5, 1.730997f, -6);
                }
                else if (KingSetTime < 5)
                {
                    transform.position = new Vector3(0, 1.730997f, -6);
                    transform.rotation = Quaternion.Euler(0, 90 - (KingSetTime - 4) * 90, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    KingSetNum = 0;
                    transform.GetComponentInChildren<KingHpS>().ShowHpStart();
                }
            }
            else if (KingSetNum == 3)
            {
                if (KingSetTime < 1)
                {
                    transform.rotation = Quaternion.Euler(0, 270 + KingSetTime * 90, 0);
                }
                else if (KingSetTime < 2)
                {
                    transform.rotation = Quaternion.Euler(0, 360, 0);
                    transform.position = new Vector3(5f, 1.730997f, (KingSetTime - 1) * 7);
                }
                else if (KingSetTime < 3)
                {
                    transform.position = new Vector3(5f, 1.730997f, 7);
                    transform.rotation = Quaternion.Euler(0, (KingSetTime - 2) * -90 + 360, 0);
                }
                else if (KingSetTime < 4)
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    transform.position = new Vector3(5f + (KingSetTime - 3) * -5, 1.730997f, 7);
                }
                else if (KingSetTime < 5)
                {
                    transform.position = new Vector3(0, 1.730997f, 7);
                    transform.rotation = Quaternion.Euler(0, 270 - (KingSetTime - 4) * 90, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    KingSetNum = 0;
                    transform.GetComponentInChildren<KingHpS>().ShowHpStart();
                }
            }
            else if (KingSetNum == 4)
            {
                if (KingSetTime < 1)
                {
                    transform.rotation = Quaternion.Euler(0, 270 - KingSetTime * 90, 0);
                }
                else if (KingSetTime < 2)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.position = new Vector3(5f, 1.730997f, (KingSetTime - 1) * -6);
                }
                else if (KingSetTime < 3)
                {
                    transform.position = new Vector3(5f, 1.730997f, -6);
                    transform.rotation = Quaternion.Euler(0, 180 + (KingSetTime - 2) * 90, 0);
                }
                else if (KingSetTime < 4)
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    transform.position = new Vector3(5f + (KingSetTime - 3) * -5, 1.730997f, -6);
                }
                else if (KingSetTime < 5)
                {
                    transform.position = new Vector3(0, 1.730997f, -6);
                    transform.rotation = Quaternion.Euler(0, 270 + (KingSetTime - 4) * 90, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 360, 0);
                    KingSetNum = 0;
                    transform.GetComponentInChildren<KingHpS>().ShowHpStart();
                }
            }
            else
            {
                Debug.LogError("오류");
            }
        }
    }
    void KingMovement()
    {
        if (movement1)
        {
            TimeD += Time.deltaTime;
            if (TimeD > 0.5f)
            {
                movement1 = false;
                animator.SetBool("movement1", false);
                TimeD = 0;
            }
        }
        if (movement2)
        {
            TimeD += Time.deltaTime;
            if (TimeD > 0.2f)
            {
                movement2 = false;
                animator.SetBool("movement2", false);
                TimeD = 0;
            }
        }
        if (movement3)
        {
            TimeD += Time.deltaTime;
            if (TimeD > 0.7f)
            {
                movement3 = false;
                animator.SetBool("movement3", false);
                TimeD = 0;
            }
        }
        
    }
    void KingBackMove()
    {
        if (KingBack)
        {
            TimeBack += Time.deltaTime;
            if (KingNum == 1)
            {
                if (TimeBack < 2.0f)
                {
                    transform.position = new Vector3(-3f - TimeBack, 1.730997f, 0f);
                }
                else
                {
                    transform.position = new Vector3(-5f, 1.730997f, 0f);
                    KingBack = false;
                }
            }
            else
            {
                if (TimeBack < 2.0f)
                {
                    transform.position = new Vector3(3f + TimeBack, 1.730997f, 0f);
                }
                else
                {
                    transform.position = new Vector3(5f, 1.730997f, 0f);
                    KingBack = false;
                }
            }
        }
    }

    public void Ani_King(int n)
    {
        if (animator != null)
        {
            if (n == 1)
            {
                animator.SetBool("movement1", true);
                movement1 = true;
                TimeD = 0;
            }
            else if (n == 2)
            {
                animator.SetBool("movement2", true);
                movement2 = true;
                TimeD = 0;
            }
            else if (n == 3)
            {
                animator.SetBool("movement3", true);
                movement3 = true;
                TimeD = 0;
            }
            else if (n == 4)
            {
                animator.SetBool("movement4", true);
                TimeD = 0;
            }
            else
            {
                Debug.LogError("애니메이션 오류(파라미터)");
            }
        }
        else
        {
            Debug.LogError("애니메이션 오류(애니메이션컨틀로러)");
        }
    }
    public void BackKing(int n)
    {
        KingNum = n;
        KingBack = true;
    }
    public void SetKing(bool ME)
    {
        if (KingNum == 1)
        {
            KingSetNum = ME ? 2 : 1;
        }
        else if (KingNum == 2)
        {
            KingSetNum = ME ? 4 : 3;
        }
        else
        {
            Debug.LogError("오류");
        }
    }
}
