using System;
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
		private Collider2D col2D;
		private Bounds bounds;
		private Camera camera;
		private Bounds screenBounds;

		private void Awake() {
			camera = Camera.main;
			col2D = GetComponent<Collider2D>();

			horizontalScroller = GetComponent<HorizontalScroller>();
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.magenta;
			bounds = GetComponent<Collider2D>().bounds;
			bounds.extents += Vector3.forward * 10;
			Gizmos.DrawWireCube(bounds.center, bounds.size);
		}

		private void Start() {
			bounds = col2D.bounds;
			bounds.extents += Vector3.forward * 10;
			screenBounds = BoundariesManager.Instance.bounds;
			Debug.Log($"{bounds} {screenBounds}");
		}

		private void Update() {
			bounds = col2D.bounds;
			bounds.extents += Vector3.forward * 10;
			switch (tilemapState) {
				case TilemapState.OutPreEnter:
					if (bounds.Intersects(screenBounds)) {
						tilemapState = TilemapState.InsideBounds;
					}
					break;
				case TilemapState.InsideBounds:
					if (!bounds.Intersects(screenBounds)) {
						tilemapState = TilemapState.OutOfBounds;
					}
					break;
				case TilemapState.OutOfBounds:
					Destroy(horizontalScroller);
					Destroy(gameObject);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}