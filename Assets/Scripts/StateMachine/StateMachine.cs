using System.Collections;
using System.Collections.Generic;

public class StateMachine
{
    public State state;

    public StateMachine(State initialState)
    {
        state = initialState;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (state  != null)
        {
            state.OnUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        state.OnExit();
        state = newState;
        state.OnEnter(this);
    }

}
