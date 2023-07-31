using ICO321;
using UnityEngine;

namespace ICO321 {
	public class Bullet : MonoBehaviour {
		[SerializeField] private SpriteRenderer spriteRenderer;
		//[SerializeField] private ParticleVfx particleVfx;
		[SerializeField] private Sprite[] bulletSprites;
		[SerializeField] private TypesUtility.Phase phase;
		[SerializeField] private Vector3 direction;
		[SerializeField] private GameObject hitVfx;
		[SerializeField] private AudioClip hitClip;

		private int currentSprite;

		private Collider2D col2D;
		public float speed;
		public Vector3 Direction {
			get => direction;
			set { direction = value.normalized; }
		}
		public TypesUtility.Phase Phase {
			get => phase;
			set {
				phase = value;
				spriteRenderer.color = PhaseManager.Instance.GetPhaseColor();
				//particleVfx.Color = PhaseManager.Instance.GetPhaseColor();
			}
		}

		private void Awake() {
			direction = direction.normalized;
			col2D = GetComponent<Collider2D>();
			if (col2D == null) {
				Debug.LogError($"{name} is a Bullet with no Collider2D");
			}
			else {
				col2D.isTrigger = true;
			}
		}

		private void Update() {
			float angleDeg = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
			transform.position += Direction * (speed * Time.deltaTime);
			if (bulletSprites.Length > 0) {
				spriteRenderer.sprite = bulletSprites[currentSprite];
				currentSprite = (currentSprite + 1) % bulletSprites.Length;
			}
		}

		private void OnTriggerEnter2D(Collider2D other) {
			var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
			if (enemyHealth != null) {
				enemyHealth.Damage(phase);
			}
			var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
			if (playerHealth != null) {
				playerHealth.Damage();
			}
			var vfx = PoolManager.Instance.GetItem(hitVfx.name);
			vfx.transform.position = transform.position;
			vfx.GetComponent<ParticleVfx>().Color = spriteRenderer.color;
			vfx.GetComponent<ParticleVfx>().Play();
			SfxManager.Instance.PlayClip(hitClip, 0.2f);
			gameObject.SetActive(false);
		}
	}
}