using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	public static UnitController instance { get; private set; }

	private List<PlayerScript> _player;
	private bool _ignoreNext = false;


	// Use this for initialization
	void Start () {
		instance = this;
		_player = new List<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_ignoreNext) {
			_ignoreNext = false;
			return;
		}
		if (_player.Count > 0) {
			if (Input.GetMouseButtonDown (1)) {
				_player.Clear ();
				return;
			}
			if (Input.GetMouseButtonDown (0)) {
				Vector2 toCheck = Input.mousePosition;
				Collider2D col = Physics2D.OverlapCircle (Camera.main.ScreenToWorldPoint(toCheck), 0.1f, 1 << LayerMask.NameToLayer("Ennemy"));
				if (col) {
					_player.ForEach (p => p.attack (col.gameObject));
				}
				else {
					_player.ForEach (p => p.move (Input.mousePosition));
				}
			}
		}
	}

	public void playerClicked(PlayerScript thingClicked) {
		_player.Clear ();
		_player.Add (thingClicked);
		_ignoreNext = true;
	}

	public void addPlayer(PlayerScript thingClicked) {
		_player.Add (thingClicked);
		_ignoreNext = true;
	}
}
