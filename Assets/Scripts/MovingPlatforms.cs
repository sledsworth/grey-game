using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour
{

		public float speed = 10f;
		public Transform[] nodes;
		private int currentNode = 0;
		private bool nextNode;
		public bool onPlatform = false;
		private float nodeRadius = .1f;
		public LayerMask movingPlatform;
		private GameObject player;
		private Vector2 velocity;
		public bool isContinuous = true;
		
	
		void FixedUpdate ()
		{
				if (isContinuous || onPlatform) {
						velocity = (nodes [currentNode].position - this.transform.position).normalized * speed * Time.deltaTime;
				
						if ((this.transform.position - nodes [currentNode].position).magnitude <= speed * Time.deltaTime) {
								velocity = Vector2.ClampMagnitude (velocity, (nodes [currentNode].position - this.transform.position).magnitude);
						}
				
						nextNode = Physics2D.OverlapCircle (nodes [currentNode].position, nodeRadius, movingPlatform);
				
						if (nextNode == true) {
								if (currentNode < nodes.Length - 1) {
										currentNode++;
								} else {
										currentNode = 0;
								}
						}
						if (onPlatform) {
								player.transform.Translate (velocity);
								
						}

						this.transform.Translate (velocity);
				} else {
						velocity = (nodes [0].position - this.transform.position).normalized * speed * Time.deltaTime;
					
						if ((this.transform.position - nodes [0].position).magnitude <= speed * Time.deltaTime) {
								velocity = Vector2.ClampMagnitude (velocity, (nodes [0].position - this.transform.position).magnitude);
						}
			
						this.transform.Translate (velocity);
			
				}
		}
		
		public void AddNode (Transform node)
		{
				nodes [nodes.Length] = node;
		}
		
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Player") {
						player = col.gameObject;
						col.gameObject.GetComponent<playerController1> ().platform = this.GetComponent<MovingPlatforms> ();
						onPlatform = true;
				} else if (col.gameObject.tag == "Bomb") {
						col.gameObject.transform.parent = this.transform;
						col.gameObject.GetComponent<Rigidbody2D>().fixedAngle = true;
				}
			
		}
		
		void OnCollisionExit2D (Collision2D col)
		{
				if (col.gameObject.tag == "Player") {
						col.gameObject.GetComponent<playerController1> ().platform = null;
						onPlatform = false;
				} else if (col.gameObject.tag == "Bomb") {
						col.gameObject.transform.parent = null;
						col.gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
			
				}
		}
	
}
