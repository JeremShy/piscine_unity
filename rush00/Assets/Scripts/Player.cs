using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player player;
	public float speed;

	public Weapon weapon = null;


	public CircleCollider2D soundTrigger;
	public CircleCollider2D playerTrigger;
	public AudioSource grabWeaponSound;


	new private Rigidbody2D rigidbody2D;
	private GameObject potentialWeapon = null;
	private Animator animator;

	public delegate void OnGrabWeapon(Weapon w);
	public static event OnGrabWeapon onGrabWeapon;

	private int remaningFrames = 0;

	[HideInInspector]
	public bool gameOver = false;

	private void Awake() {
		if (player == null)
			player = this;
	}

	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update() {
		if (gameOver) return;

		HandleWeapon();
	}

	void FixedUpdate () {
		if (gameOver) return;

		HandleMovement();

		if (soundTrigger.isActiveAndEnabled && remaningFrames == 0) {
			soundTrigger.enabled = false;
		}
		remaningFrames--;
	}

	void HandleMovement() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 ourPos = transform.position;
		float a = Vector2.SignedAngle(Vector2.down, mousePos - ourPos);
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, a);

		Vector2 v;
		v.x = Input.GetAxisRaw("Horizontal");
		v.y = Input.GetAxisRaw("Vertical");
		v = v * speed * Time.fixedDeltaTime;
		rigidbody2D.MovePosition(rigidbody2D.position + v);

		if (Input.GetKeyDown(KeyCode.E) && potentialWeapon && !weapon) {
			player.weapon = potentialWeapon.GetComponent<Weapon>();
			weapon.Grab();
			weapon.transform.SetParent(this.transform);
			weapon.transform.localPosition = new Vector3(-0.25f, -0.25f, 0f);
			weapon.transform.localRotation = Quaternion.identity;
			grabWeaponSound.Play();
			onGrabWeapon(weapon);
		}
		animator.SetBool("IsWalking", Mathf.Abs(v.x) > 0.001f || Mathf.Abs(v.y) > 0.001f);
	}

	void HandleWeapon() {
		// if sound trigger enabled, disable it
		if (Input.GetMouseButton(0)) {
			if (weapon != null) {
				// shoot
				if (weapon.Shoot("ennemy") && weapon.makesSound) {
					soundTrigger.enabled = true;
					remaningFrames = 10;
				}
			}
		}

		if (Input.GetMouseButtonDown(1)) {
			if (weapon != null) {
				weapon.Drop();
				weapon.transform.SetParent(null);
				weapon = null;
				onGrabWeapon(null);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("droppedWeapon")) {
			potentialWeapon = collision.gameObject;
		}

		//foreach (ContactPoint2D contact in collision.GetComponent<Coll>().contacts) {
		//	Debug.Log(contact);
		//}



		if (playerTrigger.IsTouching(collision) && collision.CompareTag("ennemyBullet")) {
			Debug.Log(collision.gameObject.name);
			Destroy(collision.gameObject);
			LevelManager.instance.OnPlayerDie();
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("droppedWeapon")) {
			potentialWeapon = null;
		}
	}
}
