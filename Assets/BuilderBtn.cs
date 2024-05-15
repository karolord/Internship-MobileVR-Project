using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBtn : MonoBehaviour
{
    public HouseBuilder houseBuilder;
    public ValueCollector[] valueCollector;
    private bool _Gazing;
    private float _timer = 0f;

    void Update()
        {
            if (_Gazing)
            {
                _timer += Time.deltaTime;
                if (_timer >= 2f)
                {
                    BuildWall();
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
    public void BuildWall()
    {
        houseBuilder.PlateLength = valueCollector[0].value;
        houseBuilder.WallHeight = valueCollector[1].value;
        houseBuilder.Spacing = valueCollector[2].value;
        houseBuilder.NogginsSpacing = valueCollector[3].value;
        houseBuilder.WallBuilder();
    }
   
}
