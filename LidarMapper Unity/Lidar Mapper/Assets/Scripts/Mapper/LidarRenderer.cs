using UnityEngine;

namespace LidarProject {
    public class LidarRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lr;
        [SerializeField] private GameObject pointPrefab;
        [SerializeField] public const int pointAmnt = 50;
        [SerializeField] private float distanceFactor = 0.1f;

        public GameObject[] points = new GameObject[pointAmnt];

        private int iterator = 0;

        void Start()
        {
            lr.positionCount = pointAmnt;
            for (int i = 0; i < pointAmnt; i++)
            {
                points[i] = Instantiate(pointPrefab);
            }
        }

        public void RenderLidarPoint(LidarData data)
        {
            Vector2 coord = getAngleCoordinates(data.angle, data.distance);
            points[iterator].transform.position = coord;
            lr.SetPosition(iterator, coord);

            iterator++;
            iterator %= pointAmnt;
        }

        private Vector2 getAngleCoordinates(double angle, double distance)
        {
            angle *= Mathf.Deg2Rad;
            distance *= distanceFactor;

            double x = Mathf.Cos((float)angle) * distance;
            double y = Mathf.Sin((float)angle) * distance;

            Vector2 returnPoint = new Vector2((float)x, (float)y);
            return (returnPoint);
        }
    }
}