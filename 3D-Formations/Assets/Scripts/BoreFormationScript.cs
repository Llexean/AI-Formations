using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoreFormationScript : MonoBehaviour
{
    private float pointOffset = 2.5f;
    private float circleRadius = 2f;

    private float rotationSpeed = -90f * Mathf.Deg2Rad;
    private float currentRotation = 0f;

    public void CreateFormation(List<ConnectToMaster> drones, float deltaT)
    {
        int currentAmountOfDrones = drones.Count;
        currentRotation += rotationSpeed * deltaT;
        
        ////////////////////////////
        ///Only one drone
        ////////////////////////////
        if (currentAmountOfDrones == 1)
        {
            Vector3 newPos = Vector3.zero;

            newPos.z = pointOffset;
            drones[0].transform.localPosition = newPos;
        }

        ////////////////////////////
        ///Smaller or equal than 4
        ////////////////////////////
        else if (currentAmountOfDrones <= 4)
        {
            float angleIncreasePerDrone = (360f / 3) * Mathf.Deg2Rad;
            for (int i = 0; i < currentAmountOfDrones; i++)
            {
                Vector3 newPos = Vector3.zero;
                float newRot = 0f;
                if (i == (currentAmountOfDrones - 1))
                {
                    newPos.z = (pointOffset * 2);
                }
                else
                {
                    newPos.z = pointOffset;
                    newRot = Mathf.Sin(-currentRotation + (angleIncreasePerDrone * i));
                    newPos.x = circleRadius * newRot;

                    newRot = Mathf.Cos(-currentRotation + (angleIncreasePerDrone * i));
                    newPos.y = circleRadius * newRot;
                }
                drones[i].transform.localPosition = newPos;
            }
        }

        ////////////////////////////
        ///Bigger than 4
        ////////////////////////////
        else
        {
            ///The first 4
            ////////////////////////////
            float angleIncreasePerDrone = (360f / 3) * Mathf.Deg2Rad;
            for (int i = 0; i < 4; i++)
            {
                Vector3 newPos = Vector3.zero;
                float newRot = 0f;
                if (i == 3)
                {
                    newPos.z = (pointOffset * 3);
                }
                else
                {
                    newPos.z = (pointOffset * 2);
                    newRot = Mathf.Sin(-currentRotation + (angleIncreasePerDrone * i));
                    newPos.x = circleRadius * newRot;

                    newRot = Mathf.Cos(-currentRotation + (angleIncreasePerDrone * i));
                    newPos.y = circleRadius * newRot;
                }
                drones[i].transform.localPosition = newPos;
            }

            ///The rest until 10 (MAX)
            ////////////////////////////
            angleIncreasePerDrone = (360f / (currentAmountOfDrones - 4)) * Mathf.Deg2Rad;
            for (int i = 4; i < currentAmountOfDrones; i++)
            {
                Vector3 newPos = Vector3.zero;
                float newRot = 0f;
                newPos.z = pointOffset;
                newRot = Mathf.Sin(currentRotation + (angleIncreasePerDrone * (i - 4)));
                newPos.x = (circleRadius * 2) * newRot;

                newRot = Mathf.Cos(currentRotation + (angleIncreasePerDrone * (i - 4)));
                newPos.y = (circleRadius * 2) * newRot;

                drones[i].transform.localPosition = newPos;
            }
        }
    }
}
