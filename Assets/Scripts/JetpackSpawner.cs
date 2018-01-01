using UnityEngine;
using System.Collections;

public class JetpackSpawner : MonoBehaviour
{

		//public GameObject jetpack;
		public LayerMask spawn;
		public float radius = 1f;
		bool isSpawned = true;
		public GameObject jetPackPrefab;
	
		// Update is called once per frame
		void Update ()
		{
				isSpawned = Physics2D.OverlapCircle (this.transform.position, radius, spawn);
				if (!isSpawned) {
						Instantiate (jetPackPrefab, this.transform.position, this.transform.rotation);
				}
		}
}
