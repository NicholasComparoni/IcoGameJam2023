using DG.Tweening;
using UnityEngine;

namespace ICO321 {
	public class PlayerAim : MonoBehaviour {
		[SerializeField] private GameObject turret;

		private void Update() {
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var direction = mousePos - transform.position;
			float angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			turret.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
		}
	}
}