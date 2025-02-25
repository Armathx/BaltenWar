using UnityEngine;

public class ProjectileTurret : Turret
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform pivot;

    public override void Aim()
    {
        head.transform.LookAt(target.transform.position);
    }

    public override void Attack()
    {
        Projectile p = Instantiate(projectile).GetComponent<Projectile>();
        p.transform.position = muzzle.transform.position;
        p.Target = target;
        p.Damage = damage;
        pivot.localRotation *= Quaternion.Euler(5000f * Time.deltaTime, 0, 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}