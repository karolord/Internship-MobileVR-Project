using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onClick;
    private bool _Gazing;
    private float _timer = 0f;
    public void OnClick()
    {
        onClick.Invoke();
    }

    void Update()
    {
        if (_Gazing)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                OnClick();
                _timer = 0f;
            }

        }
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
