using UnityEngine;

namespace ICO321 {
	[CreateAssetMenu(menuName = "ICO/Level Settings", fileName = "LevelSettings")]
	public class LevelSettings : ScriptableObject {
		public int trackNumber = -1;
		public float scrollingSpeed = 1.3f;
	}
}