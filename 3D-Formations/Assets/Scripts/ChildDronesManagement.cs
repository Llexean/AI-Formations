using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildDronesManagement : MonoBehaviour
{
    [SerializeField] private int maxAmountOfDrones;
    [SerializeField] private GameObject childDrone;
    private List<ConnectToMaster> childDronesConnectComponent = new List<ConnectToMaster>();
    private int currentAmountOfDrones = 0;
    public int CurrentAmountOfDrones
    {
        get { return currentAmountOfDrones; }
    }

    private List<Vector3> currentChildDronePositions;
    public List<Vector3> ChildDronePositions
    {
        get { return currentChildDronePositions; }
        set { currentChildDronePositions = value; }
    }

    private float elapsedSec = 0f;
    private float changeModeCooldown = 2f;

    ///
    /// FORMATIONS
    ///
    LineFormationScript lineFormation = null;
    WheelFormationScript wheelFormation = null;
    RingFormationScript ringFormation = null;
    BoreFormationScript boreFormation = null;
    enum FormationType
    {
        Line,
        Wheel,
        Ring,
        Bore,
        overflow
    }

    FormationType currentFormation = FormationType.Line;
    public void Start()
    {
        if(maxAmountOfDrones > 10)
        {
            maxAmountOfDrones = 10;
        }
        else if(maxAmountOfDrones < 1)
        {
            maxAmountOfDrones = 1;
        }
        currentAmountOfDrones = maxAmountOfDrones;
        currentChildDronePositions = new List<Vector3>();
        ChildDronesManagement CDM = gameObject.GetComponent<ChildDronesManagement>();
        for (int i = 0; i < currentAmountOfDrones; i++)
        {
            currentChildDronePositions.Add(Vector3.zero);

            if(childDrone != null)
            {
                GameObject go = Instantiate(childDrone, transform);
                go.transform.localPosition += new Vector3(5f * i + 2, 0f, 0f); 
                ConnectToMaster goCc = go.GetComponent<ConnectToMaster>();
                goCc.ChildIdx = i;
                goCc.MasterShipDroneManagement = CDM;

                childDronesConnectComponent.Add(goCc);
            }
        }
        lineFormation = GetComponent<LineFormationScript>();
        wheelFormation = GetComponent<WheelFormationScript>();
        ringFormation = GetComponent<RingFormationScript>();
        boreFormation = GetComponent<BoreFormationScript>();

        CreateFormation(0f);
    }

    private void Update()
    {
        elapsedSec += Time.deltaTime;
        if(Input.GetAxisRaw("ChangeFormation") > 0.2f)
        {
            ChangeFormation(Time.deltaTime);
        }
        CreateFormation(Time.deltaTime);
    }

    private void ChangeFormation(float deltaT)
    {
        if(elapsedSec > changeModeCooldown)
        {
            elapsedSec = 0f;
            currentFormation++;
            if(currentFormation == FormationType.overflow)
            {
                currentFormation = FormationType.Line;
            }
        }
    }
    private void CreateFormation(float deltaT)
    {
        switch(currentFormation)
        {
            case FormationType.Line:
                lineFormation.CreateFormation(childDronesConnectComponent);
                break;
            case FormationType.Wheel:
                wheelFormation.CreateFormation(childDronesConnectComponent, deltaT);
                break;
            case FormationType.Ring:
                ringFormation.CreateFormation(childDronesConnectComponent);
                break;
            case FormationType.Bore:
                boreFormation.CreateFormation(childDronesConnectComponent, deltaT);
                break;
            default:
                break;
        }
    }
}
