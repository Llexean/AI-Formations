using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToMaster : MonoBehaviour
{
    private ChildDronesManagement masterShipDroneManagement = null;
    public ChildDronesManagement MasterShipDroneManagement
    {
        set { masterShipDroneManagement = value; }
    }
    private int childIdx;
    public int ChildIdx
    {
        get { return childIdx; }
        set { childIdx = value; }
    }
}
