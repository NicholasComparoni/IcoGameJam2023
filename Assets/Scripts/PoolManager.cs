using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
	[Serializable]
	public class Pool {
		[HideInInspector] public string id;
		public GameObject prefab;
		public int amount;
		public Queue<GameObject> queue;

		public void Init(Transform parent) {
			queue = new Queue<GameObject>();
			for (int i = 0; i < amount; i++) {
				var newItem = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
				newItem.name = $"{prefab.name} {i} instance";
				newItem.SetActive(false);

				queue.Enqueue(newItem);
			}
			id = prefab.name;
		}

		public GameObject Get() {
			var item = queue.Dequeue();
			item.gameObject.SetActive(true);
			queue.Enqueue(item);
			return item;
		}
	}

	public static PoolManager Instance;
	public List<Pool> pools = new List<Pool>();

	private void Awake() {
		if (Instance != null) {
			Destroy(this);
		}
		else {
			Instance = this;
		}
		InitPools();
	}

	private void InitPools() {
		foreach (var p in pools) {
			var newParent = new GameObject(p.id);
			newParent.transform.parent = transform;
			p.Init(newParent.transform);
		}
	}

	public GameObject GetItem(string id) {
		foreach (var p in pools) {
			if (p.id == id) {
				return p.Get();
			}
		}
		return null;
	}
}