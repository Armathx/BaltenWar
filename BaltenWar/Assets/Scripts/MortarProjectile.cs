using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MortarProjectile : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;

    private float distance;
    private Vector3 midpoint;

    private float damage;

    private float timer = 0;
    private float timeToHit = 3;

    private LayerMask layerMask;

    [SerializeField] private float radius = 5f;

    [SerializeField] private GameObject explosionPS;

    public Vector3 m_Start { get => start; set => start = value; }
    public Vector3 m_End { get => end; set => end = value; }
    public float Damage { get => damage; set => damage = value; }
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        midpoint = (start + end) / 2;
        distance = Vector3.Distance(start, end);
        midpoint.y += 100f;
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        float t = timer / timeToHit;

        transform.position = Vector3.Lerp(Vector3.Lerp(start, midpoint, t), Vector3.Lerp(midpoint, end, t), t);

        if (t >= 1) //Hit
        {
            foreach (var enemy in Physics.OverlapSphere(end, radius, layerMask))
            {
                enemy.GetComponent<Enemy>()?.TakeDamage(damage);
            }

            GetComponent<Renderer>().enabled = false;

            GameObject explosion = Instantiate(explosionPS, transform.position, Quaternion.identity);
            StartCoroutine(LateDelete(explosion));


        }
    }

    private IEnumerator LateDelete(GameObject _gameObject)
    {
        if (_gameObject.GetComponent<ParticleSystem>().isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(_gameObject);
        Destroy(gameObject);
    }
}