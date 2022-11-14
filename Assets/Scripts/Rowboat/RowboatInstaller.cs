using Zenject;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowboatInstaller : MonoInstaller 
    {
        [SerializeField] private RowboatPhysicsController _physicsController;
        [SerializeField] private RowboatAnimator _rowboatAnimator;
        [SerializeField] private RowboatPhysicsParametersProvider _rowboatPhysicsParametersProvider;
        [SerializeField] private RowboatPlayerInputs _rowboatPlayerInputs;

        public override void InstallBindings()
        {
            RowboatSlideState rowboatSlideState = new RowboatSlideState(0f);
            GlobalDirectionStateMachine globalDirectionStateMachine = new GlobalDirectionStateMachine();
            RowboatMaps rowboatMaps = new RowboatMaps();

            RowingStateMachineContext<RowingState, RowingStateMachineTransition> rowingStateMachineContext = new RowingStateMachineContext<RowingState, RowingStateMachineTransition>(RowingState.ENTRY);
            RowingStateMachineFactory<RowingState, RowingStateMachineTransition> rowingStateMachineFactory = new RowingStateMachineFactory<RowingState, RowingStateMachineTransition>();
            rowingStateMachineFactory.RegisterNewState(RowingState.ENTRY, new EntryRowingStateProcessor(rowingStateMachineContext, _rowboatPlayerInputs, _physicsController));
            rowingStateMachineFactory.RegisterNewState(RowingState.FORWARDS_RECOV, new ForwardsRecoveryRowingStateProcessor(rowingStateMachineContext, _rowboatPlayerInputs, _physicsController, rowboatMaps, globalDirectionStateMachine));
            rowingStateMachineFactory.RegisterNewState(RowingState.FORWARDS_DRIVE, new ForwardsDriveRowingStateProcessor(rowingStateMachineContext, _rowboatPlayerInputs, _physicsController, rowboatMaps, globalDirectionStateMachine));
            rowingStateMachineFactory.RegisterNewState(RowingState.BACKWARDS_RECOV, new BackwardsRecoveryRowingStateProcessor(rowingStateMachineContext, _rowboatPlayerInputs, _physicsController, rowboatMaps, globalDirectionStateMachine));
            rowingStateMachineFactory.RegisterNewState(RowingState.BACKWARDS_DRIVE, new BackwardsDriveRowingStateProcessor(rowingStateMachineContext, _rowboatPlayerInputs, _physicsController, rowboatMaps, globalDirectionStateMachine));
            rowingStateMachineFactory.RegisterNewState(RowingState.STOP, new StopRowingStateProcessor(rowingStateMachineContext, _rowboatPlayerInputs, _physicsController, rowboatMaps, globalDirectionStateMachine, _rowboatAnimator));
            rowingStateMachineFactory.RegisterNewState(RowingState.SPIN, new SpinRowingStateProcessor(rowingStateMachineContext, _rowboatPlayerInputs, _rowboatAnimator, globalDirectionStateMachine));
            RowingStateMachine<RowingState, RowingStateMachineTransition> rowingMotionStateMachine = rowingStateMachineFactory.Make(rowingStateMachineContext);
            _physicsController.OnDriveFinished += () => rowingMotionStateMachine.Transition(RowingStateMachineTransition.FINISH_DRIVE);
            _rowboatAnimator.OnSpinFinished += () => rowingMotionStateMachine.Transition(RowingStateMachineTransition.FINISH_SPIN);

            RowingStateMachineContext<RudderState, RudderStateMachineTransition> rudderStateMachineContext = new RowingStateMachineContext<RudderState, RudderStateMachineTransition>(RudderState.STRAIGHT);
            RowingStateMachineFactory<RudderState, RudderStateMachineTransition> rudderStateMachineFactory = new RowingStateMachineFactory<RudderState, RudderStateMachineTransition>();
            rudderStateMachineFactory.RegisterNewState(RudderState.STRAIGHT, new StraightRudderStateProcessor(rudderStateMachineContext, _rowboatPlayerInputs, rowboatMaps, globalDirectionStateMachine, _physicsController));
            rudderStateMachineFactory.RegisterNewState(RudderState.PORT, new PortRudderStateProcessor(rudderStateMachineContext, _rowboatPlayerInputs, _physicsController));
            rudderStateMachineFactory.RegisterNewState(RudderState.STAR, new StarRudderStateProcessor(rudderStateMachineContext, _rowboatPlayerInputs, _physicsController));
            RowingStateMachine<RudderState, RudderStateMachineTransition> rudderStateMachine = rudderStateMachineFactory.Make(rudderStateMachineContext);

            Container.BindInstance(rowingMotionStateMachine).AsSingle();
            Container.BindInstance(rudderStateMachine).AsSingle();
            Container.BindInstance(_physicsController).AsSingle();
            Container.BindInstance(_rowboatAnimator).AsSingle();
            Container.BindInstance(rowboatSlideState).AsSingle();
            Container.BindInstance(globalDirectionStateMachine).AsSingle();
            Container.BindInstance(_rowboatPlayerInputs).AsSingle();
            Container.BindInstance(rowboatMaps).AsSingle();
            Container.BindInstance(_rowboatPhysicsParametersProvider).AsSingle();
        }
    }
}