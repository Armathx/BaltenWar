using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject laserTurret;
    [SerializeField] private GameObject mortarTurret;

    [SerializeField] private GameObject gatlingTurret;

    public bool editMode = false;

    private GameObject currentTurret = null;

    private void Start()
    {
        currentTurret = laserTurret;
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
                    if (hit.collider.transform.childCount == 0)
                    {
                        currentTurret.SetActive(true);
                        currentTurret.transform.position = hit.collider.transform.position + Vector3.back * 2.5f + Vector3.right * 2.5f;
                    }
                    else
                    {
                        currentTurret.SetActive(false);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad1)) //Spawn Laser
            {
                currentTurret.SetActive(false);
                currentTurret = laserTurret;
                currentTurret.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))//Spawn Mortar
            {
                currentTurret.SetActive(false);
                currentTurret = mortarTurret;
                currentTurret.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))//Spawn Gatling
            {
                currentTurret.SetActive(false);
                currentTurret = gatlingTurret;
                currentTurret.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider != null && hit.collider.CompareTag("Slot") && hit.collider.transform.childCount == 0)
                {
                    GameObject turret = Instantiate(currentTurret, hit.collider.transform);
                    turret.transform.position = currentTurret.transform.position;
                    turret.GetComponent<Turret>().inGame = true;
                }
            }
        }
        else
        {
            currentTurret.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
    }
}