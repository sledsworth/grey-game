using UnityEngine;
using System.Collections;

public class ChaserTrigger : MonoBehaviour
{

		public ChaserController controller;
		public BombSpawner bombSpawner;

		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.gameObject.tag == "Player") {
						controller.enabled = true;
						bombSpawner.enabled = true;
				}
		}
	
		void OnTriggerExit2D (Collider2D col)
		{
				if (col.gameObject.tag == "Player") {
						controller.enabled = false;
						bombSpawner.enabled = false;
				}
		}
	
}
