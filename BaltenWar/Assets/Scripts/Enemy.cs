using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float health;
    public Transform goal;
    public Transform weakPoint;
    private NavMeshAgent agent;
    private Animator animator;

    public delegate void IsDestroyed();

    public IsDestroyed m_destroy;

    public bool b_isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.SetDestination(goal.position);
        if (animator != null)
        {
            animator.SetBool("Walk_Anim", true);
        }
        agent.updateRotation = true;

        m_destroy += Destroy;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !b_isDead) m_destroy?.Invoke();
    }

    public void Destroy()
    {
        b_isDead = true;
        Destroy(gameObject);
    }
}