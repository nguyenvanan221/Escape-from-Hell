using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    [Range(0, 5)]
    [SerializeField] private float speed;
    Vector3 targetPos;

    [SerializeField] private GameObject ways;
    [SerializeField] private Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    readonly int direction = 1;

    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).transform;
        }
    }

    private void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 0;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (transform.position == targetPos)
        {
            NextPoint();
            TurnCorner();
        }
    }

    private void NextPoint()
    {
        if (pointIndex == pointCount - 1)
        {
            pointIndex = -1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
        
    }

    private void TurnCorner()
    {
        Vector3 turningAngle =  transform.rotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(turningAngle + new Vector3(0, 0, -90f));
    }
}
