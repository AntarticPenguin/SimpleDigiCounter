using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DigiCounter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ePlayer _owner;
    [SerializeField] int _cost;

    private Image _icon;
    private Color _origColor;
    private Color _clickedColor;

    private DigiCounterController _digiController;

    public ePlayer Player => _owner;
    public int Cost => _cost;

    private void Awake()
    {
        _digiController = GameObject.FindGameObjectWithTag("DigiController").GetComponent<DigiCounterController>();
        _icon = GetComponentsInChildren<Image>()[1];
        _clickedColor = new Color(1.0f, 0.7f, 0f, 1.0f);

        switch (_owner)
        {
            case ePlayer.ZERO:
                _origColor = new Color(0.3f, 0.3f, 0.3f, 1.0f);
                break;
            case ePlayer.RED:
                _origColor = new Color(1.0f, 0f, 0f, 1.0f);
                break;
            case ePlayer.BLUE:
                _origColor = new Color(0f, 0.589f, 1.0f, 1.0f);
                break;
        }

        ResetToOriginColor();
    }

    public void ResetToOriginColor()
    {
        _icon.color = _origColor;
    }

    public void ClickManually()
    {
        _icon.color = _clickedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _icon.color = _clickedColor;
        _digiController.ClickCounter(this);
    }
}