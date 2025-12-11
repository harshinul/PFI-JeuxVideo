using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Interrupt
{
    Conditions[] conditions;
    BehaviorTree behaviorTree;
    bool[] conditionsState;

    CancellationTokenSource cts;

    public Interrupt(Conditions[] conditions, BehaviorTree behaviorTree)
    {
        this.behaviorTree = behaviorTree;
        this.conditions = conditions;
        conditionsState = new bool[conditions.Length];

        Start();
    }

    async private void CheckConditions(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            for (int index = 0; index < conditions.Length; ++index)
            {
                if (conditions[index].Evaluate() != conditionsState[index])
                {
                    behaviorTree.Interupt();
                    UpdateState();
                    break;
                }
            }
            await Task.Delay(100);
        }
    }

    private void UpdateState()
    {
        for (int index = 0; index < conditions.Length; ++index)
        {
            conditionsState[index] = conditions[index].Evaluate();
        }
    }

    public void Start()
    {
        cts = new CancellationTokenSource();
        UpdateState();
        CheckConditions(cts.Token);
    }

    public void Stop()
    {
        cts.Cancel();
    }
}
