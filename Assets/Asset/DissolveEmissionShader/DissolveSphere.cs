using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSphere : MonoBehaviour {

    Material mat;
    bool IsHide = false;
    float T = 0;

    private void Start() {
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_DissolveAmount", 0);
    }

    private void Update() {
        if (IsHide) {
            if (T < 3) {
                T += Time.deltaTime;
                mat.SetFloat("_DissolveAmount", T/3);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void CoinHide() {
        IsHide = true;
    }

}