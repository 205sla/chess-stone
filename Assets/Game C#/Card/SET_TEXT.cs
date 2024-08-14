using UnityEngine;

public class SET_TEXT : MonoBehaviour
{
    TextMesh textMesh;
    public string T;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = T;
    }

    // Update is called once per frame


}
