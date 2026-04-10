using UnityEngine;
using UnityEngine.Splines;

public class SplineRecorder : MonoBehaviour
{
    public SplineContainer targetSpline;
    
    void Update()
    {
        if (targetSpline == null) return;

        // Manual recording with Spacebar is the most reliable way to test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecordPoint();
        }
    }

    void RecordPoint()
    {
        // Convert player world position to local spline space
        Vector3 localPos = targetSpline.transform.InverseTransformPoint(transform.position);
        
        // Add the knot to the spline
        targetSpline.Spline.Add(new BezierKnot(localPos));
        
        Debug.Log("Recorded Knot #" + targetSpline.Spline.Count + " at " + transform.position);
    }
}