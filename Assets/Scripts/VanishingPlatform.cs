using UnityEngine;
using System.Collections;

public class VanishingPlatform : MonoBehaviour {
	
	public float timeBeforeVanish = 1f;


	void OnCollisionEnter2D()
	{
			Destroy(GetComponent<Collider2D>().gameObject, timeBeforeVanish);
	}
}
