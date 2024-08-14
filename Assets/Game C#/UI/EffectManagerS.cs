using UnityEngine;

public class EffectManagerS : MonoBehaviour
{
    float dtime1, dtime2;
    public GameObject DrawingEffect, NoMana, FullField, Die, Healing, NoCard, Heart, Firecracker;
    public GameObject[] SwordEffects;
    public bool[] DoSwordEffect = { false, false, false, false, false, false };

    bool IsGameOpening = true;
    public Vector3 EffectAddVector;

    float TimeD = 0;

    public int FirecrackerNum = 0;
    float FirecrackerTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        EffectAddVector = new Vector3(0, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOpening)
        {
            GameOpeningEffect();
        }
        if (FirecrackerNum > 0)
        {
            FirecrackerTime -= Time.deltaTime;
            if (FirecrackerTime < 0)
            {
                FirecrackerTime = Random.Range(0.4f, 0.8f);
                FirecrackerNum--;
                GameObject myInstance = Instantiate(Firecracker);
                myInstance.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-2.3f, -1.7f), Random.Range(-2.0f, 2.0f));

            }
        }

    }
    void GameOpeningEffect()
    {
        TimeD += Time.deltaTime;
        if (!DoSwordEffect[0] && TimeD > 3.83f)
        {
            DoSwordEffect[0] = true;
            GameObject myInstance = Instantiate(SwordEffects[0]);
            myInstance.transform.position = new Vector3(-0.19f, 2.84f, -0.22f);
        }
        if (!DoSwordEffect[1] && TimeD > 4.29f)
        {
            DoSwordEffect[1] = true;
            GameObject myInstance = Instantiate(SwordEffects[1]);
            myInstance.transform.position = new Vector3(-1.68f, 3.5f, -0.14f);
            myInstance.transform.rotation = Quaternion.Euler(-813, 131, -6);
            myInstance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (!DoSwordEffect[2] && TimeD > 4.95f)
        {
            DoSwordEffect[2] = true;
            GameObject myInstance = Instantiate(SwordEffects[2]);
            myInstance.transform.position = new Vector3(-2.55f, 2.9f, 0.02f);
            myInstance.transform.rotation = Quaternion.Euler(30, 0, 0);
        }
        if (!DoSwordEffect[3] && TimeD > 5.54f)
        {
            DoSwordEffect[3] = true;
            GameObject myInstance = Instantiate(SwordEffects[3]);
            myInstance.transform.position = new Vector3(0.379f, 3.381f, -0.156f);
        }
        if (!DoSwordEffect[4] && TimeD > 6.05f)
        {
            DoSwordEffect[4] = true;
            GameObject myInstance = Instantiate(SwordEffects[5]);
            myInstance.transform.position = new Vector3(2.9f, 3.03f, 0.55f);
            //myInstance.transform.position = new Vector3(2.06f, 2.85f, 0.41f);
            myInstance.transform.rotation = Quaternion.Euler(-77.3f, -80.4f, 0);
        }
        if (!DoSwordEffect[5] && TimeD > 6.5f)
        {
            DoSwordEffect[5] = true;
            GameObject myInstance = Instantiate(SwordEffects[5]);
            myInstance.transform.position = new Vector3(2.28f, 3.26f, 0.61f);
            myInstance.transform.rotation = Quaternion.Euler(61, 143, 32);
            myInstance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            IsGameOpening = false;
        }
    }

    public void EffectJen(string EffectName, Vector3 V)
    {
        if (EffectName == "NoMana")
        {
            GameObject myInstance = Instantiate(NoMana);
            myInstance.transform.position = V + EffectAddVector;
        }
        else if (EffectName == "FullField")
        {
            GameObject myInstance = Instantiate(FullField);
            myInstance.transform.position = V + EffectAddVector;
        }
        else if (EffectName == "Die")
        {
            GameObject myInstance = Instantiate(Die);
            myInstance.transform.position = V;
            myInstance.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (EffectName == "Healing")
        {
            GameObject myInstance = Instantiate(Healing);
            myInstance.transform.position = V;

        }
        else if (EffectName == "Heart")
        {
            GameObject myInstance = Instantiate(Heart);
            myInstance.transform.position = V;
        }
        else if (EffectName == "Firecracker")
        {
            FirecrackerNum++;
        }
        else
        {
            Debug.LogError("EffectName ¿À·ù");
        }
    }

    public void JENDrawingEffect(bool IsMe, bool Can = true)
    {

        if (Can)
        {
            GameObject myInstance = Instantiate(DrawingEffect);
            if (IsMe)
            {
                myInstance.transform.position = new Vector3(9, 2, -6);
            }
            else
            {
                myInstance.transform.position = new Vector3(9, 2, 6);
            }
        }
        else
        {
            GameObject myInstance = Instantiate(NoCard);
            if (IsMe)
            {
                myInstance.transform.position = new Vector3(9, 2, -6);
            }
            else
            {
                myInstance.transform.position = new Vector3(9, 2, 6);
            }
        }


    }
}
