using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Units;
using Assets.Units.FSM;

public class FsmStateAttack : FsmState
{
    private Fsm _fsm;
    private PlayerUnit _player;
    private Transform _unit;
    private float _attackDistance;

    public FsmStateAttack(Fsm fsm, PlayerUnit player, Transform unit, float attackDistance) : base(fsm)
    {
        _fsm = fsm;
        _player = player;
        _unit = unit;
        _attackDistance = attackDistance;
    }

    public override void Update()
    {
        if (Vector3.Distance(_player.transform.position, _unit.transform.position) > _attackDistance)
        {
            _fsm.SetState<FsmStateMove>();
            return;
        }
        Debug.Log("Attack");
    }
}
