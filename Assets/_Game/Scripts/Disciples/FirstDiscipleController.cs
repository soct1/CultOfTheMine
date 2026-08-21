using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class FirstDiscipleController : MonoBehaviour
    {
        private enum State
        {
            Searching,
            Moving,
            Mining
        }

        [SerializeField] private MineTargetSelector targetSelector;
        [SerializeField] private DiscipleMovement movement;
        [SerializeField] private DiscipleMining mining;

        private State state;
        private MineNode currentTarget;

        private void Start()
        {
            ChangeState(State.Searching);
        }

        private void Update()
        {
            switch (state)
            {
                case State.Searching:
                    UpdateSearching();
                    break;

                case State.Moving:
                    UpdateMoving();
                    break;

                case State.Mining:
                    UpdateMining();
                    break;
            }
        }

        private void UpdateSearching()
        {
            currentTarget = targetSelector.FindNearestMine();

            if (currentTarget == null)
            {
                movement.ClearTarget();
                mining.ClearTarget();
                return;
            }

            movement.SetTarget(currentTarget);
            mining.SetTarget(currentTarget);

            ChangeState(State.Moving);
        }

        private void UpdateMoving()
        {
            if (currentTarget == null || currentTarget.IsBroken)
            {
                ChangeState(State.Searching);
                return;
            }

            if (movement.IsInMiningRange())
            {
                ChangeState(State.Mining);
            }
        }

        private void UpdateMining()
        {
            if (currentTarget == null || currentTarget.IsBroken)
            {
                mining.ClearTarget();
                movement.ClearTarget();

                currentTarget = null;
                ChangeState(State.Searching);
            }
        }

        private void ChangeState(State newState)
        {
            state = newState;
        }
    }
}