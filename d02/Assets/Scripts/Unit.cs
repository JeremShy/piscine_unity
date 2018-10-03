using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	haut		= 1,
	droite		= 2,
	bas			= 3,
	gauche		= 4,
	hautDroite	= 5,
	basDroite	= 6,
	basGauche	= 7,
	hautGauche	= 8
};


public abstract class Unit : Attackable {
	public float attackPoints;
	public float velocity;
	private Vector3 _arrivee;
	private bool _moving;
	protected Direction _direction;
	private Animator _animator;
	public AudioClip[] onMove;
	public AudioClip onAttack;
	private bool _attacking = false;
	private Attackable _target;
	private Direction _lastDirection;

	public virtual void Start() {
		_direction = Direction.haut;
		_lastDirection = _direction;
		_moving = false;
		_animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (_moving) {
			Vector2 directionVector;
			Vector2 diff;

			directionVector = _arrivee - transform.position;
			directionVector.Normalize ();
			directionVector *= velocity * Time.fixedDeltaTime;
			transform.Translate (directionVector);
			diff = _arrivee - transform.position;
			if (diff.x <= 0.1 && diff.x >= -0.1 &&
				diff.y <= 0.1 && diff.y >= -0.1) {
				_moving = false;
				_animator.SetBool ("moving", _moving);
			}
		}
		if (_attacking) {
			if (_target == null) {
				_attacking = false;
				_animator.SetBool ("attacking", false);
			} else {
				Vector2 diff = _target.transform.position - transform.position;
				if (diff.magnitude <= 1) {
					if (_moving) {
						_moving = false;
						_animator.SetBool ("moving", false);
						_animator.SetBool ("attacking", true);
					}
					if (_target.takeDamage (15 * Time.fixedDeltaTime)) {
						_attacking = false;
						_animator.SetBool ("attacking", false);
					} else if (_target.life > 0.1) {
						Debug.Log (_target.name + " has been attacked. He now has " + _target.life + " HP.");
					} else {
						Debug.Log (_target.name + " died.");
					}
				} else {
					face (_target.transform.position);
					if (!_moving) {
						AudioManager.instance.Play(onAttack);
						_moving = true;
						_animator.SetBool ("moving", true);
					}
					_arrivee = _target.transform.position;
				}
			}
		}
	}


	public void attack(GameObject target) {
		Attackable attackableTarget = target.GetComponent<Attackable>();
		if (!attackableTarget)
			return ;
		_target = attackableTarget;
		_attacking = true;
//		attackableTarget.takeDamage (attackPoints);
	}

	public void face(Vector3 target) {

		Vector3 diff = target - transform.position;
		//		Debug.Log ("Diff : " + diff.x + "," + diff.y);
		bool xpositif = (diff.x > 0);
		bool xnegatif = (diff.x < 0);
		bool ynegatif = (diff.y < 0);
		bool ypositif = (diff.y > 0);

		bool xnull = (diff.x <= 0.75f && diff.x >= -0.75f);
		bool ynull = (diff.y <= 0.75f && diff.y >= -0.75f);
		if (xpositif && ynull)
			_direction = Direction.droite;
		else if (xnull && ypositif)
			_direction = Direction.haut;
		else if (xnegatif && ynull)
			_direction = Direction.gauche;
		else if (xnull && !ypositif)
			_direction = Direction.bas;
		else if (xpositif && ypositif)
			_direction = Direction.hautDroite;
		else if (xnegatif && ypositif)
			_direction = Direction.hautGauche;
		else if (xnegatif && ynegatif)
			_direction = Direction.basGauche;
		else if (xpositif && ynegatif)
			_direction = Direction.basDroite;

		if (_direction != _lastDirection) {
			_animator.SetInteger ("direction", (int)_direction);
			_lastDirection = _direction;
		}
	}

	public void move(Vector2 destination) {
		if (_attacking) {
			_attacking = false;
			_animator.SetBool ("attacking", false);
		}
		//		Debug.Log ("Trying to move the player to : " + destination);

		Vector3 target = Camera.main.ScreenToWorldPoint (destination);
		target.z = transform.position.z;
		face (target);

		_animator.SetBool ("moving", true);

		_moving = true;
		_arrivee = target;

		int nbr = Random.Range (0, onMove.Length);
		AudioManager.instance.Play (onMove [nbr]);
	}

}
