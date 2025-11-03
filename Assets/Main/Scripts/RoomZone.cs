using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Zone Settings")]
    public string zoneName; // например "Кровать", "Окно", "Дверь"
    public Color hoverColor = Color.yellow;
    public Color normalColor = Color.white;

    [Header("Visual")]
    [SerializeField] private Image highlightImage; // ссылка на UI-объект или спрайт

    private bool _isHovered;

    private void Start()
    {
        if (highlightImage != null)
            highlightImage.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
        if (highlightImage != null)
            highlightImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        if (highlightImage != null)
            highlightImage.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isHovered) return;
        RoomManager.Instance.EnterZone(this);
    }
}