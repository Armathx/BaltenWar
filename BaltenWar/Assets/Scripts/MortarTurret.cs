using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MortarTurret : Turret
{
    [SerializeField] private GameObject projectile;
    [SerializeField] public GameObject rangeSphere;

    [SerializeField, Min(3)]
    private int lineSegment = 60;

    public override void Aim()
    {
        head.transform.LookAt(target.transform.position);
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Instantiate(rangeSphere, transform.position, transform.rotation);
        rangeSphere.transform.localScale = new Vector3(this.range, this.range, this.range);

        Debug.Log("Range : " + range);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;

        //Gizmos.DrawSphere(target.transform.position, 1);
    }
}