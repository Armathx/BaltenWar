using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject laserTurret;
    [SerializeField] private GameObject mortarTurret;

    [SerializeField] private GameObject gatlingTurret;

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

            if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit, 1000))//classic Hit 
            {
                if (hit.collider.CompareTag("Slot"))
                {
                    sphere.transform.position = hit.collider.transform.position + Vector3.up + Vector3.back * 2.5f + Vector3.right * 2.5f;
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad1)) //Spawn Laser
            {
                if (hit.collider != null && hit.collider.CompareTag("Slot") && hit.collider.transform.childCount == 0)
                {
                    GameObject turret = Instantiate(laserTurret, hit.collider.transform);
                    turret.transform.position = sphere.transform.position + Vector3.down;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))//Spawn Mortar
            {
                if (hit.collider != null && hit.collider.CompareTag("Slot") && hit.collider.transform.childCount == 0)
                {
                    GameObject turret = Instantiate(mortarTurret, hit.collider.transform);
                    turret.transform.position = sphere.transform.position + Vector3.down;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))//Spawn Gatling
            {
                if (hit.collider != null && hit.collider.CompareTag("Slot") && hit.collider.transform.childCount == 0)
                {
                    GameObject turret = Instantiate(gatlingTurret, hit.collider.transform);
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