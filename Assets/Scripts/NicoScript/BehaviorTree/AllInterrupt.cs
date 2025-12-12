using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class AllInterrupt
{
    Conditions[] conditions;
    BehaviorTree BT;
    bool previouslyMet = false;

    CancellationTokenSource cts;

    float cooldown;
    float lastInterruptTime = -10f;

    public AllInterrupt(Conditions[] conditions, BehaviorTree BT)
    {
        this.conditions = conditions;
        this.BT = BT;

        Start();
    }

    async private void CheckConditions(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            bool allConditionsMet = true;

            foreach (Conditions c in conditions)
            {
                if (!c.Evaluate())
                {
                    allConditionsMet = false;
                    break;
                }
            }

            if (allConditionsMet && !previouslyMet)
            {
                if (Time.time - lastInterruptTime >= cooldown)
                {
                    lastInterruptTime = Time.time;
                    Debug.Log("INTERRUPT: All conditions met!");
                    BT.Interupt();
                }
            }

            previouslyMet = allConditionsMet;

            try
            {
                await Task.Delay(100, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }

    private void UpdateState()
    {
        bool allMet = true;
        foreach (Conditions c in conditions)
        {
            if (!c.Evaluate())
            {
                allMet = false;
                break;
            }
        }
        previouslyMet = allMet;
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
