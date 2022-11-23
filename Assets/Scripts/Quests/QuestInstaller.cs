using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class QuestInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameQuests>().FromNew().AsSingle();
            Container.Bind<QuestRunner>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<QuestStateMachine>().FromNew().AsSingle();
        }
    }

    public QuestStateMachine MakeTeaserQuest()
    {
        StateMachineContext<TeaserChallenges, ChallengeStateMachineTransition> context = new StateMachineContext<TeaserChallenges, ChallengeStateMachineTransition>(TeaserChallenges.TUTORIAL);

        ChallengeStateProcessorFactory<TeaserChallenges> tutorialFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.TUTORIAL, context);
        tutorialFactory.RegisterTransition(ChallengeStateMachineTransition.COMPLETED, TeaserChallenges.STEAL_WATER);

        ChallengeStateProcessorFactory<TeaserChallenges> stealWaterFactory = new ChallengeStateProcessorFactory<TeaserChallenges>(TeaserChallenges.TUTORIAL, context);
        tutorialFactory.RegisterTransition(ChallengeStateMachineTransition.COMPLETED, TeaserChallenges.STEAL_WATER);



        ChallengeStateMachineFactory<TeaserChallenges> teaserFactory = new ChallengeStateMachineFactory<TeaserChallenges>(context);

        teaserFactory.RegisterNewState()


        //// Teaser
        //ChallengeStateMachineFactory teaserFactory = new ChallengeStateMachineFactory();
        //teaserFactory.RegisterNewState(ChallengeConsts.TeaserTutorial,
        //    new Dictionary<ChallengeStateMachineTransition, string>
        //    {
        //            { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserStealWater }
        //    });
        //teaserFactory.RegisterNewState(ChallengeConsts.TeaserStealWater,
        //    new Dictionary<ChallengeStateMachineTransition, string>
        //    {
        //            { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserEscapeCanal },
        //            { ChallengeStateMachineTransition.FAILED, ChallengeConsts.TeaserStealWater }
        //    }
        //);
        //teaserFactory.RegisterNewState(ChallengeConsts.TeaserEscapeCanal,
        //    new Dictionary<ChallengeStateMachineTransition, string>
        //    {
        //            { ChallengeStateMachineTransition.FAILED, ChallengeConsts.TeaserInterrogation }
        //    }
        //);
        //teaserFactory.RegisterNewState(ChallengeConsts.TeaserInterrogation,
        //    new Dictionary<ChallengeStateMachineTransition, string>
        //    {
        //            { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserCutscene }
        //    }
        //);
        //teaserFactory.RegisterNewState(ChallengeConsts.TeaserCutscene,
        //    new Dictionary<ChallengeStateMachineTransition, string>
        //    {
        //            { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserStrandedOnIsland }
        //    }
        //);
        //teaserFactory.RegisterNewState(ChallengeConsts.TeaserStrandedOnIsland,
        //    new Dictionary<ChallengeStateMachineTransition, string> { }
        //);

        //ChallengeInitializationData teaserTutorial = new ChallengeInitializationData(ChallengeConsts.TeaserTutorial);
        //teaserTutorial.StartChallengeWithDialogue("Tutorial1", PlayerControlInputState.ROWING);

        //ChallengeInitializationData teaserStealWaterData = new ChallengeInitializationData(ChallengeConsts.TeaserStealWater);
        //teaserStealWaterData.SetStealthGameplay(true, false);

        //ChallengeInitializationData teaserEscapeCanalData = new ChallengeInitializationData(ChallengeConsts.TeaserEscapeCanal);
        //teaserEscapeCanalData.SetStealthGameplay(true, true, true);
        //teaserEscapeCanalData.BackgroundMusicFilename = "westlake-night-chase";
        //teaserEscapeCanalData.ChangeSceneOnChallengeFailure(GameSceneName.INTERROGATION_ROOM);
        //teaserEscapeCanalData.StartChallengeWithDialogue("EscapeCanalChallenge1", PlayerControlInputState.ROWING);

        //ChallengeInitializationData teaserInterrogationData = new ChallengeInitializationData(ChallengeConsts.TeaserInterrogation);
        //teaserInterrogationData.ChangeSceneOnChallengeComplete(GameSceneName.TEASER_CUTSCENE);
        //teaserInterrogationData.StartChallengeWithDialogue("InterrogationChallenge1", PlayerControlInputState.ROWING);

        //ChallengeInitializationData teaserCutsceneData = new ChallengeInitializationData(ChallengeConsts.TeaserCutscene);
        //teaserCutsceneData.ChangeSceneOnChallengeComplete(GameSceneName.DOWNSTREAM_SETTLEMENT);

        //ChallengeInitializationData teaserStrandedOnIslandData = new ChallengeInitializationData(ChallengeConsts.TeaserStrandedOnIsland);

        //List<ChallengeInitializationData> teaserChallenges = new List<ChallengeInitializationData>
        //    {
        //        teaserTutorial,
        //        teaserStealWaterData,
        //        teaserEscapeCanalData,
        //        teaserInterrogationData,
        //        teaserCutsceneData,
        //        teaserStrandedOnIslandData
        //    };

        //ChallengeStateMachine teaserSM = teaserFactory.Make(ChallengeConsts.TeaserTutorial, teaserChallenges);

        //QuestInitializationData teaserInitData = new QuestInitializationData(QuestState.TEASER);
        //Quest teaser = new Quest(QuestState.TEASER, teaserSM, teaserInitData);

        //return new Dictionary<QuestState, Quest>
        //    {
        //        { QuestState.TEASER, teaser }
        //    };
    }
}
