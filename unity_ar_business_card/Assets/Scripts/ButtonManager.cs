using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Get button gameobjects
    [SerializeField] private GameObject emailButton;
    [SerializeField] private GameObject instaButton;
    [SerializeField] private GameObject linkedinButton;
    [SerializeField] private GameObject gitButton;

    // Social links
    private string emailURL = "mailto:emarkle99@gmail.com";
    private string instaURL = "https://www.instagram.com/evon350/";
    private string linkedInURL = "https://www.linkedin.com/in/evanmarkledev/";
    private string gitURL = "https://github.com/EJMarkle";

    // Reference to main camera
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Use GetTouch(0) instead of Input.touches[0]

            // Get position of touch where tap began
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;

                // Create ray from camera touch position
                Ray ray = mainCamera.ScreenPointToRay(touchPosition);
                RaycastHit hit;

                // Perform raycast
                if (Physics.Raycast(ray, out hit))
                {
                    // Get which object was hit
                    GameObject hitObject = hit.collider.gameObject;

                    // On button impact, open appropriate link
                    if (hitObject == emailButton)
                    {
                        Debug.Log("Email button pressed");
                        Application.OpenURL(emailURL);
                    }
                    else if (hitObject == instaButton)
                    {
                        Debug.Log("Instagram button pressed");
                        Application.OpenURL(instaURL);
                    }
                    else if (hitObject == linkedinButton)
                    {
                        Debug.Log("LinkedIn button pressed");
                        Application.OpenURL(linkedInURL);
                    }
                    else if (hitObject == gitButton)
                    {
                        Debug.Log("GitHub button pressed");
                        Application.OpenURL(gitURL);
                    }
                }
            }
        }
    }
}
