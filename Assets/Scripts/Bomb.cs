using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	


		public void SetColliderEnable (bool action)
		{
				foreach (Collider2D c in GetComponents<Collider2D>()) {
						c.enabled = action;
				}
		}
		
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Explosion") {
						this.gameObject.GetComponent<BombExploder> ().startFuse ();
						Destroy (this.gameObject);
				}
		}
		
		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.gameObject.tag == "Respawn") {			
						Destroy (this.gameObject);
				}
		}
}

