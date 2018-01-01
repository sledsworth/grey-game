using UnityEngine;
using System.Collections;


public class ChaserController : MonoBehaviour
{

		public float speed = 3f;
		public Transform[] nodes;
		private int currentNode = 0;
		private bool nextNode;
		private float nodeRadius = .5f;
		public LayerMask chaserPosition;
		private Vector2 velocity;
		public float health = 3f;
	
	
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
				}
				
		}
		
		
}
