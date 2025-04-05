using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Rendering
{
    public class ProjectionSettings
    {
        public ProjectionType ProjectionType { get; set; } = ProjectionType.Orthographic;
        public FieldOfViewAxis FOVAxis { get; set; } = FieldOfViewAxis.Vertical;
        public float FieldOfView { get; set; } = 60f;
        public float NearClipPlane { get; set; } = 0.1f;
        public float FarClipPlane { get; set; } = 1000f;
        public bool PhysicalCamera { get; set; } = false;
    }

    public enum ProjectionType { Orthographic, Perspective }
    public enum FieldOfViewAxis { Vertical, Horizontal }
}
