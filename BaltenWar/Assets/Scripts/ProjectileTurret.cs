using UnityEngine;

public class ProjectileTurret : Turret
{
    [SerializeField] private GameObject projectile;

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