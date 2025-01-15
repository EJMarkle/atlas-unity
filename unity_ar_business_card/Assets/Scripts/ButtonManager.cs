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
    [SerializeField] private GameObject hey;

    // Social links
    private string emailURL = "mailto:emarkle99@gmail.com";
    private string instaURL = "https://www.instagram.com/evon350/";
    private string linkedInURL = "https://www.linkedin.com/in/evanmarkledev/";
    private string gitURL = "https://github.com/EJMarkle";
    private string heyURL = "https://www.youtube.com/watch?v=ZZ5LpwO-An4";

    // Reference to main camera
    private Camera mainCamera;

    // Button materials
    [SerializeField] private Material emailMaterial;
    [SerializeField] private Material instaMaterial;
    [SerializeField] private Material linkedinMaterial;
    [SerializeField] private Material gitMaterial;

    // Button press settings
    private float pressDuration = 0.3f;
    private float darknessFactor = 0.5f; 

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

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
                    GameObject hitObject = hit.collider.gameObject;

                    // Trigger button effect and open URL of respective buttons
                    if (hitObject == emailButton)
                    {
                        Debug.Log("Email button pressed");
                        StartCoroutine(ButtonPressEffect(emailMaterial, emailURL));
                    }
                    else if (hitObject == instaButton)
                    {
                        Debug.Log("Instagram button pressed");
                        StartCoroutine(ButtonPressEffect(instaMaterial, instaURL));
                    }
                    else if (hitObject == linkedinButton)
                    {
                        Debug.Log("LinkedIn button pressed");
                        StartCoroutine(ButtonPressEffect(linkedinMaterial, linkedInURL));
                    }
                    else if (hitObject == gitButton)
                    {
                        Debug.Log("GitHub button pressed");
                        StartCoroutine(ButtonPressEffect(gitMaterial, gitURL));
                    }
                    else if (hitObject == hey)
                    {
                        Application.OpenURL(heyURL);
                    }
                }
            }
        }
    }

    // button effect
    private IEnumerator ButtonPressEffect(Material buttonMaterial, string url)
    {
        // get material color and darken
        Color originalColor = buttonMaterial.color;
        buttonMaterial.color = originalColor * darknessFactor;
        yield return new WaitForSeconds(pressDuration);
        buttonMaterial.color = originalColor;
        
        // Open URL
        Application.OpenURL(url);
    }
}
