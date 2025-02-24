using UnityEngine;

public class ProjectileTurret : Turret
{
    public override void Aim()
    {
        head.transform.LookAt(target.transform.position);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
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