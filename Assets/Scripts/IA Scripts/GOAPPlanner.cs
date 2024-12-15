using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class GOAPPlanner
{
    public List<GOAPAction> Plan(Dictionary<string, object> worldStateCopy, Dictionary<string, object> goal, List<GOAPAction> availableActions)
    {
        Debug.Log("Starting plan generation...");
        List<GOAPAction> plan = new List<GOAPAction>();
        float currentPlanHeuristic = 100000;
        foreach (var action in availableActions)
        {
            List <GOAPAction> newPlan = new List<GOAPAction>();
            Debug.Log($"Evaluating action: {action.GetType().Name}");

            if (action.ArePreconditionsMet(worldStateCopy))
            {
                Debug.Log($"Preconditions met for action: {action.GetType().Name}");


                float newPlanHeuristic = 0;
                foreach (var newPlanAction in newPlan)
                {
                    newPlanHeuristic += newPlanAction.Cost;
                }
                if(newPlanHeuristic < currentPlanHeuristic)
                {
                    currentPlanHeuristic = newPlanHeuristic;
                    plan = newPlan;
                }
                //worldState = action.ApplyEffects(new Dictionary<string, object>(worldState));
                

                if (IsGoalAchieved(worldStateCopy, goal))
                {
                    Debug.Log("Goal achieved. Plan successfully generated.");
                    return plan;
                }
            }
            else
            {
                Debug.LogWarning($"Preconditions not met for action: {action.GetType().Name}");
            }
        }

        Debug.LogWarning("No valid plan could be generated.");
        return null;
    }
    public List<GOAPAction> GeneratePlan(Dictionary<string, object> worldStateCopy, Dictionary<string, object> goal
        , List<GOAPAction> availableActions)
    {

        var openList = new PriorityQueue<(List<GOAPAction> plan, Dictionary<string, object> state, float cost)>();

        var bestPlan = new List<GOAPAction>();
        float bestCost = float.MaxValue;



        openList.Put((new List<GOAPAction>(), worldStateCopy, 0), 0);
        int watchdog = 100;
        while (openList.Count > 0 && watchdog >0)
        {
            watchdog--;

            if (watchdog == 0)
                Debug.LogWarning("Watchdog limit reached. GOAP stopped.");

            var (currentPlan, currentState, currentCost) = openList.Get();


            if (IsGoalAchieved(currentState,goal) && currentCost < bestCost)
            {
                bestCost = currentCost;
                bestPlan = currentPlan;
                continue;
            }
            foreach (var action in availableActions)
            {
                if (action.ArePreconditionsMet(currentState))
                {
                    Debug.Log(action.GetName());

                    var newState = new Dictionary<string, object>(currentState);
                    newState = action.ApplyEffects(newState);

                    var newPlan = new List<GOAPAction>(currentPlan) { action };
                    float newCost = currentCost + action.Cost;

                    openList.Put((newPlan, newState, newCost), newCost);
                }
            }
        }

        return bestPlan;

    }
        /*
        var plan = availableActions.Aggregate((plan: new List<GOAPAction>(),currentState:worldStateCopy)
            ,(currentPlan, currentAction) =>
            {
                if (worldStateCopy.ContainsKey("player"))
                {

                }
                if (currentAction.ArePreconditionsMet(currentPlan.currentState))
                {
                    var updatedState = currentAction.ApplyEffects(currentPlan.currentState);
                }

                return currentPlan;
            }).plan;

        yield return new WaitForSeconds(0.01f);

   */
    private bool IsGoalAchieved(Dictionary<string, object> worldStateCopy, Dictionary<string, object> goal)
    {
        foreach (var condition in goal)
        {
            if (!worldStateCopy.ContainsKey(condition.Key))
            {
                Debug.LogWarning($"Key '{condition.Key}' not found in worldState.");
                return false;
            }

            if (!(worldStateCopy[condition.Key] is int))
            {
                Debug.LogError($"Value for '{condition.Key}' is not an integer. Ensure it is correctly initialized.");
                return false;
            }

            if ((int)worldStateCopy[condition.Key] != (int)condition.Value)
            {
                Debug.LogWarning($"Condition '{condition.Key}' in goal does not match worldState. Goal: {condition.Value}, WorldState: {worldStateCopy[condition.Key]}");
                return false;
            }
        }
        Debug.Log("se cumplio");
        return true;
    }
}
