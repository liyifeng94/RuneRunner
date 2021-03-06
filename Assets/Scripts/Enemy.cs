﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rb2D;
    private Animator _animator;

    public Vector3 MovementVelocity;
    [HideInInspector]public bool Alive;

    public enum AttackMove
    {
        Mid,
        High,
        Low,
        NumOfMoves
    }

    public AttackMove CurrentAttackMove;

    // Use this for initialization
    protected virtual void Start ()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
        Alive = true;
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if (Alive == false)
        {
            _animator.SetTrigger("enemyDeath");
        }
        Transform thisTransform = GetComponent<Transform>();
        thisTransform.position += MovementVelocity * Time.deltaTime;
	}

    public void ChangeVelocity(Vector3 newVelocity)
    {
        MovementVelocity = newVelocity;
    }

    protected virtual void OnTriggerEnter2D(Collider2D colliObject)
    {
        //check if collision is with an enemy.
        if (colliObject.gameObject.tag == "Player")
        {
            GameManager.Instance.GetLevelManager().EnemyInCombat(this);
            EnterCombat();
        }
    }

    //Animation hit event callback function
    public void HitTarget()
    {
        GameManager.Instance.GetLevelManager().EnemyExitCombat();
    }

    protected virtual void EnterCombat()
    {
        PlayAnimation();
    }

    public void EndCombat(bool alive)
    {
        Alive = alive;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public AttackMove GetAttackMove()
    {
        return CurrentAttackMove;
    }

    void PlayAnimation()
    {
        _animator.SetTrigger("enemyCombat");
    }
}
