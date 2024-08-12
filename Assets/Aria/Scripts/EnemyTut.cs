using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTut : MonoBehaviour
{

    public Transform player;
    NavMeshAgent agent;
    public float currentDistance;
    public float range;
    public float attackRange;
    Vector3 startPosition;
    public EnemyStatus enemyStatus;
    public bool isAttacking;


    public enum EnemyStatus
    {
        idle,
        follow,
        attack,
        home
    }

    // Update is called once per frame
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    void Update()
    {
        PlayerDistanceCheck();

        switch (enemyStatus)
        {
            case EnemyStatus.idle:
                IdleState();
                break;
            case EnemyStatus.follow:
                FollowState();
                break;
            case EnemyStatus.attack:
                AttackState();
                break;
            case EnemyStatus.home:
                HomeState();
                break;
        }

    }

    void PlayerDistanceCheck()
    {
        currentDistance = Vector3.Distance(transform.position, player.position);
    }

    private void HomeState()
    {
        agent.destination = startPosition;
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
    }

    private void AttackState()
    {
        agent.isStopped = true;
        if (isAttacking = false)
        {
            Debug.Log("Attack");
            isAttacking = true;
            Invoke("AttackingCoolDown", 2.0f);
        }
      
    }

    void AttackCoolDown()
    {
        if(currentDistance >= attackRange)
        {
            enemyStatus = EnemyStatus.follow;
        }

        isAttacking = false;

    }

    private void FollowState()
    {
        if(agent.isStopped)
        {
            agent.isStopped = false;
        }

        agent.destination = player.position;
        if(currentDistance >= range + 10)
        {
            // Enemy is too far from player
            enemyStatus = EnemyStatus.home;
        }

        if(currentDistance <= attackRange)
        {
            // Enemy is close enough to attack
            enemyStatus = EnemyStatus.attack;
        }
    }

    private void IdleState()
    {
        if(currentDistance <= range)
        {
            enemyStatus = EnemyStatus.follow;
        }

    }
}
