using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LidarProject;

namespace Mapper
{
    public class MapEventArgs{
        public Vector2 Point { get; set; }
    }

    public class Map : MonoBehaviour
    {
        public List<Vector2> mapPoints = new List<Vector2>();
        private List<Vector3> queuedPoints = new List<Vector3>();
        [SerializeField] private int queueThreshold = 5;
        [SerializeField] private int maxQueueLength = 100;

        public EventHandler<MapEventArgs> onMappedNewPoint;

        public bool MapPoint(LidarData lidarPoint)
        {
            Vector2 newPoint = lidarPoint.ToVector2();
            newPoint.x = Mathf.RoundToInt(newPoint.x);
            newPoint.y = Mathf.RoundToInt(newPoint.y);

            foreach (Vector2 _point in mapPoints)
            {
                if(_point.x == newPoint.x)
                {
                    if (_point.y == newPoint.y) return false; //Means the point already exists on map and is unneccecary to have go through the queue.
                }
            }

            AddToQueue(newPoint);

            return true;
        }

        private void AddToQueue(Vector2 newPoint)
        {
            for (int i = 0; i < queuedPoints.Count; i++)
            {
                if (queuedPoints[i].x == newPoint.x)
                {
                    if (queuedPoints[i].y == newPoint.y)
                    {
                        //Increments amount of double hits the point has in queue
                        Vector3 incrementedQueue = new Vector3(queuedPoints[i].x, queuedPoints[i].y, queuedPoints[i].z + 1);
                        //Adds the point to map if enought double hits on queued points are recognized
                        if(incrementedQueue.z >= queueThreshold)
                        {
                            AddToMap(newPoint);
                            return;
                        }
                        queuedPoints.Add(incrementedQueue);
                        return;
                    }
                }
            }
            //If point is not in queue. Add it to queue
            queuedPoints.Add(new Vector3(newPoint.x, newPoint.y, 0f));
            ShortenQueue();
        }

        private void AddToMap(Vector2 mappedPoint)
        {
            mapPoints.Add(mappedPoint);

            //Trigger event for subscribers to render the new mapped point on screen
            onMappedNewPoint.Invoke(this, new MapEventArgs() { Point = mappedPoint });
        }

        private void ShortenQueue()
        {
            if(queuedPoints.Count > maxQueueLength) 
                queuedPoints.RemoveAt(0);
        }
    }
}