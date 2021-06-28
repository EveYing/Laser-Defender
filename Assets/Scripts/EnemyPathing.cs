using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> wayPoints;
    float enemyMoveSpeed = 4f;
    int wayPointsIndex = 0;

    private void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointsIndex].transform.position;
    }

    private void Update()
    {
        if (wayPointsIndex < wayPoints.Count)
        {
            var targetPosition = wayPoints[wayPointsIndex].transform.position;
            var movementPerFrame = enemyMoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementPerFrame);
            if (transform.position == targetPosition)
            {
                wayPointsIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
