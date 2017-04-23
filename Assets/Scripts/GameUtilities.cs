
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static Vector3 ProjectileMotionIV(Vector3 target, Vector3 throwPos, float maxHeightOffset)
    {
        Vector3 iVelocity = new Vector3();

        Vector3 direction = new Vector3(target.x, 0, target.z) - new Vector3(throwPos.x, 0, throwPos.z);





        float range = direction.magnitude;

        Vector3 unitDirection = direction.normalized;

        //float maxYPos = target.y + maxHeightOffset;
        float maxYPos = Mathf.Lerp(1.5f,5, (range) / 30);

        if (range / 2f > maxYPos)
        {
            //maxYPos = range / 2f;
        }

        //initial velocity y
        iVelocity.y = Mathf.Sqrt(-2.0f * Physics.gravity.y * (maxYPos - throwPos.y));

        //time to reach max y
        float timeToMax = Mathf.Sqrt(-2.0f * (maxYPos - throwPos.y) / Physics.gravity.y);

        //time to return to y-target
        float timeToTargetY = Mathf.Sqrt(-2.0f * (maxYPos - target.y) / Physics.gravity.y);

        float totalTime = timeToMax + timeToTargetY;

        float horizontalVelocityMagnitude = range / totalTime;

        iVelocity.x = horizontalVelocityMagnitude * unitDirection.x;
        iVelocity.z = horizontalVelocityMagnitude * unitDirection.z;

        return iVelocity;

        
    }
}
