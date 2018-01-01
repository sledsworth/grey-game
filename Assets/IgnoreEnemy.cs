using UnityEngine;
using System.Collections;

public class IgnoreEnemy : MonoBehaviour
{

		public Collider2D[] enemies;
		// Update is called once per frame
	
		void Start ()
		{
				foreach (Collider2D col in enemies) {
						Physics2D.IgnoreCollision (this.GetComponent<Collider2D>(), col);
			
				}
		}
	
}
