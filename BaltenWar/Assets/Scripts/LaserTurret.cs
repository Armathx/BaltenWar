using System.Collections;
using UnityEngine;

public class LaserTurret : Turret
{
    [SerializeField] private LineRenderer lineRenderer;

    public override void Aim()
    {
        head.transform.LookAt(target.transform.position);
    }

    public override void Attack()
    {
        target.TakeDamage(damage * Time.deltaTime);

        lineRenderer.SetPosition(0, muzzle.transform.position);
        lineRenderer.SetPosition(1, target.weakPoint.position);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        lineRenderer.enabled = target != null;

        if (target == null)
        {
            FindTarget();
        }
        else
        {
            Aim();
            Attack();
            CheckValidTarget();
        }
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;

        //Gizmos.DrawSphere(target.transform.position, 1);
    }
}