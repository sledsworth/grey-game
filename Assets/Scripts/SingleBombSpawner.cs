using UnityEngine;
using System.Collections;

public class SingleBombSpawner : MonoBehaviour
{

		
		public GameObject[] enemies;		// Array of enemy prefabs.
		GameObject item;
	
		
		void FixedUpdate ()
		{
				if (item == null) {
						Spawn ();
				}
		}
	
		void Spawn ()
		{
				// Instantiate a random enemy.
				int enemyIndex = Random.Range (0, enemies.Length);
				item = (GameObject)Instantiate (enemies [enemyIndex], transform.position, transform.rotation);
		
				// Play the spawning effect from all of the particle systems.
				foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>()) {
						p.Play ();
				}
		}
}
