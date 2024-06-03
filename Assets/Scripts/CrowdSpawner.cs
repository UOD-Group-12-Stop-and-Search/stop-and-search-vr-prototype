using System;
using System.Collections;
using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using Items;
using JetBrains.Annotations;
using Pathing;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CrowdSpawner : ReferenceResolvedBehaviour
{
    private Transform m_agentParent = null!;

    [SerializeField] private GameObject m_agentPrefab = null!;

    [SerializeField] private MinMaxFloat m_spawnInterval;
    [SerializeField] private AnimationCurve m_spawnCountCurve = null!;

    [SerializeField] private AgentTarget[] m_agentTargets = null!;

    [SerializeField] private NpcItemController m_npcItemController = null!;

    [BindComponent] private AgentTarget m_startPosition = null!;

    public override void Start()
    {
        base.Start();
        m_agentParent = GameObject.FindWithTag("Agent Parent").transform;
        StartCoroutine(SpawnLoop());
    }

    private int GetSpawnCount()
    {
        // get a random value from the curve
        float sampledValue = m_spawnCountCurve.Evaluate(m_spawnCountCurve.GetRandomTime());

        // floor the value
        // ensure minimum of 1
        return Mathf.Max(1, Mathf.FloorToInt(sampledValue));
    }

    private CrowdAgent SpawnSingleAgent(Vector3 startPosition)
    {
        Vector3 targetPosition = GetTargetPosition();

        // set the destination for the navigation agent
        GameObject agentInstance = Instantiate(m_agentPrefab, startPosition, Quaternion.identity, m_agentParent);

        // get CrowdAgent component and initialize it
        CrowdAgent crowdAgent = agentInstance.GetComponent<CrowdAgent>();
        crowdAgent.InitAgent(targetPosition);
        return crowdAgent;
    }

    private Vector3 GetTargetPosition()
    {
        AgentTarget agentTarget = m_agentTargets.GetRandomElement();
        return agentTarget.GetRandomTargetPosition();
    }

    private void GenerateItem(GameObject npc)
    {
        if (!m_npcItemController)
        {
            Debug.LogError("No NpcItemController found");
            return;
        }
        m_npcItemController.AttachItem(npc, "ItemHolster");
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return Spawn(m_startPosition.GetRandomTargetPosition());

            // wait for a random amount of time
            yield return new WaitForSeconds(m_spawnInterval.GetRandomInRange());
        }

        // ReSharper disable once IteratorNeverReturns
        // coroutine is ended when the object lifetime ends
    }

    public IEnumerator Spawn(Vector3 startPosition)
    {
        // spawn the first agent
        CrowdAgent firstAgent = SpawnSingleAgent(startPosition);
        GenerateItem(firstAgent.gameObject);

        // get amount of agents to spawn
        int spawnCount = GetSpawnCount();

        // spawn those agents
        // but skip the first one as that's already done
        for (int i = 1; i < spawnCount; i++)
        {
            // wait a frame between spawns
            yield return null;

            // secondary agents should match the speed of the first agent to stay clumped
            CrowdAgent newAgent = SpawnSingleAgent(startPosition);
            newAgent.MatchSpeed(firstAgent);
            GenerateItem(newAgent.gameObject);
        }
    }
}