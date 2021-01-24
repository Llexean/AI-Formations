using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFormationScript : MonoBehaviour
{
    float ringRadius = 2.5f;
    public void CreateFormation(List<ConnectToMaster> drones)
    {
        float currentAmountOfDrones = drones.Count;
        Vector3 newPos = Vector3.zero;
        float angleOffsetPerDrone = (360f / currentAmountOfDrones) * Mathf.Deg2Rad;

        for(int i = 0; i < currentAmountOfDrones; i++)
        {
            newPos.z = ringRadius * Mathf.Cos(angleOffsetPerDrone * i);
            newPos.x = ringRadius * Mathf.Sin(angleOffsetPerDrone * i);

            drones[i].transform.localPosition = newPos;
        }
    }
}
