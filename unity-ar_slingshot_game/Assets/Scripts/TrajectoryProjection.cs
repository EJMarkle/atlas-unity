using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


/// <summary>
/// Class for drawing arc of ball trajectory
/// </summary>
public class TrajectoryPredictor : MonoBehaviour
{
    public LineRenderer trajectoryLine;
    public int resolution = 30; 
    public float gravity = -9.81f;
    public float maxSimulationTime = 2f;
    private SlingshotInteraction slingshotInteraction;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        slingshotInteraction = GetComponent<SlingshotInteraction>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (trajectoryLine == null)
        {
            trajectoryLine = gameObject.AddComponent<LineRenderer>();
        }

        trajectoryLine.enabled = false;
    }

    /// <summary>
    /// While ball is being grabbed, draw trajectory
    /// </summary>
    private void Update()
    {
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            CalculateAndDrawTrajectory();
        }
        else
        {
            trajectoryLine.enabled = false;
        }
    }

    /// <summary>
    /// Draws a parabola based on launch velocity and direction 
    /// </summary>
    private void CalculateAndDrawTrajectory()
    {
        trajectoryLine.enabled = true;
        Vector3 startPosition = transform.position;

        Vector3 pullDirection = slingshotInteraction.slingshotOrigin.position - transform.position;
        float pullStrength = pullDirection.magnitude * 5f; 
        Vector3 launchVelocity = pullDirection.normalized * pullStrength;

        Vector3[] points = new Vector3[resolution];
        float timeStep = maxSimulationTime / resolution;

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            points[i] = startPosition + (launchVelocity * t) + (0.5f * gravity * t * t * Vector3.up);
        }

        trajectoryLine.positionCount = points.Length;
        trajectoryLine.SetPositions(points);
    }
}
