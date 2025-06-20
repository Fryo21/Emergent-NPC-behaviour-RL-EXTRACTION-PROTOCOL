public abstract class BehaviourNode
{
    public NodeState State { get; protected set; }


    public abstract NodeState Evaluate();

}