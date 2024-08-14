using UnityEngine;

public class ManaS : MonoBehaviour
{
    public int MEsee;
    Vector3 TargetV = Vector3.zero;
    private void Update()
    {

        if (MEsee <= transform.parent.GetComponent<ManaCountS>().ManaCount)
        {
            TargetV = new Vector3(0, 0, MEsee * 0.5f - 0.5f) + new Vector3(0, 1, 0) * Mathf.Cos(Time.time) * 0.1f;
        }
        else
        {
            TargetV = new Vector3(0, -1, MEsee * 0.5f - 0.5f);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, TargetV, Time.deltaTime * 2);
    }
}
