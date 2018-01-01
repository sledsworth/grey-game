using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BossScript : MonoBehaviour
{

		public float speed = 3f;
		public Transform[] nodes;
		private int currentNode = 0;
		private bool nextNode;
		private float nodeRadius = .5f;
		public LayerMask chaserPosition;
		private Vector2 velocity;
		public int healthPoints = 4;
		public GameObject goal;
		public BombSpawner bombSpawner;
		
	
		void FixedUpdate ()
		{
				if (nodes.Length != 0) {
			
						velocity = (nodes [currentNode].position - this.transform.position).normalized * speed * Time.deltaTime;
			
						if ((this.transform.position - nodes [currentNode].position).magnitude <= speed * Time.deltaTime) {
								velocity = Vector2.ClampMagnitude (velocity, (nodes [currentNode].position - this.transform.position).magnitude);
						}
			
						nextNode = Physics2D.OverlapCircle (nodes [currentNode].position, nodeRadius, chaserPosition);
						if (nextNode == true) {
								if (currentNode < nodes.Length - 1) {
										currentNode++;
								} else {
										currentNode = 0;
								}
						}
			
						this.transform.Translate (velocity);
						if (healthPoints <= 0) {
								goal.GetComponent<CircleCollider2D> ().enabled = true;
								goal.GetComponent<MovingPlatforms> ().enabled = true;
								this.RemoveChild ();
								
								//goal.transform.parent = null;
								Destroy (this.gameObject);
								
						}
				}
		
		}
	
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Explosion") {
						healthPoints -= 1;
						Enrage ();
				} else if (col.gameObject.tag == "Bomb") {
						col.gameObject.GetComponent<BombExploder> ().startFuse ();
				}
		}
		
		void Enrage ()
		{
				speed += 1f;
				bombSpawner.spawnTime -= .4f;
		}
		
		public void RemoveChild ()
		{
				List<Transform> unparent = new List<Transform> (transform.childCount);
		
				foreach (Transform child in transform) {
						if (child.tag == "Finish")
								unparent.Add (child);
				}
		
				foreach (Transform child in unparent) {
						child.parent = null;
				}
		}
	
}
