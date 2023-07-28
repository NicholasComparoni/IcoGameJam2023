using System;
using DG.Tweening;
using UnityEngine;

namespace ICO321 {
	public class PlayerAim : MonoBehaviour {
		[SerializeField] private GameObject turret;

		private void Update() {
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float angleDeg = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			turret.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
			//turret.transform.DOLookAt(mousePos, .1f, AxisConstraint.None, Vector3.forward);
		}
	}
}