using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Projectile projectile;

	[Header("Caracteristics")]
	[Range(0.1f, 10f)]
	public float reloadTime;
	[Range(0.1f, 20f)]
	public float range;
	public int maxAmmo;
	public bool unlimitedAmmo = false;
	public bool makesSound = true;
	new public string name;
	public float dropForce;

	[Header("Game Design")]
	public Sprite droppedSprite;
	public Sprite holdingSprite;
	public AudioClip fireSound;

	[HideInInspector]
	public int ammo;

	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;
	new private Rigidbody2D rigidbody;
	new private Collider2D collider;

	private bool grabbed = false;
	private float blinkingScale = 1f;
	private Coroutine blinkingCoroutine;
	private float lastShoot;

	void Start () {
		ammo = maxAmmo;
		if (unlimitedAmmo) {
			ammo = int.MaxValue;
		}
		spriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Log(spriteRenderer);
		audioSource = GetComponent<AudioSource>();
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<Collider2D>();

		rigidbody.bodyType = RigidbodyType2D.Static;
		audioSource.clip = fireSound;

		blinkingCoroutine = StartCoroutine(Blinking());
		lastShoot = reloadTime + 0.1f;
	}

	void Update () {
		if (rigidbody.bodyType == RigidbodyType2D.Dynamic && rigidbody.velocity.magnitude > 0.1f) {
			rigidbody.drag += 0.1f;
			rigidbody.MoveRotation(rigidbody.rotation + (1300f / rigidbody.drag) * Time.deltaTime);
		} else if (!grabbed) {
			blinkingScale = (Mathf.Cos(Time.time * 7) / 8.0f) + 1.125f;
			transform.localScale = new Vector3(blinkingScale, blinkingScale, blinkingScale);
		}
	}

	public bool Shoot(string targetTag) {
		if (lastShoot + reloadTime < Time.time && ammo > 0) {
			if (!unlimitedAmmo) {
				ammo--;
			}
			audioSource.Play();
			Projectile p = Instantiate(projectile, transform.position, transform.rotation);
			if (targetTag.Equals("Player")) {
				p.tag = "ennemyBullet";
			} else {
				p.tag = "bullet";
			}
			p.transform.Rotate(0f, 0f, -90f);
			p.range = range;
			p.targetTag = targetTag;
			lastShoot = Time.time;
			return true;
		}
		return false;
	}

	public void Grab() {
		gameObject.tag = "weapon";
		spriteRenderer.sprite = holdingSprite;
		ResetPhysics();
		StopCoroutine(blinkingCoroutine);
		ResetSprite();
		grabbed = true;
	}

	public void Drop() {
		gameObject.tag = "droppedWeapon";
		spriteRenderer.sprite = droppedSprite;
		Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
		direction.Normalize();
		rigidbody.bodyType = RigidbodyType2D.Dynamic;
		rigidbody.AddForce(direction * dropForce, ForceMode2D.Impulse);
		grabbed = false;
		StartCoroutine(LaunchWeapon());
	}

	private void ResetPhysics() {
		rigidbody.bodyType = RigidbodyType2D.Static;
		rigidbody.drag = 0;
		collider.isTrigger = true;
	}

	private void ResetSprite() {
		spriteRenderer.color = Color.white;
		transform.localScale = Vector3.one;
	}

	IEnumerator LaunchWeapon() {
		yield return new WaitForSeconds(0.3f);
		collider.isTrigger = false;
		yield return new WaitForSeconds(1f);
		ResetPhysics();
	}

	IEnumerator Blinking() {
		while (true) {
			spriteRenderer.color = Color.white;
			yield return new WaitForSeconds(1f);
			spriteRenderer.color = Color.grey;
			yield return new WaitForSeconds(1f);
		}
	}
}
