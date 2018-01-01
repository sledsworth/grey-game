using UnityEngine;
using System.Collections; 

public class playerController1 : MonoBehaviour
{
	
		public Animator anim;
		public float maxSpeed = 10.0f;
		public Transform groundCheck;
		public Transform wallCheckFront;
		public Transform wallCheckBack;
		float groundRadius = 0.1f;
		public LayerMask whatIsGround;
		public LayerMask whatIsWall;
		public LayerMask whatIsItem;
		public float jumpForce = 700f;
		public float wallJumpVerticalForce = 1000f;
		public float respawnDelay = 2f;
		public float dashCooldown = 2f;
		public float dashForce = 20f;
		public float dashDuration = 1f;
		public float throwDistance = 700f;
		public float pocketPlatformForce = 50f;
		public Transform bombHoldingHand;
		public bool removeJetpackOnDeath = false;

		bool facingRight = true;
		bool grounded = false;
		bool onWall = false;
		bool doubleJump = false;
		bool hasDash = true;
		bool isDashing = false;
		GameObject item;
		GameObject holding;
		public MovingPlatforms platform;
	
	
		public Transform respawnPoint;
		
	
//		void FixedUpdate ()
//		{
//
//				if (grounded)
//						doubleJump = false;
//
//				onWall = Physics2D.OverlapCircle (wallCheckFront.position, groundRadius, whatIsWall) ||
//						Physics2D.OverlapCircle (wallCheckBack.position, groundRadius, whatIsWall);
//		 		
//				grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
//				float move = Input.GetAxis ("Horizontal");
//				
//				if (!isDashing) {
//						rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
//				}
//				if (move > 0 && !facingRight)
//						Flip ();
//				else if (move < 0 && facingRight)
//						Flip ();
//		}

