using UnityEngine;


/// <summary>
/// Class for handling "rubber band" graphic
/// </summary>
public class RubberBand : MonoBehaviour
{
    public Transform leftBandAttachment; 
    public Transform rightBandAttachment; 
    public Transform ball;

    private LineRenderer leftLineRenderer;
    private LineRenderer rightLineRenderer;
    private bool isReleased = false;

    private void Start()
    {
        leftLineRenderer = leftBandAttachment.GetComponent<LineRenderer>();
        rightLineRenderer = rightBandAttachment.GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Update line renderers every frame
    /// </summary>
    private void Update()
    {
        UpdateLineRenderers();
    }

    /// <summary>
    /// If ball not launched, line renderer end point is set to ball position. End points are switched to other renderers start point on launch, giving the illusion of a taught rubber band
    /// </summary>
    private void UpdateLineRenderers()
    {
        if (!isReleased)
        {
            leftLineRenderer.SetPosition(0, leftBandAttachment.position);
            leftLineRenderer.SetPosition(1, ball.position);

            rightLineRenderer.SetPosition(0, rightBandAttachment.position);
            rightLineRenderer.SetPosition(1, ball.position);
        }
        else
        {
            leftLineRenderer.SetPosition(0, leftBandAttachment.position);
            leftLineRenderer.SetPosition(1, rightBandAttachment.position);

            rightLineRenderer.SetPosition(0, rightBandAttachment.position);
            rightLineRenderer.SetPosition(1, leftBandAttachment.position);
        }
    }

    /// <summary>
    /// For other scripts to set band state
    /// </summary>
    public void SetBandReleased(bool released)
    {
        isReleased = released;
    }
}
