using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinS : MonoBehaviour
{
	static Rigidbody rb;
	public static Vector3 diceVelocity;
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log("∞·¡§" + GameObject.Find("CoinCheck").GetComponent<CoinCheckS>().CoinSideNum);
		diceVelocity = rb.velocity;
		
	}
	public void CoineSpinStart()
	{
		CoinSpine();
		
		
	}
	void CoinSpine()
    {
		GameObject.Find("CoinCheck").GetComponent<CoinCheckS>().CoinSpinStart = true;
		float dirX = Random.Range(0, 3000);
		float dirY = Random.Range(0, 3000);
		float dirZ = Random.Range(0, 3000);
		transform.position = new Vector3(0, 5, -3);
		transform.rotation = Quaternion.identity;
		rb.AddForce(transform.up * Random.Range(500, 1000));
		rb.AddTorque(dirX, dirY, dirZ);
	}
}


