using UnityEngine;
using System.Collections;

public class SpikeItem : MonoBehaviour
{
		public int health = 4;

		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Explosion") {
						health--;
						if (health <= 0) {
								Destroy (this.gameObject);
				
						}
				}
				if (col.gameObject.tag == "Respawn") {
						Destroy (this.gameObject);
				}
		}
}
