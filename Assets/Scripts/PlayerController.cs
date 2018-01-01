using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float horizontalSpeed = 0.1f;
	public float verticalSpeed = 0.1f;
	public float threshold = 0f;

	GameObject groundedOn = null;
	private bool isGrounded = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			moveRight ();
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveLeft ();
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log("Is Grounded: " + isGrounded);
			if (isGrounded) {
				moveUp ();
			}
		}
		
	}

	void FixedUpdate() {

	}

	void OnCollisionEnter2D(Collision2D col) {
		Debug.Log (col.gameObject.GetComponent<Rigidbody2D>().isKinematic);
		if (col.gameObject.GetComponent<Rigidbody2D>().isKinematic) //Only want kinematic rigidbodies
		{
			foreach(ContactPoint2D contact in col.contacts)
			{
				bool contacts = contact.normal.y > threshold;
				Debug.Log ("Contact: " + contacts);
				Debug.Log (contact.normal.y);
				if(contact.normal.y < threshold)
				{
					Debug.Log("Gets here.");
					isGrounded = true;
					groundedOn = col.gameObject;
					break;
				}
			}
		}
		if (col.gameObject.tag == "blueGoal") {
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject == groundedOn)
		{
			groundedOn = null;
			isGrounded = false;
		}
	}

	void moveUp () {
		transform.GetComponent<Rigidbody2D>().AddForce (Vector3.up * verticalSpeed);
	}

	void moveRight() {
		Vector3 v = transform.position;
		v.x += horizontalSpeed;
		transform.position = v;
	}

	void moveLeft() {
		Vector3 v = transform.position;
		v.x -= horizontalSpeed;
		transform.position = v;
	}
}
