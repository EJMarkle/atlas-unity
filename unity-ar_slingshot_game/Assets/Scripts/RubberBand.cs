using UnityEngine;

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

    private void Update()
    {
        UpdateLineRenderers();
    }

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

    public void SetBandReleased(bool released)
    {
        isReleased = released;
    }

    private void SetupLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.positionCount = 2;
    }
}
