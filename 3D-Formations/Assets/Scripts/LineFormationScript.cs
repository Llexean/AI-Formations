using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFormationScript : MonoBehaviour
{
    private float offsetToFront = 2f;
    private float offsetToSide = 5f;
    public void CreateFormation(List<ConnectToMaster> drones)
    {
        int currentAmountOfDrones = drones.Count;
        for(int i = 0; i < currentAmountOfDrones; i++)
        {
            Vector3 newPos = Vector3.zero;

            newPos.z += offsetToFront;
            if(currentAmountOfDrones % 2 == 0)
            {
                newPos.x += (offsetToSide / 2);
            }
            else
            {
                newPos.x += offsetToSide;
            }
            newPos.x += (offsetToSide * ((currentAmountOfDrones / 2) - 1));
            newPos.x -= (offsetToSide * i);

            drones[i].transform.localPosition = newPos;
        }
    }
}
