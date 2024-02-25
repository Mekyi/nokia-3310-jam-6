using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBlinker : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _spriteActivated = false;
    private float _interval = 0.75f;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blinker());
    }

    private IEnumerator Blinker()
    {
        while (true)
        {
            _spriteRenderer.enabled = _spriteActivated;
            _spriteActivated = !_spriteActivated;
            yield return new WaitForSecondsRealtime(_interval);
        }
    }

    public void SetInterval(float interval)
    {
        _interval = interval;
    }
}
