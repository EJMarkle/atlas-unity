
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;
using System.Collections.Generic;

public class SlingshotInteraction : MonoBehaviour
{
    public Transform slingshotOrigin;
    public float forceMultiplier = 6f;
    public float maxPullDistance = 0.5f;
    public float maxLateralOffset = 0.3f;
    public float maxVerticalOffset = 0.1f;
    public float resetDelay = 5f;
    public ScoreManager scoreManager;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private FixedJoint fixedJoint;
    private bool isLaunched = false;
    public bool followOrigin = true;
    private Vector3 initialPosition;

    public RubberBand rubberBand;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        fixedJoint = GetComponent<FixedJoint>();

        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.selectEntered.AddListener(OnGrabbed);

        initialPosition = transform.position;
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        rb.isKinematic = true;
        isLaunched = false;
        followOrigin = false;
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            ConstrainBallPosition();
        }
        else
        {
            if (followOrigin)
            {
                transform.position = slingshotOrigin.position;
                transform.rotation = slingshotOrigin.rotation;
            }
        }
    }

    private void ConstrainBallPosition()
    {
        Vector3 localPosition = slingshotOrigin.InverseTransformPoint(transform.position);
        localPosition.z = Mathf.Clamp(localPosition.z, -maxPullDistance, 0f);
        localPosition.x = Mathf.Clamp(localPosition.x, -maxLateralOffset, maxLateralOffset);
        localPosition.y = Mathf.Clamp(localPosition.y, -maxVerticalOffset, maxVerticalOffset);
        transform.position = slingshotOrigin.TransformPoint(localPosition);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (fixedJoint != null)
        {
            Destroy(fixedJoint);
        }

        isLaunched = true;

        if (rubberBand != null)
        {
            rubberBand.SetBandReleased(true);
        }

        float pullDistance = Vector3.Distance(slingshotOrigin.position, transform.position);
        pullDistance = Mathf.Clamp(pullDistance, 0f, maxPullDistance);

        Vector3 pullDirection = slingshotOrigin.position - transform.position;
        pullDirection.y *= 0.2f;

        rb.isKinematic = false;
        rb.AddForce(pullDirection.normalized * forceMultiplier * (pullDistance / maxPullDistance), ForceMode.Impulse);

        followOrigin = false;

        scoreManager.SpendAmmo();

        int ammo = scoreManager.ammo;

        if (ammo > 0)
        {
            StartCoroutine(ResetBallAfterDelay());
        }
    }


    private IEnumerator ResetBallAfterDelay()
    {

        yield return new WaitForSeconds(resetDelay);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (slingshotOrigin != null)
        {
            transform.position = slingshotOrigin.position;
            transform.rotation = slingshotOrigin.rotation;
        }
        else
        {
            Debug.LogWarning("[Slingshot] Pocket transform is not assigned!");
        }

        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = slingshotOrigin.GetComponent<Rigidbody>();

        followOrigin = true;

        if (rubberBand != null)
        {
            rubberBand.SetBandReleased(false);
        }

        Debug.Log("[Slingshot] Ball reset to pocket!");
    }
    public void ResetBallState()
    {
        Debug.Log("[SlingshotInteraction] Attempting to reset ball state");
        
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            Debug.Log("[SlingshotInteraction] Rigidbody obtained: " + (rb != null));
        }
        
        if (slingshotOrigin == null)
        {
            Debug.LogWarning("[SlingshotInteraction] SlingshotOrigin is null during reset!");
            return;
        }
        
        isLaunched = false;
        followOrigin = true;
        
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
        
        if (rubberBand != null)
        {
            rubberBand.SetBandReleased(false);
        }
        
        transform.position = slingshotOrigin.position;
        transform.rotation = slingshotOrigin.rotation;
        
        Rigidbody originRigidbody = slingshotOrigin.GetComponent<Rigidbody>();
        if (originRigidbody != null)
        {
            if (fixedJoint != null)
            {
                Destroy(fixedJoint);
            }
            
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = originRigidbody;
        }
        else
        {
            Debug.LogWarning("[SlingshotInteraction] SlingshotOrigin has no Rigidbody!");
        }
        
        Debug.Log("[SlingshotInteraction] Ball state reset complete");
    }
}
