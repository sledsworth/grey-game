using UnityEngine;
using System.Collections;

public class WeakWallController : MonoBehaviour
{

		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Explosion") {
						Destroy (this.gameObject);
				} 
		}
}
