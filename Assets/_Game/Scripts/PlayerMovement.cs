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

    [SerializeField] 
    private BoxCollider2D _runCollider;
    
    [SerializeField] 
    private BoxCollider2D _sliderCollider;

    [SerializeField] 
    private Transform _heightLimiter;
    
    private bool _holdingFly;
    private bool _isGrounded;
    private bool _holdingSlide;
    private bool _isSliding;
    
    private static readonly int IsGroundedAnimationParameter = Animator.StringToHash("isGrounded");
    private static readonly int IsSlidingAnimationParameter = Animator.StringToHash("isSliding");


    private void Update()
    {
        if (GameManager.Instance.IsGameRunning == false)
        {
            return;
        }
        
        GetInput();
        if (_isGrounded && _holdingFly)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Jump, transform.position);
        }
        FlyWhenInput();
        SlideWhenInput();
        ToggleTriggerColliders();
    }


    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameRunning == false)
        {
            return;
        }
        
        CheckGround();
        CheckSlide();
        CheckHeight();
    }

    private void FlyWhenInput()
    {
        if (!_holdingFly)
        {
            return;
        }
        
        _isGrounded = false;
        _rigidbody2D.velocity = new Vector2(0f, _jumpVelocity);
    }
    
    private void SlideWhenInput()
    {
        if (_holdingFly || !_isGrounded || !_holdingSlide)
        {
            _isSliding = false;
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
        _holdingFly = Input.GetKey(KeyCode.Z);
        _holdingSlide = Input.GetKey(KeyCode.X);
    }

    private void ToggleTriggerColliders()
    {
        _runCollider.enabled = !_isSliding;
        _sliderCollider.enabled = _isSliding;
    }

    private void CheckHeight()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -100f, _heightLimiter.position.y) ,
            transform.position.z
        );
    }
}
