using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MortarTurret : Turret
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject projectile;

    [SerializeField, Min(3)]
    private int lineSegment = 60;

    public override void Aim()
    {
        head.transform.LookAt(target.transform.position);

        //TrajectoryLine();
    }

    public override void Attack()
    {
        MortarProjectile p = Instantiate(projectile).GetComponent<MortarProjectile>();

        p.transform.position = muzzle.transform.position;
        p.m_Start = muzzle.transform.position;
        p.m_End = target.weakPoint.position;

        p.Damage = damage;
        p.LayerMask = layerMask;
    }

    public void TrajectoryLine()
    {
        lineRenderer.positionCount = lineSegment;

        Vector3 start = muzzle.transform.position;
        Vector3 end = target.transform.position;
        float distance = Vector3.Distance(start, end);

        Vector3 midpoint = (start + end) / 2;
        midpoint.y += 100f;

        for (int i = 0; i < lineSegment; ++i)
        {
            float t = i / (float)(lineSegment - 1);

            Vector3 point = Vector3.Lerp(Vector3.Lerp(start, midpoint, t), Vector3.Lerp(midpoint, end, t), t);
            lineRenderer.SetPosition(i, point);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        lineRenderer.enabled = target != null;

        base.Update();
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;

        //Gizmos.DrawSphere(target.transform.position, 1);
    }
}