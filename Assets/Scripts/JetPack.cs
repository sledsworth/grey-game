using UnityEngine;
using System.Collections;

public class JetPack : MonoBehaviour
{

		public Transform jetpackRespawn;
	
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Spikes") {
						Destroy (this.gameObject);
				}
		}
		
}
