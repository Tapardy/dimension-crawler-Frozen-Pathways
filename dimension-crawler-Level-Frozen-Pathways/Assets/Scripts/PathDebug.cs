using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathDebug : MonoBehaviour
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        DrawPath(agent.path);
    }

    void DrawPath(NavMeshPath path)
    {
        if (path == null) return;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.blue);
        }
    }
}
