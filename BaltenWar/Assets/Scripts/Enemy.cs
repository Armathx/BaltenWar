using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float health;
    public Transform goal;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.SetDestination(goal.position);
        animator.SetBool("Walk_Anim", true);
        agent.updateRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}