﻿using System;
using UnityEngine;

namespace ICO321 {
	public class ScrollingTiledMap : MonoBehaviour {
		public enum TilemapState {
			OutPreEnter = 0,
			InsideBounds = 1,
			OutOfBounds = 2
		}

		private HorizontalScroller horizontalScroller;
		public TilemapState tilemapState;
		[SerializeField] private float timeToReachScreen;
		[Space] [Header("Stuff to activate")] [SerializeField]
		private GameObject[] stuffToActivateOnEnterScreen;
		private Collider2D col2D;
		private Bounds bounds;
		private Camera camera;
		private Bounds screenBounds;

		private void Awake() {
			camera = Camera.main;
			col2D = GetComponent<Collider2D>();

			horizontalScroller = GetComponent<HorizontalScroller>();
			for (int i = 0; i < stuffToActivateOnEnterScreen.Length; i++) {
				stuffToActivateOnEnterScreen[i].SetActive(false);
			}
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.magenta;
			bounds = GetComponent<Collider2D>().bounds;
			bounds.extents += Vector3.forward * 10;
			Gizmos.DrawWireCube(bounds.center, bounds.size);


			var min = Camera.main.ViewportToWorldPoint(Vector3.zero);
			min.z = -100;
			var max = Camera.main.ViewportToWorldPoint(Vector3.one);
			max.z = 100;
			screenBounds.min = min;
			screenBounds.max = max;
			timeToReachScreen = Mathf.Abs(bounds.min.x - screenBounds.max.x) / GetComponent<HorizontalScroller>().speed;
		}

		private void Start() {
			bounds = col2D.bounds;
			bounds.extents += Vector3.forward * 10;
			screenBounds = BoundariesManager.Instance.bounds;
			//Debug.Log($"{bounds} {screenBounds}");
		}

		private void Update() {
			bounds = col2D.bounds;
			bounds.extents += Vector3.forward * 10;
			switch (tilemapState) {
				case TilemapState.OutPreEnter:
					if (bounds.Intersects(screenBounds)) {
						tilemapState = TilemapState.InsideBounds;
						for (int i = 0; i < stuffToActivateOnEnterScreen.Length; i++) {
							stuffToActivateOnEnterScreen[i].SetActive(true);
						}
					}
					break;
				case TilemapState.InsideBounds:
					if (!bounds.Intersects(screenBounds)) {
						tilemapState = TilemapState.OutOfBounds;
					}
					break;
				case TilemapState.OutOfBounds:
					//Destroy(horizontalScroller);
					Destroy(gameObject, 1);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}