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

    ///
    /// FORMATIONS
    ///
    LineFormationScript lineFormation = null;
    WheelFormationScript wheelFormation = null;
    RingFormationScript ringFormation = null;
    enum FormationType
    {
        Line,
        Wheel,
        Ring
    }

    FormationType currentFormation = FormationType.Wheel;
    public void Start()
    {
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

        CreateFormation();
    }

    private void Update()
    {
        if(Input.GetAxisRaw("ChangeFormation") > 0.2f)
        {
            ChangeFormation();
        }
    }

    private void ChangeFormation()
    {
        //currentFormation++;
        CreateFormation();
    }
    private void CreateFormation()
    {
        switch(currentFormation)
        {
            case FormationType.Line:
                lineFormation.CreateFormation(childDronesConnectComponent);
                break;
            case FormationType.Wheel:
                wheelFormation.CreateFormation(childDronesConnectComponent);
                break;
            case FormationType.Ring:
                ringFormation.CreateFormation(childDronesConnectComponent);
                break;
        }
    }
}
