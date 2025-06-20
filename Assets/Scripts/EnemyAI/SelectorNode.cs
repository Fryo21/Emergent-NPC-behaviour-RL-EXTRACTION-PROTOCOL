using System.Collections.Generic;

public class SelectorNode : BehaviourNode
{
    // list of child nodes to evaluate
    private List<BehaviourNode> children;

    // Constructor to initialize the selector node with a list of child nodes
    public SelectorNode(List<BehaviourNode> nodes)
    {
        this.children = nodes;
    }
    // Evaluate through each child node in order 
    public override NodeState Evaluate()
    {
        foreach (BehaviourNode node in children)
        {
            NodeState childState = node.Evaluate();
            if (childState == NodeState.Success)
            {
                State = NodeState.Success;
                return State;
            }
            else if (childState == NodeState.Running)
            {
                State = NodeState.Running;
                return State;
            }
        }

        // If no child node succeeded or is running, return failure
        State = NodeState.Failure;
        return State;
    }
}