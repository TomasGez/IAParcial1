using UnityEngine;

public class ExamineState : State
{
    public ExamineState(ExamineData data, FSM stateMachine) : base(stateMachine)
    {
        _examineData = data;
    }

    private ExamineData _examineData;

    public override void Enter()
    {

    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}

[System.Serializable]
public class ExamineData
{
    [HideInInspector] public Agent _agent;
}
