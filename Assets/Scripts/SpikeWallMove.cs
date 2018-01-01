using UnityEngine;
using System.Collections;

public class SpikeWallMove : MonoBehaviour
{
		public Transform starter;
		public float speed;
		public Rigidbody2D[] spikes;
		private float nodeRadius = .1f;
		public LayerMask stopper;
		public Transform stopTransform;
		bool atEnd = false;
		public Collider2D[] enemies;
		// Update is called once per frame
		
		void Start ()
		{
				foreach (Collider2D col in enemies) {
						Physics2D.IgnoreCollision (this.GetComponent<Collider2D>(), col);
				
				}
		}
		void FixedUpdate ()
		{
				this.transform.Translate (new Vector2 (speed * Time.deltaTime, 0f));
				atEnd = Physics2D.OverlapCircle (this.transform.position, nodeRadius, stopper);
				if (atEnd) {
						this.transform.position = starter.position;
				}
		} 
		
		void OnCollisionEnter2D (Collision2D col)
		{
				
				if (col.gameObject.tag == "Stopper") {
						
						this.transform.position = starter.position;
				}
		}
		
		void BreakWall ()
		{
				this.GetComponent<Collider2D>().enabled = false;
				foreach (Rigidbody2D spike in spikes) {
						spike.isKinematic = false;
				}
				StartCoroutine ("RemoveWall");
				
		}
		
		IEnumerator RemoveWall ()
		{
				yield return new WaitForSeconds (4);
				Destroy (this.gameObject);
		}
}
