using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonAnimation : MonoBehaviour
{
    public class GraphicColor
    {
        private Graphic _graphic;
        private Color _standartColor;

        public GraphicColor(Graphic graphic)
        {
            _graphic = graphic;
            _standartColor = graphic.color;
        }

        public void AddColor(Color additionalColor)
        {
            _graphic.color = (_standartColor + additionalColor) / 2f;
        }
    }

    [SerializeField] private Graphic[] _graphics;
    [SerializeField] private Color _pressColor;
    [SerializeField] private Color _disableColor;

    private List<GraphicColor> _graphicColors = new List<GraphicColor>();

    public void Init()
    {
        foreach (var graphic in _graphics)
        {
            var newGraphicColor = new GraphicColor(graphic);
            _graphicColors.Add(newGraphicColor);
        }
    }

    public void SetStandart()
    {
        SetToAllGraphics(Color.white);
    }

    public void SetPressed()
    {
        SetToAllGraphics(_pressColor);
    }

    public void SetDisabled()
    {
        SetToAllGraphics(_disableColor);
    }

    private void SetToAllGraphics(Color additionalColor)
    {
        foreach (var graphicColor in _graphicColors)
        {
            graphicColor.AddColor(additionalColor);
        }
    }
}
