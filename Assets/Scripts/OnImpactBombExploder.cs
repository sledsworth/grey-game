using UnityEngine;
using System.Collections;

public class OnImpactBombExploder : MonoBehaviour
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
				if (col.gameObject.tag != "Player") {
						Instantiate (explosion, center.position, this.transform.rotation);
						Destroy (this.gameObject);
				}
		}
}
