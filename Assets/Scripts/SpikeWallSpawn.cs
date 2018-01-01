using UnityEngine;
using System.Collections;

public class SpikeWallSpawn : MonoBehaviour
{
		public float distanceDropped = -7f;

		public void DropWalls ()
		{
				this.gameObject.transform.Translate (new Vector3 (0f, distanceDropped, 0f));
		}
}
