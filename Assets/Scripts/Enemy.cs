using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _timeToStay;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private Vector2 _startPosition;
    private float _tempSpeed;

    private void Start()
    {
        _startPosition = transform.position;
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _tempSpeed = _speed;

        _animator.SetBool("IsMoving", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.transform.position = player.StartPosition;
        }   
    }

    private void Update()
    {       
        float xPosition = Mathf.MoveTowards(transform.position.x, _target.position.x, _speed * Time.deltaTime) - transform.position.x;
        transform.position += new Vector3(xPosition, 0);        

        if (transform.position.x <= _startPosition.x || transform.position.x >= _target.position.x)
        {
            _tempSpeed *= -1;

            StartCoroutine(Wait());       
        }  
        
    }

    private IEnumerator Wait()
    {
        var waitForNeedSeconds = new WaitForSeconds(_timeToStay);

        _animator.SetBool("IsMoving", false);
        _speed = 0;

        yield return waitForNeedSeconds;

        _animator.SetBool("IsMoving", true);
        _speed = _tempSpeed;

        Flip();
    }

    private void Flip()
    {
        _renderer.flipX = !_renderer.flipX;
    }
}