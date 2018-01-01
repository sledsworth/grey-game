using UnityEngine;
using System.Collections;

public class HorizontalMovement : MonoBehaviour
{
	
		private float origY;
		private float origX;
		private bool max;

		public bool vertical;
		public float distance;
		public float speed;
	
		// Use this for initialization
		void Start ()
		{
				origX = transform.position.x;
				origY = transform.position.y;
		}

		// Update is called once per frame
		void Update ()
		{
				Vector3 platform = transform.position;

				if (vertical) {
						if (platform.y >= origY + distance) {
								max = true;
						} else if (platform.y <= origY) {
								max = false;
						}
				} else {
						if (platform.x >= origX + distance) {
								max = true;
						} else if (platform.x <= origX) {
								max = false;
						}
				}

				if (vertical) {
						if (!max) {
								platform.y += speed;
						} else {
								platform.y -= speed;
						}
						transform.position = platform;
				} else {
						if (!max) {
								platform.x += speed;
						} else {
								platform.x -= speed;
						}
						transform.position = platform;
				}
		}
		
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Player") {
				}
		}
}
