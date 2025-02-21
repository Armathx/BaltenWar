using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject laserTurret;
    public bool editMode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) editMode = !editMode;

        if (editMode)
        {
            RaycastHit hit;
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 49; //distance of the plane from the camera

            if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit, 1000))
            {
                if (hit.collider.CompareTag("Slot"))
                {
                    sphere.transform.position = hit.collider.transform.position + Vector3.up + Vector3.back * 2.5f + Vector3.right * 2.5f;
                }
            }
            if (Input.GetMouseButtonDown(0) && editMode)
            {
                if (hit.collider != null && hit.collider.CompareTag("Slot") && hit.collider.transform.childCount == 0)
                {
                    GameObject turret = Instantiate(laserTurret, hit.collider.transform);
                    turret.transform.position = sphere.transform.position + Vector3.down;
                }
            }
        }
        sphere.GetComponent<Renderer>().enabled = editMode;
    }

    private void OnDrawGizmos()
    {
    }
}