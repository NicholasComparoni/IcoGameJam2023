using UnityEngine;
using UnityEngine.Serialization;

namespace ICO321 {
	public class AutoRunningCharacter : MonoBehaviour {
		[FormerlySerializedAs("running")] public float movementSpeed = 5f;

		private Rigidbody2D rb;

		private void Start() {
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() { }
	}
}