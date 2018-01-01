using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

		public float movement = 1;
		public bool jump = false;
		public float jumpSpeed = 5;
		public float jumpHeight = 400f;
		public float maxSpeed = 8f;
		public Transform spawn;
		
		// Use this for initialization
		void Start ()
		{
				if (jump) {
						InvokeRepeating ("JumpLoop", 0f, jumpSpeed);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				GetComponent<Rigidbody2D>().velocity = new Vector2 (movement, GetComponent<Rigidbody2D>().velocity.y);
				
				if (Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x) > maxSpeed || (GetComponent<Rigidbody2D>().velocity.y) > maxSpeed) {
						// clamp velocity:
						Vector3 newVelocity = GetComponent<Rigidbody2D>().velocity.normalized;
						newVelocity *= maxSpeed;
						GetComponent<Rigidbody2D>().velocity = newVelocity;
				} else if (GetComponent<Rigidbody2D>().velocity.y < -maxSpeed) {
						Vector3 newVelocity = GetComponent<Rigidbody2D>().velocity.normalized;
						newVelocity *= maxSpeed + 2;
						GetComponent<Rigidbody2D>().velocity = newVelocity;
				}
		}
		
		void JumpLoop ()
		{
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (GetComponent<Rigidbody2D>().velocity.x, jumpHeight));
		}
		
		
		void OnTriggerEnter2D (Collider2D col)
		{
		
				if (col.gameObject.tag == "Respawn") {
						this.transform.position = spawn.position;
				}
		}
		
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Explosion") {
						Destroy (this.gameObject);
				} 
		}
		
//		public void Respawn ()
//		{
//				Debug.Log ("Gets here");
//				this.transform.position = spawn.transform.position;
//		}

}
