using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class LaserTurret : Turret
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] public GameObject rangeSphere;

    private VisualEffect effect;

    public override void Aim()
    {
        head.transform.LookAt(target.transform.position);
    }

    public override void Attack()
    {
        target.TakeDamage(damage * Time.deltaTime);
        effect.Play();
        float dist = (muzzle.transform.position - target.weakPoint.position).magnitude;
        //effect.SetVector3("StartPosition", Vector3.zero);
        effect.transform.LookAt(target.weakPoint.position);
        //effect.SetVector3("EndPosition", Vector3.forward  );
        effect.SetFloat("YScale", dist / 3f);
        //lineRenderer.SetPosition(0, muzzle.transform.position);
        //lineRenderer.SetPosition(1, target.weakPoint.position);
    }

    private void Start()
    {
        effect = GetComponentInChildren<VisualEffect>();
        rangeSphere.transform.localScale = new Vector3(range, range, range) / transform.localScale.x * 2f;//Set rangesphere
    }

    // Update is called once per frame
    protected override void Update()
    {
        //lineRenderer.enabled = target != null;

        base.Update();
        rangeSphere.SetActive(!inGame);

        if (target == null)
        {
            effect.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;

        //Gizmos.DrawSphere(target.transform.position, 1);
    }
}