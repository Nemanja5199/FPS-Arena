using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;

    [SerializeField]
    private Color normalColor = new Color(2f / 255f, 229f / 255f, 207f / 255f);  // #02E5CF
    [SerializeField]
    private Color hoverColor = new Color(8f / 255f, 1f / 255f, 229f / 255f);      // #0801E5

    void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
            buttonText.color = normalColor;
        else
            Debug.LogError("TextMeshProUGUI component not found in children of Play Button!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = normalColor;
    }



}
