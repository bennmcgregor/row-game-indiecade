using Zenject;
using UnityEngine;

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

            Container.BindInstance(MakeRowingMotionStateMachine(rowboatMaps, globalDirectionStateMachine)).AsSingle();
            Container.BindInstance(MakeRudderStateMachine(rowboatMaps, globalDirectionStateMachine)).AsSingle();
            Container.BindInstance(_physicsController).AsSingle();
            Container.BindInstance(_rowboatAnimator).AsSingle();
            Container.BindInstance(rowboatSlideState).AsSingle();
            Container.BindInstance(globalDirectionStateMachine).AsSingle();
            Container.BindInstance(_rowboatPlayerInputs).AsSingle();
            Container.BindInstance(rowboatMaps).AsSingle();
            Container.BindInstance(_rowboatPhysicsParametersProvider).AsSingle();
        }

        public RowingStateMachine<RowingMotionState, RowingMotionStateMachineTransition> MakeRowingMotionStateMachine(RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine)
        {
            RowingStateMachineContext<RowingMotionState, RowingMotionStateMachineTransition> context = new RowingStateMachineContext<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.ENTRY);

            RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition> entryFactory = new RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.ENTRY, context, _rowboatPlayerInputs);
            entryFactory.RegisterTransition(RowingMotionStateMachineTransition.ENTRY, RowingMotionState.FORWARDS_RECOV, delegate
            {
                _physicsController.StartRecovery(true);
            });
            entryFactory.SetNotifyInputStateMachine(false);

            RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition> forwardsRecovFactory = new RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.FORWARDS_RECOV, context, _rowboatPlayerInputs);
            forwardsRecovFactory.RegisterTransition(RowingMotionStateMachineTransition.BOW_DOWN, RowingMotionState.FORWARDS_DRIVE, delegate
            {
                _physicsController.StopRecovery();
                _physicsController.StartDrive(true);
            });
            forwardsRecovFactory.RegisterTransition(RowingMotionStateMachineTransition.STERN_DOWN, RowingMotionState.STOP, delegate {
                _physicsController.StopRecovery();
                _physicsController.StartStopBoat();
            });

            RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition> forwardsDriveFactory = new RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.FORWARDS_DRIVE, context, _rowboatPlayerInputs);
            forwardsDriveFactory.RegisterTransition(RowingMotionStateMachineTransition.FINISH_DRIVE, RowingMotionState.FORWARDS_RECOV, delegate
            {
                _physicsController.StopDrive();
                _physicsController.StartRecovery(true);
            });
            forwardsDriveFactory.RegisterTransition(RowingMotionStateMachineTransition.STERN_DOWN, RowingMotionState.STOP, delegate {
                _physicsController.StopDrive();
                _physicsController.StartStopBoat();
            });

            RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition> backwardsRecovFactory = new RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.BACKWARDS_RECOV, context, _rowboatPlayerInputs);
            backwardsRecovFactory.RegisterTransition(RowingMotionStateMachineTransition.STERN_DOWN, RowingMotionState.BACKWARDS_DRIVE, delegate
            {
                _physicsController.StopRecovery();
                _physicsController.StartDrive(false);
            });
            backwardsRecovFactory.RegisterTransition(RowingMotionStateMachineTransition.BOW_DOWN, RowingMotionState.STOP, delegate {
                _physicsController.StopRecovery();
                _physicsController.StartStopBoat();
            });

            RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition> backwardsDriveFactory = new RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.BACKWARDS_DRIVE, context, _rowboatPlayerInputs);
            backwardsDriveFactory.RegisterTransition(RowingMotionStateMachineTransition.FINISH_DRIVE, RowingMotionState.BACKWARDS_RECOV, delegate
            {
                _physicsController.StopDrive();
                _physicsController.StartRecovery(false);
            });
            backwardsDriveFactory.RegisterTransition(RowingMotionStateMachineTransition.BOW_DOWN, RowingMotionState.STOP, delegate {
                _physicsController.StopDrive();
                _physicsController.StartStopBoat();
            });

            RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition> stopFactory = new RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.STOP, context, _rowboatPlayerInputs);
            stopFactory.RegisterTransition(
                delegate (RowingMotionStateMachineTransition transition)
                {
                    InputKey sternKey = rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.STERN, globalDirectionStateMachine.CurrentState);

                    return (transition == RowingMotionStateMachineTransition.BOW_DOWN &&
                        _rowboatPlayerInputs.InputStateMachines[sternKey].CurrentState == InputState.NONE &&
                        _physicsController.CanTransitionFromStopped())
                        ||
                        (transition == RowingMotionStateMachineTransition.STERN_UP &&
                        (context.PreviousState == RowingMotionState.FORWARDS_RECOV || context.PreviousState == RowingMotionState.FORWARDS_DRIVE) &&
                        !_physicsController.CanTransitionFromStopped());
                },
                RowingMotionState.FORWARDS_RECOV,
                delegate
                {
                    _physicsController.EndStopBoat();
                    _physicsController.StartRecovery(true);
                }
            );
            stopFactory.RegisterTransition(
                delegate (RowingMotionStateMachineTransition transition) {
                    InputKey bowKey = rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.BOW, globalDirectionStateMachine.CurrentState);

                    return (transition == RowingMotionStateMachineTransition.STERN_DOWN &&
                        _rowboatPlayerInputs.InputStateMachines[bowKey].CurrentState == InputState.NONE &&
                        _physicsController.CanTransitionFromStopped())
                        ||
                        (transition == RowingMotionStateMachineTransition.BOW_UP &&
                        (context.PreviousState == RowingMotionState.BACKWARDS_RECOV || context.PreviousState == RowingMotionState.BACKWARDS_DRIVE) &&
                        !_physicsController.CanTransitionFromStopped());
                },
                RowingMotionState.BACKWARDS_RECOV,
                delegate
                {
                    _physicsController.EndStopBoat();
                    _physicsController.StartRecovery(false);
                }
            );
            stopFactory.RegisterTransition(
                delegate (RowingMotionStateMachineTransition transition)
                {
                    return transition == RowingMotionStateMachineTransition.SHIFT_DOWN && _physicsController.CanTransitionFromStopped();
                },
                RowingMotionState.SPIN,
                delegate {
                    _physicsController.EndStopBoat();
                    _rowboatAnimator.StartSpin();
                }
            );

            RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition> spinFactory = new RowingStateProcessorFactory<RowingMotionState, RowingMotionStateMachineTransition>(RowingMotionState.SPIN, context, _rowboatPlayerInputs);
            spinFactory.RegisterTransition(RowingMotionStateMachineTransition.FINISH_SPIN, RowingMotionState.STOP, delegate
            {
                _rowboatAnimator.StopSpin();
                globalDirectionStateMachine.Transition(GlobalDirectionStateMachineTransition.SPIN);
            });

            RowingStateMachineFactory<RowingMotionState, RowingMotionStateMachineTransition> factory = new RowingStateMachineFactory<RowingMotionState, RowingMotionStateMachineTransition>(context);
            factory.RegisterNewState(entryFactory.Make());
            factory.RegisterNewState(forwardsRecovFactory.Make());
            factory.RegisterNewState(forwardsDriveFactory.Make());
            factory.RegisterNewState(backwardsRecovFactory.Make());
            factory.RegisterNewState(backwardsDriveFactory.Make());
            factory.RegisterNewState(stopFactory.Make());
            factory.RegisterNewState(spinFactory.Make());

            RowingStateMachine<RowingMotionState, RowingMotionStateMachineTransition> sm = factory.Make();
            _physicsController.OnDriveFinished += () => sm.Transition(RowingMotionStateMachineTransition.FINISH_DRIVE);
            _rowboatAnimator.OnSpinFinished += () => sm.Transition(RowingMotionStateMachineTransition.FINISH_SPIN);

            return sm;
        }

        public RowingStateMachine<RudderState, RudderStateMachineTransition> MakeRudderStateMachine(RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine)
        {
            RowingStateMachineContext<RudderState, RudderStateMachineTransition> context = new RowingStateMachineContext<RudderState, RudderStateMachineTransition>(RudderState.STRAIGHT);

            RowingStateProcessorFactory<RudderState, RudderStateMachineTransition> straightFactory = new RowingStateProcessorFactory<RudderState, RudderStateMachineTransition>(RudderState.STRAIGHT, context, _rowboatPlayerInputs);
            straightFactory.RegisterTransition(
                delegate (RudderStateMachineTransition transition)
                {
                    InputKey portKey = rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.PORT, globalDirectionStateMachine.CurrentState);

                    return transition == RudderStateMachineTransition.PORT_DOWN ||
                        (transition == RudderStateMachineTransition.STAR_UP &&
                        _rowboatPlayerInputs.InputStateMachines[portKey].CurrentState == InputState.HOLD);
                },
                RudderState.PORT,
                delegate {
                    _physicsController.StartTurnRudder(false);
                }
            );
            straightFactory.RegisterTransition(
                delegate (RudderStateMachineTransition transition)
                {
                    InputKey starKey = rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.STARBOARD, globalDirectionStateMachine.CurrentState);

                    return transition == RudderStateMachineTransition.STAR_DOWN ||
                        (transition == RudderStateMachineTransition.PORT_UP &&
                        _rowboatPlayerInputs.InputStateMachines[starKey].CurrentState == InputState.HOLD);
                },
                RudderState.STAR,
                delegate {
                    _physicsController.StartTurnRudder(true);
                }
            );

            RowingStateProcessorFactory<RudderState, RudderStateMachineTransition> starFactory = new RowingStateProcessorFactory<RudderState, RudderStateMachineTransition>(RudderState.STAR, context, _rowboatPlayerInputs);
            starFactory.RegisterTransition(
                delegate (RudderStateMachineTransition transition)
                {
                    return transition == RudderStateMachineTransition.STAR_UP ||
                        transition == RudderStateMachineTransition.PORT_DOWN;
                },
                RudderState.STRAIGHT,
                delegate
                {
                    _physicsController.EndTurnRudder(true);
                    _physicsController.EndTurnRudder(false);
                }
            );

            RowingStateProcessorFactory<RudderState, RudderStateMachineTransition> portFactory = new RowingStateProcessorFactory<RudderState, RudderStateMachineTransition>(RudderState.PORT, context, _rowboatPlayerInputs);
            portFactory.RegisterTransition(
                delegate (RudderStateMachineTransition transition)
                {
                    return transition == RudderStateMachineTransition.PORT_UP ||
                        transition == RudderStateMachineTransition.STAR_DOWN;
                },
                RudderState.STRAIGHT,
                delegate
                {
                    _physicsController.EndTurnRudder(true);
                    _physicsController.EndTurnRudder(false);
                }
            );

            RowingStateMachineFactory<RudderState, RudderStateMachineTransition> factory = new RowingStateMachineFactory<RudderState, RudderStateMachineTransition>(context);
            factory.RegisterNewState(straightFactory.Make());
            factory.RegisterNewState(starFactory.Make());
            factory.RegisterNewState(portFactory.Make());

            return factory.Make();
        }
    }
}