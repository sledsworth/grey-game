using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{

		public Transform respawnPoint;
		public Transform enemySpawn;
		public Transform enemySpawn1;

		void OnTriggerEnter2D (Collider2D col)
		{
				
//				if (col.gameObject.tag == "Enemy") {
//						col.gameObject.GetComponent<EnemyController> ().Respawn ();
//						Debug.Log (col.gameObject.name);
//				}
		}
		
		

}
