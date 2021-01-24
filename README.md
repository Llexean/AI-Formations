# Drone Formations in 2D and 3D

This project was made using Unity version: 2020.1.17f1 .
Steps of downloading Unity are at the very bottom of this file!

## Concept

The idea was to create a master-child like formation controller.
The master **ship** controls the what the children **drones** must do. (In this case their position and rotation)
While the master **ship** moves the children **drones** must follow it, keeping their formation active.
You'll be able to alter the formation by pressing down the key '**c**'.

```cs
        if(Input.GetAxisRaw("ChangeFormation") > 0.2f)
        {
            ChangeFormation(Time.deltaTime);
        }
```

The general idea came from a game which uses drone formations (both 2D and 3D) to give you temporarly boosts and weaken some statistics.
But whilst doing this, your drones do take damage and eventually get destroyed if you do not repair them.
You could swap between numerous formations and their were some which did not damage the drones, but also didn't gave any benefits nor did weaken your statistics.

## Drone and Ship Setup

The drones each have their own index. This is used to calculate each their new positions indepentend from each other.

The ship controls the formation and is the source of the drones their position. Wherever the ship goes, the drones will follow.
Each drone index gets their designated position at the creation of a formation.

## Formations

### Line Formation 2D

The simplest of formations. All drones form up in a straight line infront of you. They're all looking the way your ship is looking.
Both the distance between you and the drones and gap between them are easely adjustable.
```cs 
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
```
with **i** representing the index

**Outcome**

<img src="https://github.com/Llexean/AI-Formations/blob/main/Photo's/LineFormation.jpg" alt="LineFormation" width="600"/>

### ring Formation 2D

The drones form a circle around the ship. The degrees the drones are seperated from each other is calculated on how many drones there are.
```cs
        float angleOffsetPerDrone = (360f / currentAmountOfDrones) * Mathf.Deg2Rad;
```
Then they are positioned at a specific radius around the master **ship**.
```cs
            newPos.z = ringRadius * Mathf.Cos(angleOffsetPerDrone * i);
            newPos.x = ringRadius * Mathf.Sin(angleOffsetPerDrone * i);

            drones[i].transform.localPosition = newPos;
```
with **i** representing the index

**Outcome**

<img src="https://github.com/Llexean/AI-Formations/blob/main/Photo's/RingFormation.jpg" alt="RingFormation" width="600"/>

### Wheel Formation 3D

The drones are placed on a circle like position on each side of the ship. If the drones are uneven, one will be placed infront of the ship. They'll spin at a constant rate like wheels on a car. The angle is again calculated by the amount of drones on each side.
```cs
            float angleIncreasePerDrone = (360f / (currentAmountOfDrones / 2f)) * Mathf.Deg2Rad;
```
And then the drones are placed where they belong. Again, if the drones are uneven. One is placed infront of the ship.
```cs
            newPos.z = frontOffset;
```
And then the rest.
```cs
            newPos.x = -offsetToSide;
            newRot = Mathf.Sin(currentRotation + (angleIncreasePerDrone * i));
            newPos.y = frontOffset * newRot;

            newRot = Mathf.Cos(currentRotation + (angleIncreasePerDrone * i));
            newPos.z = frontOffset * newRot;
```
With the only difference in coding being x-axis when the index is halfway.

**Outcome**

<img src="https://github.com/Llexean/AI-Formations/blob/main/Gif's/WheelFormation.gif" alt="WheelFormation" width="600"/>

### Bore Formation 3D

Bore formation, being the most difficult formation is again based on the amount of drones. If only one drone is active, he'll be assigned a position infront of the ship. If more than one, but less than 5 are active. You'll get one in the front ant 3 circling between you and the one in front.
```cs
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
```

**Bore With Four**

If more are activated your outcome will be calculated differently. 1 in the front, 3 behind that one and the rest between the ship in the 3 spinning in the opposite direction.
```cs
            Vector3 newPos = Vector3.zero;
            float newRot = 0f;
            newPos.z = pointOffset;
            newRot = Mathf.Sin(currentRotation + (angleIncreasePerDrone * (i - 4)));
            newPos.x = (circleRadius * 2) * newRot;

            newRot = Mathf.Cos(currentRotation + (angleIncreasePerDrone * (i - 4)));
            newPos.y = (circleRadius * 2) * newRot;

            drones[i].transform.localPosition = newPos;
```

**Outcome**

<img src="https://github.com/Llexean/AI-Formations/blob/main/Gif's/BoreFormation.gif" alt="BoreFormation" width="600"/>

## Conclusion

You would come out on 4 modes of which 2 are 2D and the other 2 are 3D. Offcours there could be many other added and altered. By switching the modes with the button "**c**". Which has a cooldown of 2 seconds before you could use it again. It increases the value in an enum class called **FormationType**
```cs
            enum FormationType
            {
                Line,
                Wheel,
                Ring,
                Bore,
                overflow
            }
```
You would come out on this.

**Outcome**

<img src="https://github.com/Llexean/AI-Formations/blob/main/Gif's/ChangeMode.gif" alt="ChangeMode" width="800"/>

## Extra's

As extra's I could've added a little more. I've been held back by my time left to complete this project. But this doesn't mean that the project is complete, it is far from complete. 

New formation could be added, an improved damage system and the availability to remove and swap drones mid flight. These are all the idea's I failed to execute in this project. However no one is holding me back from doing those in the future. 

I played the game which features those things for quite a while now and to see what I can improve on their system. Makes me want to dig into that more.

## Unity download

**- Official site of unity3d**

[Unity3D](https://unity3d.com/get-unity/download)

**- Using C# as programming language**

**- Using ScreenToGif for GIF's**

[ScreenToGif](https://www.screentogif.com/)

**- Using LightShot for images**

[LightShot](https://app.prntscr.com/en/download.html)