		void Update ()
		{
				if (grounded)
						doubleJump = false;
		
				onWall = Physics2D.OverlapCircle (wallCheckFront.position, groundRadius, whatIsWall) ||
						Physics2D.OverlapCircle (wallCheckBack.position, groundRadius, whatIsWall);
		
				grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
				float move = Input.GetAxis ("Horizontal");
		
				if (!isDashing) {
						GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
				}
				if (move > 0 && !facingRight)
						Flip ();
				else if (move < 0 && facingRight)
						Flip ();
			
			
			
				
				if (onWall) {
						doubleJump = false;
				}
				if (grounded && (Input.GetButtonDown ("Jump"))) {
						GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
				} else if (onWall && Input.GetButtonDown ("Jump")) {
						doubleJump = false;
						GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, wallJumpVerticalForce));
				} else if (!grounded && !doubleJump && Input.GetButtonDown ("Jump")) {
						GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
						doubleJump = true;
				}
				
				if (holding && Input.GetButtonDown ("Grab")) {
						Throw ();
				}
				if (!holding && item && Input.GetButtonDown ("Grab")) {
						PickUpItem ();
				}
				
			
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
				
				if (hasDash && Input.GetButtonDown ("Dash")) {
						StartCoroutine (Dash ());
				}
				
				anim.SetBool ("Dashing", isDashing);
				anim.SetBool ("Grounded", grounded);
				anim.SetFloat ("Vertical Movement", GetComponent<Rigidbody2D>().velocity.y);
				bool isMoving = true;
				if (GetComponent<Rigidbody2D>().velocity.x == 0.0f)
						isMoving = false;
				anim.SetBool ("Moving", isMoving);
				anim.SetFloat ("Speed", Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x));
				
				anim.SetBool ("On Wall", onWall);
				anim.SetBool ("Double Jump", doubleJump);
		}

		void Flip ()
		{
				facingRight = !facingRight;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
		}
		
		public void RemoveJetPack ()
		{
				if (holding == null)
						return;
				if (holding.tag == "Jetpack") {
						holding.transform.parent = null;
						holding.GetComponent<Rigidbody2D>().isKinematic = false;
						holding.GetComponent<Rigidbody2D>().AddForce (new Vector2 (-.2f * throwDistance, .2f * throwDistance));
						holding = null;
						jumpForce = 900f;
				}
				
		}
		
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Explosion" || col.gameObject.tag == "Spikes") {
						this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
						this.transform.SetParent (null);
						DeathAnimation ();
						if (removeJetpackOnDeath)
								RemoveJetPack ();
				}
				
		}
		
		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.gameObject.tag == "Respawn") {
						DeathAnimation ();
				}
				if (col.gameObject.tag == "Bomb" || col.gameObject.tag == "PocketPlatform" || col.gameObject.tag == "Jetpack") {
						item = col.gameObject;
				}
				if (col.gameObject.tag == "CheckPoint") {
						this.respawnPoint = col.transform;
				}
				
		}
		
		void OnTriggerExit2D (Collider2D col)
		{
				if (col.gameObject.tag == "Bomb" || col.gameObject.tag == "PocketPlatform" || col.gameObject.tag == "Jetpack") {
						item = null;
				}
		}
		
		public void DeathAnimation ()
		{
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ().enabled = false;
				if (platform)
						platform.onPlatform = false;
				anim.SetBool ("Dead", true);
				enabled = false;
				StartCoroutine (RespawnPlayer ());
		}
		
		
		IEnumerator RespawnPlayer ()
		{
				yield return new WaitForSeconds (respawnDelay);
				anim.SetBool ("Dead", false);
				transform.position = respawnPoint.position;
				this.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ().enabled = true;
				enabled = true;
		}
		
		IEnumerator Dash ()
		{
				float time = 0;
				hasDash = false;
				isDashing = true;
				Vector2 dashVector = new Vector2 (facingRight ? dashForce : -dashForce, 0);
				while (dashDuration > time) {
						time += Time.deltaTime;
						GetComponent<Rigidbody2D>().velocity = dashVector;
						if (Input.GetButtonDown ("Jump"))
								break;
						yield return 0;
				}
				isDashing = false;
				yield return new WaitForSeconds (dashCooldown);
				hasDash = true;
		}
		
		void PickUpItem ()
		{
				item.GetComponent<Rigidbody2D>().isKinematic = true;
				if (item.gameObject.tag != "Jetpack") {
						foreach (Collider2D c in item.GetComponents<Collider2D>()) {
								Physics2D.IgnoreCollision (c, this.GetComponent<Collider2D>());
								c.enabled = false;
						}
				} else {
						foreach (Collider2D c in item.GetComponents<Collider2D>()) {
								Physics2D.IgnoreCollision (c, this.GetComponent<Collider2D>());
								jumpForce = 500f;
								//c.enabled = false;
				
						}
				}
				
				
				if (item.tag == "Bomb") {
						item.GetComponent<BombExploder> ().LightFuse ();
				}	
				
				item.transform.position = this.transform.position;
		
				item.transform.parent = bombHoldingHand.transform;
		
				//titem.transform.localScale = new Vector3 (1.5f, 1.5f, 0);
				holding = item;
				item = null;
		}
	
		void Throw ()
		{
				if (holding.tag == "Bomb") {
						holding.transform.parent = null;
						holding.GetComponent<Rigidbody2D>().isKinematic = false;
						float moveX = Input.GetAxis ("Horizontal");
						float moveY = Input.GetAxis ("Vertical");
						holding.GetComponent<Rigidbody2D>().AddForce (new Vector2 (moveX * throwDistance, moveY * throwDistance));
						StartCoroutine (DelayColliders (holding));
				} else if (holding.tag == "PocketPlatform") {
						holding.transform.parent = null;
						float moveX = Input.GetAxis ("Horizontal");
						float moveY = Input.GetAxis ("Vertical");
						holding.GetComponent<Rigidbody2D>().transform.Translate (new Vector2 (pocketPlatformForce * moveX, pocketPlatformForce * moveY));
						StartCoroutine (DelayColliders (holding));
			
				}	
		
		}

		IEnumerator DelayColliders (GameObject item)
		{
				yield return new WaitForSeconds (.02f);
				foreach (Collider2D c in item.GetComponents<Collider2D>()) {
						Physics2D.IgnoreCollision (c, this.GetComponent<Collider2D>(), false);
						c.enabled = true;
						c.enabled = true;
				}
				holding = null;
		}
		
//	void Jump ()
//	{
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			rigidbody.AddForce(Vector3.up * 10,ForceMode.VelocityChange);
//		}
//		
//		if (Input.GetKey (KeyCode.Space)) {
//			if (jumpValue < maxJumpValue) {
//				rigidbody.AddForce(Vector3.up * 10,ForceMode.Acceleration);
//			}
//			
//		}
//		
//	}
	
}
