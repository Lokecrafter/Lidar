using UnityEngine;

namespace Mapper {
    public class MapRenderer : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private Transform mapBackground;
        [SerializeField] private GameObject pointPrefab;

        private void Awake()
        {
            map.onMappedNewPoint += RenderNewPoint;
        }

        void RenderNewPoint(object sender, MapEventArgs args)
        {
            GameObject newPoint = Instantiate(pointPrefab, args.Point, Quaternion.identity);
            newPoint.transform.parent = mapBackground;
        }
    }
}