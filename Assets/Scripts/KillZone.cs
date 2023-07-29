using System.Collections;
using UnityEngine;

namespace ICO321 {
	public class KillZone : MonoBehaviour {
		private IEnumerator Start() {
			yield return null;
			yield return null;
			transform.position = new Vector3((BoundariesManager.Instance.bounds.min.x - 1.5f), transform.position.y, transform.position.z);
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.GetComponent<PlayerHealth>() != null) {
				other.gameObject.GetComponent<PlayerHealth>().Kill();
			}
			if (other.gameObject.GetComponent<EnemyHealth>() != null) {
				other.gameObject.GetComponent<EnemyHealth>().Kill();
			}
		}
	}
}