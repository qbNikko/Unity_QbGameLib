using UnityEngine;
using UnityEngine.AI;

namespace QbGameLib.Component.Ai
{
    public class AgentMoveOnTarget
    {
        private NavMeshAgent _agent;
        private Transform _target;
        private float _distatntion;
        private bool _stop;
        private float _updateTime;

        public AgentMoveOnTarget(NavMeshAgent agent, Transform target,float distatntion)
        {
            _agent = agent;
            _target = target;
            _distatntion = distatntion;
            if(target!=null) agent.SetDestination(target.position);
            agent.stoppingDistance = distatntion;
        }

        public Transform Target
        {
            get => _target;
            set
            {
                _target = value;
                if(_target!=null) _agent.SetDestination(_target.position);
            }
        }

        public bool Stop
        {
            get => _stop;
            set
            {
                _stop = value;
                _agent.isStopped = value;
            }
        }

        public void Update(float deltaTime)
        {
            _updateTime+=deltaTime;
            if(_updateTime<=0.2f) return;
            _updateTime=0;
            if(_target!=null && _agent.destination != _target.position
               && (_agent.remainingDistance>_distatntion || Vector3.Distance(_agent.destination, _target.position) > _distatntion))
            {
                _agent.SetDestination(_target.position);
            }
        }
    }
}