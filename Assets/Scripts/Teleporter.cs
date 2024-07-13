using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Public Variables
    public GameObject Player;
    // Private Variables
    [SerializeField] private Color _inactiveColor;
    [SerializeField] private Color _gazedAtColor;
    private Renderer _meshRenderer;
    private bool _Gazing;
    public float _timer = 0f;
    void Start()
    {
        _meshRenderer = GetComponent<Renderer>();
        _meshRenderer.material.color = _inactiveColor;
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

    }
    void Update()
    {
        if (_Gazing)
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _gazedAtColor, Time.deltaTime / 4f);
            _timer += Time.deltaTime;
            if (_timer >= 2f)
            {
                Player.transform.position = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z);
                this.gameObject.transform.parent.gameObject.GetComponent<TeleportManager>().Activate();
                _timer = 0f;
                _meshRenderer.material.color = _inactiveColor;
                _Gazing = false;
                this.gameObject.SetActive(false);

            }

        }
    }
    // OnPointerEnter is called when the pointer enters the GameObject
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
            _meshRenderer.material.color = _inactiveColor;
            _timer = 0f;
        }
       
    }
}
