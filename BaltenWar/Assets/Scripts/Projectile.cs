using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    private float timer = 0f;
    private float timeToHit = 1f;
    private float damage;
    private Vector3 startPos;

    public Enemy Target { get => target; set => target = value; }
    public float Damage { get => damage; set => damage = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.weakPoint.position, 20 * Time.deltaTime);

            if ((transform.position - target.weakPoint.position).sqrMagnitude < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}