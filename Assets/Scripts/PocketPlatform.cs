using UnityEngine;
using System.Collections;

public class PocketPlatform : MonoBehaviour
{

		public float timeBeforeDeactive = 2f;
	
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		public void Drop ()
		{
				StartCoroutine ("Fall");
		}
	
		IEnumerator Fall ()
		{
				yield return new WaitForSeconds (timeBeforeDeactive);
				this.GetComponent<Rigidbody2D>().isKinematic = false;
		}
		
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag != "Player") {
						Fall ();
				}
		}
}
