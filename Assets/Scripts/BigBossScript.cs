using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BigBossScript : MonoBehaviour
{
//
//		bool redDestroyed = false;
//		bool blueDestroyed = false;
		public OrbManager redOrb;
		public OrbManager blueOrb;
		public GameObject redShield;
		public GameObject blueShield;
	
	
		public float speed = 8f;
		public Transform[] nodes;
		private int currentNode = 0;
		private bool nextNode;
		private float nodeRadius = .5f;
		public LayerMask chaserPosition;
		private Vector2 velocity;
		public GameObject goal;
		public BombSpawner bombSpawner;
		int index = 2;
		public GameObject spikes;
		public BossHitbox hitbox;
	
	
		void FixedUpdate ()
		{
				if (hitbox.health <= 0) {
						goal.GetComponent<CircleCollider2D> ().enabled = true;
						goal.GetComponent<MovingPlatforms> ().enabled = true;
						this.RemoveChild ();
			
						//goal.transform.parent = null;
						Destroy (this.gameObject);
				}
		
				if (redOrb.destroyed) {
						redShield.SetActive (false);
				}
				if (blueOrb.destroyed) {
						blueShield.SetActive (false);
				}
				if (blueOrb.destroyed && redOrb.destroyed) {
						hitbox.ShieldDown (true);
				}
			
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
						
				}
		
		}
	
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Bomb") {
						col.gameObject.GetComponent<BombExploder> ().startFuse ();
				}
		}
	
		public void Enrage ()
		{
				speed += 1f;
				bombSpawner.enemies [index] = spikes;
				index++;
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
