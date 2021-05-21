using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private Vector2 direction;

    private Animator _animator;
    private Tilemap _tilemap;

    public Transform keyFollowPoint;
    public bool isPicked;
    public bool isFinished;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
        if (direction.x != 0 || direction.y != 0)
            SetAnimator(direction);
        
        else
            _animator.SetFloat("speed", 0);
    }

    private void TakeInput()
    {
        direction.x = (Input.GetAxisRaw("Horizontal"));
        direction.y = (Input.GetAxisRaw("Vertical"));
    }

    private void SetAnimator(Vector2 direction)
    {
        
        _animator.SetFloat("xDir", direction.x);
        _animator.SetFloat("yDir", direction.y);
        _animator.SetFloat("speed", direction.sqrMagnitude);
        
    }
}
