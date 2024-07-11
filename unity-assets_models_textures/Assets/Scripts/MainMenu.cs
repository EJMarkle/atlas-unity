using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button level01Button;
    [SerializeField] private Button level02Button;
    [SerializeField] private Button level03Button;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private float hoverOpacity = 0.8f;
    [SerializeField] private float clickDarkenAmount = 0.2f;

    private void Start()
    {
        SetupButton(level01Button, "Level01");
        SetupButton(level02Button, "Level02");
        SetupButton(level03Button, "Level03");
        SetupButton(optionsButton, "Options");
        SetupButton(exitButton, "Exit");
    }

    private void SetupButton(Button button, string sceneName)
    {
        button.onClick.AddListener(() => LoadScene(sceneName));

        ButtonEffects buttonEffects = button.gameObject.AddComponent<ButtonEffects>();
        buttonEffects.Setup(button, hoverOpacity, clickDarkenAmount);
    }

    private void LoadScene(string sceneName)
    {
        if (sceneName == "Exit")
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private float hoverOpacity;
    private float clickDarkenAmount;
    private Color normalColor;

    public void Setup(Button btn, float hoverOpac, float clickDarken)
    {
        button = btn;
        hoverOpacity = hoverOpac;
        clickDarkenAmount = clickDarken;
        normalColor = button.image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = button.image.color;
        color.a = hoverOpacity;
        button.image.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Color darkenedColor = new Color(
            normalColor.r - clickDarkenAmount,
            normalColor.g - clickDarkenAmount,
            normalColor.b - clickDarkenAmount,
            normalColor.a
        );
        button.image.color = darkenedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button.image.color = normalColor;
    }
}