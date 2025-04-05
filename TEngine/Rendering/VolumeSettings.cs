using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Rendering
{
    public class VolumeSettings
    {
        public VolumeUpdateMode UpdateMode { get; set; } = VolumeUpdateMode.EveryFrame;
        public int VolumeMask { get; set; } = ~0; // All layers by default
        public object VolumeTrigger { get; set; } = null; // Optional: reference to player or camera
    }

    public enum VolumeUpdateMode { EveryFrame, OnTriggerEnter, Manual }
}
