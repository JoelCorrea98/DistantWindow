using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner
{
    public List<GOAPAction> Plan(Dictionary<string, object> worldState, Dictionary<string, object> goal, List<GOAPAction> availableActions)
    {
        Debug.Log("Starting plan generation...");
        List<GOAPAction> plan = new List<GOAPAction>();

        foreach (var action in availableActions)
        {
            Debug.Log($"Evaluating action: {action.GetType().Name}");

            if (action.ArePreconditionsMet(worldState))
            {
                Debug.Log($"Preconditions met for action: {action.GetType().Name}");
                plan.Add(action);

                // Simular efectos
                worldState = action.ApplyEffects(new Dictionary<string, object>(worldState));

                if (IsGoalAchieved(worldState, goal))
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

    private bool IsGoalAchieved(Dictionary<string, object> worldState, Dictionary<string, object> goal)
    {
        foreach (var condition in goal)
        {
            if (!worldState.ContainsKey(condition.Key))
            {
                Debug.LogWarning($"Key '{condition.Key}' not found in worldState.");
                return false;
            }

            if (!(worldState[condition.Key] is int))
            {
                Debug.LogError($"Value for '{condition.Key}' is not an integer. Ensure it is correctly initialized.");
                return false;
            }

            if ((int)worldState[condition.Key] != (int)condition.Value)
            {
                Debug.LogWarning($"Condition '{condition.Key}' in goal does not match worldState. Goal: {condition.Value}, WorldState: {worldState[condition.Key]}");
                return false;
            }
        }
        return true;
    }
}
