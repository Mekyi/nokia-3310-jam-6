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
    private Animator _animator;

    [SerializeField] 
    private BoxCollider2D _groundCheckCollider;

    [SerializeField] 
    private LayerMask _groundLayerMask;

    [SerializeField] 
    private float _jumpVelocity = 15f;
    
    private bool _pressedJump;
    private bool _isGrounded;
    private bool _isSliding;
    
    private static readonly int IsGroundedAnimationParameter = Animator.StringToHash("isGrounded");
    private static readonly int IsSlidingAnimationParameter = Animator.StringToHash("isSliding");


    private void Update()
    {
        GetInput();
        JumpWhenInput();
        SlideWhenInput();
    }


    private void FixedUpdate()
    {
        CheckGround();
        CheckSlide();
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
    
    private void SlideWhenInput()
    {
        if (_pressedJump || _isGrounded)
        {
            return;
        }

        _isSliding = true;
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
        
        _animator.SetBool(IsGroundedAnimationParameter, _isGrounded);
    }
    
    private void CheckSlide()
    {
        _animator.SetBool(IsSlidingAnimationParameter, _isSliding);
    }
    
    private void GetInput()
    {
        _pressedJump = Input.GetKeyDown(KeyCode.Space);
        _isSliding = Input.GetKey(KeyCode.S);
    }
}
