using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{

		public float delay = 1.5f;
	
		// Use this for initialization
		void Start ()
		{
				Invoke ("endExplosion", delay);
		}
	
		
		void endExplosion ()
		{
				Destroy (this.gameObject);
		}
		
		void OnCollisionExit2D (Collision2D col)
		{
				if (col.gameObject.tag == "Weak" || col.gameObject.tag == "Enemy") {
						Destroy (col.gameObject);
				} else if (col.gameObject.tag == "Bomb") {
						col.gameObject.GetComponent<BombExploder> ().startFuse ();
				}
		}
}
