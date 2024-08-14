using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChessM_S : MonoBehaviour
{
 
    // Start is called before the first frame update
    public List<Vector3> ReturnWorldLocation()
    {
        Transform parent = transform.parent;
        transform.parent = null;
        Vector3 LocalPosition = transform.localPosition;
        Vector3 LocalScale = transform.localScale;
        transform.parent = parent;
        List<Vector3> result = new List<Vector3>();
        result.Add(LocalPosition);
        result.Add(LocalScale);
        return result;
    }

}
