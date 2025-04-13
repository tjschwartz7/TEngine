using TEngine.Components.Transforms;

namespace TEngine.GameObjects.Comparers
{
    class GameObjectZIndexComparer : IComparer<GameObject>
    {
        public int Compare(GameObject? a, GameObject? b)
        {
            if (a == null || b == null) return 0;

            int za = a.GetComponent<Transform>()?.Z ?? 0;
            int zb = b.GetComponent<Transform>()?.Z ?? 0;

            return za.CompareTo(zb);
        }
    }
}
