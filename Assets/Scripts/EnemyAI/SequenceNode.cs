using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BehaviourNode
{
    // List of child nodes to evaluate in sequence
    private List<BehaviourNode> children;

    public SequenceNode(List<BehaviourNode> nodes)
    {
        this.children = nodes;
    }

    // Evaluate each child node in order until one fails or all succeed
    public override NodeState Evaluate()
    {
        bool anyNodeRunning = false;

        foreach (BehaviourNode node in children)
        {
            NodeState result = node.Evaluate();

            if (result == NodeState.Failure)
            {
                State = NodeState.Failure;
                return State;
            }
            if (result == NodeState.Running)
            {
                anyNodeRunning = true;
            }
        }
        State = anyNodeRunning ? NodeState.Running : NodeState.Success;
        return State;
    }

}
