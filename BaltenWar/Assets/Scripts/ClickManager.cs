using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject laserTurret;
    [SerializeField] private GameObject mortarTurret;

    [SerializeField] private GameObject gatlingTurret;

    public bool editMode = false;

    private enum TURRET_TYPE
    {
        LASER,
        MORTAR,
        GATLING,
        COUNT
    }

    private TURRET_TYPE type = TURRET_TYPE.LASER;

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
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                type = (TURRET_TYPE)(((int)type + 1) % (int)TURRET_TYPE.COUNT);
                UpdateCurrentTurret();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                type = (TURRET_TYPE)(((int)type - 1 + (int)TURRET_TYPE.COUNT) % (int)TURRET_TYPE.COUNT);
                UpdateCurrentTurret();
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

    private void UpdateCurrentTurret()
    {
        currentTurret.SetActive(false);
        Vector3 pos = currentTurret.transform.position;
        switch (type)
        {
            case TURRET_TYPE.LASER:
                currentTurret = laserTurret;
                break;

            case TURRET_TYPE.MORTAR:
                currentTurret = mortarTurret;
                break;

            case TURRET_TYPE.GATLING:
                currentTurret = gatlingTurret;
                break;
        }

        currentTurret.SetActive(true);
        currentTurret.transform.position = pos;
    }

    private void OnDrawGizmos()
    {
    }
}