using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace IndieCade
{
    public class QuestInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<QuestRunner>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInstance(MakeTeaserQuest()).AsSingle();
        }

        private QuestStateMachine MakeTeaserQuest()
        {
            StateMachineContext<TeaserChallenges, ChallengeStateMachineTransition> context = new StateMachineContext<TeaserChallenges, ChallengeStateMachineTransition>(TeaserChallenges.TUTORIAL);

            ChallengeStateProcessorFactory<TeaserChallenges> tutorialFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.TUTORIAL, context);
            tutorialFactory.RegisterTransition(ChallengeStateMachineTransition.COMPLETED, TeaserChallenges.STEAL_WATER);

            ChallengeStateProcessorFactory<TeaserChallenges> stealWaterFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.STEAL_WATER, context);
            stealWaterFactory.RegisterTransition(ChallengeStateMachineTransition.COMPLETED, TeaserChallenges.ESCAPE_CANAL);
            stealWaterFactory.RegisterTransition(ChallengeStateMachineTransition.FAILED, TeaserChallenges.STEAL_WATER);

            ChallengeStateProcessorFactory<TeaserChallenges> escapeCanalFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.ESCAPE_CANAL, context);
            escapeCanalFactory.RegisterTransition(ChallengeStateMachineTransition.FAILED, TeaserChallenges.INTERROGATION);

            ChallengeStateProcessorFactory<TeaserChallenges> interrogationFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.INTERROGATION, context);
            interrogationFactory.RegisterTransition(ChallengeStateMachineTransition.COMPLETED, TeaserChallenges.CUTSCENE);

            ChallengeStateProcessorFactory<TeaserChallenges> cutsceneFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.CUTSCENE, context);
            cutsceneFactory.RegisterTransition(ChallengeStateMachineTransition.COMPLETED, TeaserChallenges.STRANDED_ON_ISLAND);

            ChallengeStateProcessorFactory<TeaserChallenges> strandedOnIslandFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.STRANDED_ON_ISLAND, context);

            ChallengeInitializationData tutorialData = new ChallengeInitializationData(TeaserChallenges.TUTORIAL);
            tutorialData.StartChallengeWithDialogue("Tutorial1", PlayerControlInputState.ROWING);

            ChallengeInitializationData stealWaterData = new ChallengeInitializationData(TeaserChallenges.STEAL_WATER);
            stealWaterData.SetStealthGameplay(true, false);

            ChallengeInitializationData escapeCanalData = new ChallengeInitializationData(TeaserChallenges.ESCAPE_CANAL);
            escapeCanalData.SetStealthGameplay(true, true, true);
            escapeCanalData.BackgroundMusicFilename = "westlake-night-chase";
            escapeCanalData.ChangeSceneOnChallengeFailure(GameSceneName.INTERROGATION_ROOM);
            escapeCanalData.StartChallengeWithDialogue("EscapeCanalChallenge1", PlayerControlInputState.ROWING);

            ChallengeInitializationData interrogationData = new ChallengeInitializationData(TeaserChallenges.INTERROGATION);
            interrogationData.ChangeSceneOnChallengeComplete(GameSceneName.TEASER_CUTSCENE);
            interrogationData.StartChallengeWithDialogue("InterrogationChallenge1", PlayerControlInputState.ROWING);

            ChallengeInitializationData cutsceneData = new ChallengeInitializationData(TeaserChallenges.CUTSCENE);
            cutsceneData.ChangeSceneOnChallengeComplete(GameSceneName.DOWNSTREAM_SETTLEMENT);

            ChallengeInitializationData strandedOnIslandData = new ChallengeInitializationData(TeaserChallenges.STRANDED_ON_ISLAND);

            ChallengeStateMachineFactory<TeaserChallenges> teaserFactory = new ChallengeStateMachineFactory<TeaserChallenges>(context);
            teaserFactory.RegisterNewState(tutorialFactory.Make(), tutorialData);
            teaserFactory.RegisterNewState(stealWaterFactory.Make(), stealWaterData);
            teaserFactory.RegisterNewState(escapeCanalFactory.Make(), escapeCanalData);
            teaserFactory.RegisterNewState(interrogationFactory.Make(), interrogationData);
            teaserFactory.RegisterNewState(cutsceneFactory.Make(), cutsceneData);
            teaserFactory.RegisterNewState(strandedOnIslandFactory.Make(), strandedOnIslandData);

            ChallengeStateMachine<TeaserChallenges> teaserStateMachine = teaserFactory.Make();
            QuestData teaser = new QuestData(QuestState.TEASER, teaserStateMachine, new QuestInitializationData(QuestState.TEASER));

            return QuestStateMachine.Make(new List<StateData<QuestState>> { teaser });
        }
    }
}
