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
    private Transform m_agentParent;

    [SerializeField] private GameObject m_agentPrefab;

    [SerializeField] private MinMaxFloat m_spawnInterval;
    [SerializeField] private AnimationCurve m_spawnCountCurve;

    [SerializeField] private AgentTarget[] m_agentTargets;

    [BindComponent] private NpcItemController m_npcItemController;

    [BindComponent] private AgentTarget m_startPosition;

    public override void Start()
    {
        base.Start();
        StartCoroutine(SpawnLoop());
        m_agentParent = GameObject.FindWithTag("Agent Parent").transform;
    }

    private int GetSpawnCount()
    {
        // get a random value from the curve
        float sampledValue = m_spawnCountCurve.Evaluate(m_spawnCountCurve.GetRandomTime());

        // floor the value
        // ensure minimum of 1
        return Mathf.Max(1, Mathf.FloorToInt(sampledValue));
    }

    private CrowdAgent SpawnSingleAgent()
    {
        Vector3 startPosition = m_startPosition.GetRandomTargetPosition();
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
            // wait for a random amount of time
            yield return new WaitForSeconds(m_spawnInterval.GetRandomInRange());

            // spawn the first agent
            CrowdAgent firstAgent = SpawnSingleAgent();

            // get amount of agents to spawn
            int spawnCount = GetSpawnCount();

            // spawn those agents
            // but skip the first one as that's already done
            for (int i = 1; i < spawnCount; i++)
            {
                // wait a frame between spawns
                yield return null;

                // secondary agents should match the speed of the first agent to stay clumped
                CrowdAgent newAgent = SpawnSingleAgent();
                newAgent.MatchSpeed(firstAgent);
                GenerateItem(newAgent.gameObject);
            }
        }
    }
}