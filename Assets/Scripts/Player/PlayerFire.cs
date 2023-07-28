using System;
using UnityEngine;

namespace ICO321 {
	public class PlayerFire : MonoBehaviour {
		public GameObject bullet;
		public Transform turret;
		public Transform muzzle;

		private void Update() {
			if (Input.GetButtonDown("Fire1")) {
				var newBullet = PoolManager.Instance.GetItem(bullet.name).GetComponent<Bullet>();
				newBullet.transform.position = muzzle.position;
				newBullet.transform.rotation = turret.rotation;
				newBullet.Direction = muzzle.transform.position - turret.transform.position;
			}
		}
	}
}