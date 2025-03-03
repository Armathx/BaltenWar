using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    public float health;
    public Transform goal;
    public Transform weakPoint;
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private GameObject vfx;
    private GameObject vfxHolder;

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
        if (vfxHolder == null)
        {
            vfxHolder = Instantiate(vfx, weakPoint);
            vfxHolder.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        }
        var state = vfxHolder.GetComponent<VisualEffect>().GetSpawnSystemInfo(0);
        if (!state.playing)
        {
            vfxHolder.GetComponent<VisualEffect>().Play();
        }
        if (health <= 0 && !b_isDead) m_destroy?.Invoke();
    }

    public void Destroy()
    {
        b_isDead = true;
        Destroy(gameObject);
    }
}