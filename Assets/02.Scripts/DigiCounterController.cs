using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public enum ePlayer
{
    ZERO = 0,
    RED = 1,
    BLUE = 2,
}

public class DigiCounterController : MonoBehaviour
{
    [SerializeField] DigiCounter _zeroCounter;
    [SerializeField] GameObject _logViewer;
    [SerializeField] TMP_Text _logText;
    [SerializeField] GameObject _cointossResultPopup;

    private DigiCounter _currentCounter;
    private ePlayer _currentPlayer;

    private StringBuilder _sb;

    private void Awake()
    {
        _sb = new StringBuilder();
        _currentCounter = _zeroCounter;
    }

    public void ClickCounter(DigiCounter clickedCounter)
    {
        int useCost = _currentCounter.Cost;

        if (ePlayer.ZERO != clickedCounter.Player && clickedCounter.Player != _currentPlayer)
        {
            //턴이 바뀜
            useCost += clickedCounter.Cost;
            RecordMemoryLog(_currentPlayer, useCost);

            _currentPlayer = clickedCounter.Player;
        }
        else
        {
            //아직 안바뀜
            useCost -= clickedCounter.Cost;
            RecordMemoryLog(_currentPlayer, useCost);
        }

        if (clickedCounter != _currentCounter)
        {
            _currentCounter.ResetToOriginColor();
            _currentCounter = clickedCounter;
        }
    }

    public void CoinToss()
    {
        _zeroCounter.OnPointerClick(null);

        _sb.Clear();

        int random = UnityEngine.Random.Range(0, 2);
        if(random == 1)
        {
            _currentPlayer = ePlayer.RED;
            _sb.Append("<color=red>RED TURN!!");
        }
        else
        {
            _currentPlayer = ePlayer.BLUE;
            _sb.Append("<color=blue>BLUE TURN!!");
        }
        _sb.Append("</color>");
        _sb.AppendLine();
        _logText.text = _sb.ToString();

        StartCoroutine(ShowCoinTossResultPopupRoutine(_currentPlayer));
    }

    public void ShowLog()
    {
        _logViewer.SetActive(!_logViewer.activeSelf);
    }

    private void RecordMemoryLog(ePlayer player, int useCost)
    {
        if (ePlayer.RED == _currentPlayer)
        {
            _sb.Append("<color=red>");
        }
        else if (ePlayer.BLUE == _currentPlayer)
        {
            _sb.Append("<color=blue>");
        }

        if (useCost >= 0)
        {
            _sb.AppendFormat("{0} used {1} cost</color>", _currentPlayer.ToString(), useCost);
        }
        else
        {
            _sb.AppendFormat("{0} gained {1} cost</color>", _currentPlayer.ToString(), -useCost);
        }
        _sb.AppendLine();
        _logText.text = _sb.ToString();
    }

    private IEnumerator ShowCoinTossResultPopupRoutine(ePlayer winner)
    {
        TMP_Text tmpText = _cointossResultPopup.GetComponentInChildren<TMP_Text>();

        if(ePlayer.RED == winner)
        {
            tmpText.text = string.Format("<color=red>RED FIRST!!</color>");
        }
        else if(ePlayer.BLUE == winner)
        {
            tmpText.text = string.Format("<color=blue>BLUE FIRST!!</color>");
        }

        _cointossResultPopup.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        _cointossResultPopup.SetActive(false);
    }
}
