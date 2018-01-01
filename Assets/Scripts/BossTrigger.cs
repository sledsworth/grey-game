using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour
{

		public BossScript controller;
		public GameObject spikeWalls;
		public Camera cam;
	
	
		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.gameObject.tag == "Player") {
						controller.enabled = true;
			
						spikeWalls.GetComponent<SpikeWallSpawn> ().DropWalls ();
						cam.fieldOfView = 60;
						this.GetComponent<Collider2D>().enabled = false;
			
				}
		}
	
}
