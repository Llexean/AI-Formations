using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelFormationScript : MonoBehaviour
{
    private float offsetToSide = 5f;
    private float frontOffset = 2f;

    Vector3 currentPosition = Vector3.zero;
    public void CreateFormation(List<ConnectToMaster> drones)
    {
        int currentAmountOfDrones = drones.Count;

        for(int i = 0; i < currentAmountOfDrones; i++)
        {
            
            Vector3 newPos = Vector3.zero;
            currentPosition = drones[i].transform.position;
            float newRot = 0f;
            if (currentAmountOfDrones % 2 == 0)
            {
                float angleIncreasePerDrone = (360f / (currentAmountOfDrones / 2f)) * Mathf.Deg2Rad;
                if (i > currentAmountOfDrones / 2)
                {
                    newPos.x -= offsetToSide;
                    newRot = Mathf.Sin((angleIncreasePerDrone * i));
                    newPos.y += frontOffset * newRot;

                    newRot = Mathf.Cos((angleIncreasePerDrone * i));
                    newPos.z += frontOffset * newRot;
                }
                else
                {
                    newPos.x += offsetToSide;
                    newRot = Mathf.Sin((angleIncreasePerDrone * (i % (currentAmountOfDrones / 2))));
                    newPos.y += frontOffset * newRot;

                    newRot = Mathf.Cos((angleIncreasePerDrone * (i % (currentAmountOfDrones / 2))));
                    newPos.z += frontOffset * newRot;
                }
            }
            else
            {
                float angleIncreasePerDrone = (360f / ((currentAmountOfDrones - 1) / 2f)) * Mathf.Deg2Rad;
                if (i == 0)
                {
                    newPos.z += frontOffset;
                }
                else
                {
                    if (i > (currentAmountOfDrones - 1) / 2)
                    {
                        newPos.x -= offsetToSide;
                        newRot = Mathf.Sin((angleIncreasePerDrone * (i - 1)));
                        newPos.y += frontOffset * newRot;

                        newRot = Mathf.Cos((angleIncreasePerDrone * (i - 1)));
                        newPos.z += frontOffset * newRot;
                    }
                    else
                    {
                        newPos.x += offsetToSide;
                        newRot = Mathf.Sin((angleIncreasePerDrone * ((i - 1) % ((currentAmountOfDrones - 1) / 2))));
                        newPos.y += frontOffset * newRot;

                        newRot = Mathf.Cos((angleIncreasePerDrone * ((i - 1) % ((currentAmountOfDrones - 1) / 2))));
                        newPos.z += frontOffset * newRot;
                    }
                }
            }

            currentPosition.x = newPos.x;
            currentPosition.y += newPos.y;
            currentPosition.z += newPos.z;
            drones[i].transform.localPosition = currentPosition;
        }
    }
}
