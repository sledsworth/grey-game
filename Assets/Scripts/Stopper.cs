using UnityEngine;
using System.Collections;

public class Stopper : MonoBehaviour
{

		public Collider2D player;
	
		// Use this for initialization
		void Start ()
		{
				//Physics2D.IgnoreCollision (this.collider2D, player);
		}
	
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Stopper") {
						col.gameObject.GetComponent<SpikeWallMove> ().enabled = false;
				}
		}
	
}
