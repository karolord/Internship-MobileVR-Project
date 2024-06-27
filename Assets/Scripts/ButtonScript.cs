using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onClick;
    private bool _Gazing;
    private float _timer = 0f;
    public GameObject _button;
    public void OnClick()
    {
        onClick.Invoke();
    }

    void Update()
    {
        if (_Gazing)
        {
            _timer += Time.deltaTime;
            if(_button != null)
            _button.GetComponent<Image>().fillAmount = _timer;

            if (_timer >= 1f)
            {
                OnClick();
                _timer = 0f;
            }

        }
        if(_button != null)
            _button.GetComponent<Image>().fillAmount = _timer;
    }

    public void OnPointerEnter()
    {
        GazedAt(true);
    }

    // OnPointerExit is called when the pointer exits the GameObject
    public void OnPointerExit()
    {
        GazedAt(false);
    }
    // GazedAt changes the object's color based on if it is being gazed at or not.
    public void GazedAt(bool gazing)
    {
        _Gazing = gazing;
        if (!gazing)
        {
            _timer = 0f;
        }

    }
}
