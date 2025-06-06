using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private Queue<Character> turnQueue = new Queue<Character>();

    public void AddParticipant(Character participant)
    {
        if (participant != null)
        {
            turnQueue.Enqueue(participant);
            Debug.Log($"{participant.Name} has been added to the turn queue.");
        }
    }

    public Character NextTurn()
    {
        if (turnQueue.Count > 0)
        {
            Character currentTurn = turnQueue.Dequeue();
            Debug.Log($"It's {currentTurn.Name}'s turn.");
            return currentTurn;
        }
        Debug.LogWarning("The turn queue is empty.");
        return null;
    }

    public int NumberOfParticipants()
    {
        return turnQueue.Count;
    }
}