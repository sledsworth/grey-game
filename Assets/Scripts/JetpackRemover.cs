using UnityEngine;
using System.Collections;

public class JetpackRemover : MonoBehaviour
{
		
		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.gameObject.tag == "Player") {
						col.gameObject.GetComponent<playerController1> ().RemoveJetPack ();
				}
		}
}
