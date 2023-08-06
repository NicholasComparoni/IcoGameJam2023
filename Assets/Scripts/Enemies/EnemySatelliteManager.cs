using System.Collections.Generic;
using UnityEngine;

namespace ICO321 {
	public class EnemySatelliteManager : MonoBehaviour {
		[SerializeField] private EnemyHealth enemyHealth;
		[SerializeField] private EnemySatellite satellitePrefab;
		[SerializeField] private List<EnemySatellite> satellites = new List<EnemySatellite>();
		[SerializeField] private float rotationSpeed = 20;
		[SerializeField] private Collider2D enemyCollider;
		private float radius;
		private float angle;
		private List<GameObject> satelliteTargetPositions = new List<GameObject>();

		private void Awake() {
			angle = 360;
		}

		private void OnEnable() {
			enemyHealth.OnDeath += OnDeath;
		}

		private void OnDisable() {
			enemyHealth.OnDeath -= OnDeath;
		}

		private void Start() {
			radius = enemyCollider.bounds.size.magnitude;
			transform.localRotation = Quaternion.identity;
			gameObject.name = $"{enemyHealth.gameObject.name} Satellite Manager {radius}";
		}

		private void Update() {
			transform.Rotate(Vector3.forward, rotationSpeed * (satelliteTargetPositions.Count) * Time.deltaTime);
		}

		public void AddSatellite(TypesUtility.Phase phase) {
			var newTarget = new GameObject($"Target {satelliteTargetPositions.Count}");

			newTarget.transform.SetParent(transform);
			newTarget.transform.localPosition = Vector3.right * radius;
			satelliteTargetPositions.Add(newTarget);

			var newSatellite = PoolManager.Instance.GetItem(satellitePrefab.name);

			newSatellite.transform.SetParent(null);
			newSatellite.transform.position = transform.position;
			var enemySatellite = newSatellite.GetComponent<EnemySatellite>();
			enemySatellite.Setup(phase, enemyHealth);
			enemySatellite.followTarget = newTarget.transform;
			enemySatellite.Destroyed += OnSatelliteDestroyed;
			satellites.Add(enemySatellite);
			newSatellite.SetActive(true);
			RearrangeTargets();
		}

		private void OnDeath() {
			for (int i = satellites.Count - 1; i >= 0; i--) {
				satellites[i].GetComponent<EnemyHealth>().Kill();
			}

			Destroy(gameObject);
		}

		private void OnSatelliteDestroyed(EnemySatellite whichSatellite) {
			//Debug.Log($"Satellite destroyed");
			whichSatellite.Destroyed -= OnSatelliteDestroyed;
			satelliteTargetPositions.Remove(whichSatellite.followTarget.gameObject);
			satellites.Remove(whichSatellite);
			RearrangeTargets();
		}

		private void RearrangeTargets() {
			if (satelliteTargetPositions.Count > 0) {
				angle = (360f / satelliteTargetPositions.Count) * Mathf.Deg2Rad;
				float a = 0;
				for (int i = 0; i < satelliteTargetPositions.Count; i++) {
					satelliteTargetPositions[i].transform.localPosition = new Vector3(Mathf.Cos(a), Mathf.Sin(a));
					a += angle;
				}
				//Debug.Log($"rearranging to {satelliteTargetPositions.Count}");
			}
			else {
				angle = 360;
				for (int i = satelliteTargetPositions.Count - 1; i >= 0; i--) {
					satelliteTargetPositions.RemoveAt(i);
				}
				satelliteTargetPositions.Clear();
				for (int i = satellites.Count - 1; i >= 0; i--) {
					satellites.RemoveAt(i);
				}
				satellites.Clear();
			}
		}
	}
}