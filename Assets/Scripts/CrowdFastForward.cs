using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrowdFastForward : MonoBehaviour
{
    private CrowdSpawner[] m_crowdSpawners = Array.Empty<CrowdSpawner>();

    [SerializeField]
    private int m_spawnCount = 10;

    [SerializeField]
    private BoxCollider[] m_spawnBounds = null!;

    private void Start()
    {
        m_crowdSpawners = FindObjectsOfType<CrowdSpawner>();
        StartCoroutine(FastForwardCoroutine());
    }

    private IEnumerator FastForwardCoroutine()
    {
        for (int i = 0; i < m_spawnCount; i++)
        {
            yield return SpawnAgent();
        }
    }

    private IEnumerator SpawnAgent()
    {
        CrowdSpawner targetSpawner = m_crowdSpawners.GetRandomElement();

        // group all colliders by their distance to targetSpawner
        List<(float distance, BoxCollider collider)> closestBounds = new List<(float, BoxCollider)>();
        foreach (BoxCollider spawnBoundrary in m_spawnBounds)
        {
            Vector3 closestPoint = spawnBoundrary.ClosestPoint(targetSpawner.transform.position);
            float distance = Vector3.Distance(closestPoint, targetSpawner.transform.position);
            closestBounds.Add((distance, spawnBoundrary));
        }

        // sort the list by distance ascending and select the first collider
        BoxCollider closestCollider = closestBounds.OrderBy(t => t.distance).Select(t => t.collider).First();

        // get a random position in the closest collider
        Vector3 randomPosition = RandomPointInBounds(closestCollider.bounds);
        Vector3 spawnPosition = PlacePointOnGround(randomPosition, 1);

        // spawn agents there
        yield return targetSpawner.Spawn(spawnPosition);
    }

    private Vector3 PlacePointOnGround(Vector3 point, float checkHeight)
    {
        Vector3 checkPosition = new Vector3(point.x, checkHeight, point.z);

        if (!Physics.Raycast(checkPosition, Vector3.down, out RaycastHit hitInfo))
        {
            // return point as failsafe
            return point;
        }

        return hitInfo.point;
    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z));
    }
}