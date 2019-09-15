using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Player _player;
	// Use this for initialization
	void Start ()
    {
        _anim = GetComponent<Animator>();
        _player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_player.isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _anim.SetBool("turn_Left", true);
                _anim.SetBool("turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _anim.SetBool("turn_Left", false);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _anim.SetBool("turn_Right", true);
                _anim.SetBool("turn_Left", false);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _anim.SetBool("turn_Right", false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _anim.SetBool("turn_Left", true);
                _anim.SetBool("turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _anim.SetBool("turn_Left", false);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _anim.SetBool("turn_Right", true);
                _anim.SetBool("turn_Left", false);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _anim.SetBool("turn_Right", false);
            }
        }
    }   
}
