using UnityEngine;

public class TurnButtonS : MonoBehaviour
{
    public bool MouseOnturnButton = false;
    float TargetY = 0, ThisY = 0;
    float Speed = 20;

    private void Update()
    {
        if (Mathf.Abs(TargetY - ThisY) < 1.0)
        {
            ThisY = TargetY;
        }
        else
        {
            if (TargetY > ThisY)
            {
                ThisY += Time.deltaTime * Speed;
            }
            else
            {
                ThisY -= Time.deltaTime * Speed;
            }

        }
        transform.localRotation = Quaternion.Euler(0f, ThisY - 90, 0f);
    }

    public void TurnButtonRotation(float y)
    {
        TargetY = y;
    }





}
