using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour {

	public float speed;
	public List<GameObject> patrol;
	private int patrolNode = 0;
	List<Node> path = null;
	int	currentNode;

	Path p = null;

	bool walkingTowardPlayer = false;

	bool walkingTowardNoiseBool = false;
	Vector2 walkingTowardNoise = Vector2.zero;

	public Weapon weapon;

	public AudioSource audioSource;
	public AudioSource blood;
	public AudioClip[] audioClips;
	public AudioClip bloodClip;

	public List<Sprite> heads;
	public SpriteRenderer headSpriteRenderer;
	public Collider2D ennemyCollider;

	private SpriteRenderer[] sprites;
	private bool isDying = false;
	new private Rigidbody2D rigidbody;
	private Animator animator;

	[HideInInspector]
	public bool gameOver = false;

	public delegate void OnEnnemyKilled(Ennemy ennemy);
	public static event OnEnnemyKilled onEnnemyKilled;

	void Start () {
		if (!weapon) {
			weapon = GetComponentInChildren<Weapon>();
		}
		if (weapon) {
			weapon.Grab();
			weapon.ammo = int.MaxValue;
			weapon.transform.SetParent(this.transform);
			weapon.transform.localPosition = new Vector3(-0.25f, -0.25f, 0f);
			weapon.transform.localRotation = Quaternion.identity;
			weapon.gameObject.layer = LayerMask.NameToLayer("Ennemy");
		}

		sprites = GetComponentsInChildren<SpriteRenderer>();
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		if (heads != null && headSpriteRenderer) {
			headSpriteRenderer.sprite = heads[Random.Range(0, heads.Count)];
		}
	}
	
	void FixedUpdate()
	{
		if (isDying || gameOver) {
			return;
		}
		if (walkingTowardPlayer) {
			walkToward(Player.player.transform.position);
			TryToShoot();
		}
		else if (walkingTowardNoiseBool == true) {
			walkToward(walkingTowardNoise);
		}
		else
			DoPatrol();
	}

	void DoPatrol() {
		if (patrol == null || patrolNode >= patrol.Count || patrol[patrolNode] == null) {
			return;
		}
		PatrolWalk(patrol[patrolNode].transform.position);
		if ((Vector2)transform.position == (Vector2)patrol[patrolNode].transform.position)
			patrolNode = (patrolNode + 1) % patrol.Count;
	}

	void PatrolWalk(Vector2 destination)
	{
		Vector2 dir = new Vector2();
		Vector2 trans = new Vector2();

		if ((Vector2)transform.position == destination) {
			animator.SetBool("IsWalking", false);
			return ;
		}
		Vector2 ennemyPos = transform.position;
		Vector2 ourPos = destination;
		float a = Vector2.SignedAngle(Vector2.down, ourPos - ennemyPos);
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, a);
		animator.SetBool("IsWalking", true);

		if (transform.position.x < destination.x)
			dir.x = 1;
		else if (transform.position.x == destination.x)
			dir.x = 0;
		else
			dir.x = -1;

		if (transform.position.y < destination.y)
			dir.y = 1;
		else if (transform.position.y == destination.y)
			dir.y = 0;
		else
			dir.y = -1;

		trans = dir * Time.fixedDeltaTime * speed;
		if (Mathf.Abs(trans.x) > Mathf.Abs(destination.x - transform.position.x))
			trans.x = Mathf.Abs(destination.x - transform.position.x) * dir.x;
		if (Mathf.Abs(trans.y) > Mathf.Abs(destination.y - transform.position.y))
			trans.y = Mathf.Abs(destination.y - transform.position.y) * dir.y;
		transform.position = transform.position + (Vector3)trans;
	}

	void OnDrawGizmos()
	{
		if (path != null)
		{
			int i = 0;
			Gizmos.color = Color.green;
			while (i < path.Count)
			{
				Gizmos.DrawSphere(path[i].pos, .1f);
				i++;
			}
		}
		if (patrol != null)
		{
			Gizmos.color = Color.green;
			foreach (GameObject g in patrol)
			{
 				Gizmos.DrawSphere(g.transform.position, .3f);
			}
		}
	}

	void walkToward(Vector2 destination)
	{
		if ((Vector2)transform.position == destination) {
			animator.SetBool("IsWalking", false);
			return ;
		}
		if (p == null)
			p = new Path(transform.position, destination);
		else if (p.destVec != destination)
			p.changePath(transform.position, destination);
		if (!p.computed)
		{
			p.ComputePath();
			path = p.path;
			currentNode = 0;
		}
		if (currentNode == path.Count)
			return ;

		Vector2 ennemyPos = transform.position;
		Vector2 ourPos = destination;
		float a = Vector2.SignedAngle(Vector2.down, ourPos - ennemyPos);
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, a);

		walkToNode(path[currentNode]);
		animator.SetBool("IsWalking", true);
	}

	void walkToNode(Node n)
	{
		Vector2 dir = new Vector2();
		Vector2 trans = new Vector2();

		if (transform.position.x < n.pos.x)
			dir.x = 1;
		else if (transform.position.x == n.pos.x)
			dir.x = 0;
		else
			dir.x = -1;

		if (transform.position.y < n.pos.y)
			dir.y = 1;
		else if (transform.position.y == n.pos.y)
			dir.y = 0;
		else
			dir.y = -1;

		trans = dir * Time.fixedDeltaTime * speed;
		if (Mathf.Abs(trans.x) > Mathf.Abs(n.pos.x - transform.position.x))
			trans.x = Mathf.Abs(n.pos.x - transform.position.x) * dir.x;
		if (Mathf.Abs(trans.y) > Mathf.Abs(n.pos.y - transform.position.y))
			trans.y = Mathf.Abs(n.pos.y - transform.position.y) * dir.y;
		transform.position = transform.position + (Vector3)trans;
		if ((Vector2)transform.position == n.pos)
			currentNode++;
	}

	void TryToShoot() {
		Vector2 direction = Player.player.transform.position - transform.position;

		RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("wall", "Default"));
		if (hit2D && hit2D.transform.CompareTag("Player")) {
			if (weapon != null) {
				weapon.Shoot("Player");
			} 
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		//if (other.tag == "Player" && !walkingTowardPlayer) { // Bullet sound from the player 
		//	Debug.Log("Here");
		//	walkingTowardNoise = other.transform.position;
		//	walkingTowardNoiseBool = true;
		//}

		if (other.tag == "bullet" && ennemyCollider.IsTouching(other)) {
			Debug.Log(other.name + " dying");
			Die();
			Destroy(other.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		//Debug.Log("Collision with " + collision.name);
		if (isDying || gameOver) return;
		if (collision.tag == "Player") {
			Vector2 direction = collision.transform.position - transform.position;

			RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("wall", "Default"));
			if (hit2D && hit2D.transform.CompareTag("Player")) {
				Vector2 ennemyPos = transform.position;
				Vector2 ourPos = collision.transform.position;
				float a = Vector2.SignedAngle(Vector2.down, ourPos - ennemyPos);
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, a);

				if (weapon != null) {
					weapon.Shoot("Player");
				} 
				walkingTowardPlayer = true;
				walkingTowardNoiseBool = false;
				//else {
				//	Debug.Log("No Weapon");
				//}

			} else {
				//Debug.Log("Vision blocked by " + hit2D.transform.name + " (" + hit2D.transform.tag + ")");
			}
		}
	}

	void Die() {
		if (audioClips != null && audioClips.Length > 0) {
			audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
			audioSource.Play();
			blood.clip = bloodClip;
			blood.Play();
		}
		onEnnemyKilled(this);
		rigidbody.simulated = false;
		StartCoroutine(Blinking(3));
		Destroy(gameObject, 3);
		isDying = true;
	}

	IEnumerator Blinking(float time) {
		for (int i = 0; i < time * 2; i++) {
			foreach (SpriteRenderer spriteRenderer in sprites) {
				spriteRenderer.color = Color.clear;
			}
			yield return new WaitForSeconds(0.25f);
			foreach (SpriteRenderer spriteRenderer in sprites) {
				spriteRenderer.color = Color.white;
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
}
