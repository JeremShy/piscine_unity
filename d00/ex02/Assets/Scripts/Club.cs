using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {
    private bool _state = false;
    private float _niveau = 0f;
    private Vector3 _start;

	// Use this for initialization
	void Start () {
        hide();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            power();
        }
        else if (_state)
        {
            hide();
            _niveau = 0;
        }
    }

    void power()
    {
        if (_state == false)
        {
            show();
            _niveau = 1;
            _start = transform.position;
            return;
        }
        else if (_niveau < 100)
        {
            _start.y += -2 * Time.deltaTime;
            _niveau += 400 * Time.deltaTime;
            if (_niveau > 100)
                _niveau = 100;
            transform.position = _start;
        }
        Debug.Log("_niveau : " + _niveau);
    }

    void hide()
    {
        Vector3 vec = this.transform.position;
        vec.z = 15;
        this.transform.position = vec;
        _state = false;
    }

    void show()
    {
        Vector3 vec = this.transform.position;
        vec.z = 0;
        this.transform.position = vec;
        _state = true;
    }
}
