using System;
using System.Collections.Generic;
using TEngine.TMath;
using TEngine.Components.Transforms;

namespace TEngine.Components.UI.Layouts
{
    public class RectTransform : Transform
    {
        // Additional layout properties specific to RectTransform
        public Vector2 anchorMin;
        public Vector2 anchorMax;
        public Vector2 pivot;
        public Vector2 sizeDelta;
    }
}
