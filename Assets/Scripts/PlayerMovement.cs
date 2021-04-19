using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector2 direction;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
        SetAnimator(direction);
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }

    private void SetAnimator(Vector2 direction)
    {
        _animator.SetFloat("xDir", direction.x);
        _animator.SetFloat("yDir", direction.y);
        print(_animator.GetFloat("xDir"));
    }
    
}
