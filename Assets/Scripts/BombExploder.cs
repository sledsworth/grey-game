using UnityEngine;
using System.Collections;

public class BombExploder : MonoBehaviour
{
		public GameObject explosion;
		public float fuse = 5f;
		public Transform center;
		public bool isLit = false;
	
		// Use this for initialization
		void Start ()
		{
				if (isLit)
						Invoke ("startFuse", fuse);
		}
		
		public void LightFuse ()
		{
				isLit = true;
				Start ();
		}
		
		public void startFuse ()
		{
				Instantiate (explosion, center.position, this.transform.rotation);
				Destroy (this.gameObject);
		}
		
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Weak" || col.gameObject.tag == "Enemy") {
						Instantiate (explosion, center.position, this.transform.rotation);
						Destroy (col.gameObject);
						Destroy (this.gameObject);
				} else if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bomb") {
						LightFuse ();
				} else if (col.gameObject.tag == "Spikes") {
						Instantiate (explosion, center.position, this.transform.rotation);
						Destroy (this.gameObject);
				} else if (col.gameObject.tag == "SpikeItem") {
						Instantiate (explosion, center.position, this.transform.rotation);
						Destroy (this.gameObject);
				}
		}
}
