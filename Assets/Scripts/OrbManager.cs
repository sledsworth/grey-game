using UnityEngine;
using System.Collections;

public class OrbManager : MonoBehaviour
{

		public bool destroyed = false;
	
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Explosion") {
						this.enabled = false;
						destroyed = true;
				} else if (col.gameObject.tag == "Bomb") {
						col.gameObject.GetComponent<BombExploder> ().startFuse ();
				}
		}
	
	
}
