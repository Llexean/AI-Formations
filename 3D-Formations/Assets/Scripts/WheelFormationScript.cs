using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelFormationScript : MonoBehaviour
{
    private float offsetToSide = 2f;
    private float frontOffset = 4f;

    private float rotationSpeed = -90f * Mathf.Deg2Rad;
    private float currentRotation = 0f;
    public void CreateFormation(List<ConnectToMaster> drones, float deltaT)
    {
        int currentAmountOfDrones = drones.Count;
        currentRotation += rotationSpeed * deltaT;
        for(int i = 0; i < currentAmountOfDrones; i++)
        {
            
            Vector3 newPos = Vector3.zero;
            float newRot = 0f;
            if (currentAmountOfDrones % 2 == 0)
            {
                float angleIncreasePerDrone = (360f / (currentAmountOfDrones / 2f)) * Mathf.Deg2Rad;
                if (i >= currentAmountOfDrones / 2)
                {
                    newPos.x = -offsetToSide;
                    newRot = Mathf.Sin(currentRotation + (angleIncreasePerDrone * i));
                    newPos.y = frontOffset * newRot;

                    newRot = Mathf.Cos(currentRotation + (angleIncreasePerDrone * i));
                    newPos.z = frontOffset * newRot;
                }
                else
                {
                    newPos.x = offsetToSide;
                    newRot = Mathf.Sin(currentRotation + (angleIncreasePerDrone * (i % (currentAmountOfDrones / 2))));
                    newPos.y = frontOffset * newRot;

                    newRot = Mathf.Cos(currentRotation + (angleIncreasePerDrone * (i % (currentAmountOfDrones / 2))));
                    newPos.z = frontOffset * newRot;
                }
            }
            else
            {
                float angleIncreasePerDrone = (360f / ((currentAmountOfDrones - 1) / 2f)) * Mathf.Deg2Rad;
                if (i == 0)
                {
                    newPos.z = frontOffset;
                }
                else
                {
                    if (i > (currentAmountOfDrones - 1) / 2)
                    {
                        newPos.x = -offsetToSide;
                        newRot = Mathf.Sin(currentRotation + (angleIncreasePerDrone * (i - 1)));
                        newPos.y = frontOffset * newRot;

                        newRot = Mathf.Cos(currentRotation + (angleIncreasePerDrone * (i - 1)));
                        newPos.z = frontOffset * newRot;
                    }
                    else
                    {
                        newPos.x = offsetToSide;
                        newRot = Mathf.Sin(currentRotation + (angleIncreasePerDrone * ((i - 1) % ((currentAmountOfDrones - 1) / 2))));
                        newPos.y = frontOffset * newRot;

                        newRot = Mathf.Cos(currentRotation + (angleIncreasePerDrone * ((i - 1) % ((currentAmountOfDrones - 1) / 2))));
                        newPos.z = frontOffset * newRot;
                    }
                }
            }

            drones[i].transform.localPosition = newPos;
        }
    }
}
