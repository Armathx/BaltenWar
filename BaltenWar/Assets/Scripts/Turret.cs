using System.Linq;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    public Enemy target;
    public GameObject head;
    public GameObject muzzle;
    public float damage;
    public float attackRate;
    public float range;
    public float timer = 0;
    public bool inGame = false;

    public LayerMask layerMask;

    public abstract void Aim();

    public abstract void Attack();

    public void FindTarget()
    {
        Collider[] tab = Physics.OverlapSphere(transform.position, range, layerMask);

        if (tab.Length == 0) return;

        target = tab.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).FirstOrDefault().GetComponent<Enemy>();
    }

    public void CheckValidTarget()
    {
        if ((target.transform.position - transform.position).sqrMagnitude > range * range)
        {
            target = null;
        }
    }

    protected virtual void Update()
    {
        if (!inGame) return;

        if (target == null)
        {
            FindTarget();
        }
        else
        {
            timer += Time.deltaTime;

            Aim();
            if (timer >= attackRate)
            {
                timer -= attackRate;
                Attack();
            }
            CheckValidTarget();
        }
    }
}