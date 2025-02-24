using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private GameObject flyings;
    [SerializeField] private GameObject groundlings;

    private IEnumerator Start()
    {
        while (true)
        {
            GameObject g = Instantiate(Random.value > 0.5f ? flyings : groundlings);
            g.transform.position = start.position;

            g.GetComponent<Enemy>().goal = end;

            yield return new WaitForSeconds(0.00005f);
        }
    }
}