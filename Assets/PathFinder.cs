using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public WaveConfig myConfig; 
    private List<Transform> wayPoints; 
    private int wayPointIndex = 0; 

    void Start()
    {
        if (myConfig == null)
        {
            Debug.LogError("PathFinder: myConfig не установлен!");
            return;
        }

        wayPoints = myConfig.GetPoints();
        transform.position = wayPoints[wayPointIndex].position; 
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (wayPoints == null || wayPoints.Count == 0) return;

        if (wayPointIndex < wayPoints.Count)
        {
            Vector3 target = wayPoints[wayPointIndex].position;
            float delta = myConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, delta);

            if (transform.position == target)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
