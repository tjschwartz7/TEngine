using System;
using System.Collections.Generic;
using TEngine.TMath;

namespace TEngine.Components.Layouts
{
    public class RectTransform : Component
    {
        Vector2 Position = Vector2.zero;
        float Width = 0, Height = 0;
        Vector2 AnchorMin = Vector2.zero;
        Vector2 AnchorMax = Vector2.one;
        Vector2 Pivot = Vector2.zero;
        Vector2 Rotation = Vector2.zero;
        Vector2 Scale = Vector2.one;
    }
}
