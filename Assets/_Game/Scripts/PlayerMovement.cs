using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody2D _rigidbody2D;

    [SerializeField] 
    private BoxCollider2D _groundCheckCollider;

    [SerializeField] 
    private LayerMask _groundLayerMask;

    [SerializeField] 
    private float _jumpVelocity = 15f;
    
    private bool _pressedJump;
    private bool _isGrounded;

    private void Update()
    {
        GetInput();
        JumpWhenInput();
    }
    
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void GetInput()
    {
        _pressedJump = Input.GetKeyDown(KeyCode.Space);
    }

    private void JumpWhenInput()
    {
        if (!_pressedJump || !_isGrounded)
        {
            return;
        }
        
        _isGrounded = false;
        _rigidbody2D.velocity = new Vector2(0f, _jumpVelocity);
    }

    private void CheckGround()
    {
        var groundCheckBounds = _groundCheckCollider.bounds;
        
        _isGrounded =
            Physics2D.OverlapAreaAll(
                groundCheckBounds.min, 
                groundCheckBounds.max, 
                _groundLayerMask
            ).Length > 0;
    }
}
