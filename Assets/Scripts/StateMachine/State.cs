

public abstract class State
{
    public string name;
    protected StateMachine stateMachine;
    
    public virtual void OnEnter(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnExit()
    {
    }
}
