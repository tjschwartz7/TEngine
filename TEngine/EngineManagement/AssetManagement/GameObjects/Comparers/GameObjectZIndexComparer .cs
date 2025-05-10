using TEngine.Components.Transforms;

namespace TEngine.EngineManagement.AssetManagement.GameObjects.Comparers
{
    class GameObjectZIndexComparer : IComparer<GameObject>
    {
        public int Compare(GameObject? a, GameObject? b)
        {
            if (a == null || b == null) return 0;

            if(a.HasComponent<Transform>() && b.HasComponent<Transform>())
            {
                var aT = a.GetComponent<Transform>();
                var bT = b.GetComponent<Transform>();
                if (aT != null && bT != null)
                {
                    int za = (int)aT.GlobalPosition.Z;
                    int zb = (int)bT.GlobalPosition.Z;
                    return za.CompareTo(zb);
                }
            }
            return 0;
        }
    }
}
