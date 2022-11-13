using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class GameQuests
    {
        public QuestStateMachineContext GetQuestStateMachineContext()
        {
            return new QuestStateMachineContext(QuestState.ACT_1_PRE_QUEST_1);
        }

        public QuestStateMachineContext GetTeaserQuestStateMachineContext()
        {
            return new QuestStateMachineContext(QuestState.TEASER);
        }

        public Dictionary<QuestState, QuestStateProcessor> GetQuestStateProcessors(QuestStateMachineContext context)
        {
            return new Dictionary<QuestState, QuestStateProcessor>
            {
                // TODO: add other state processors
                {
                    QuestState.ACT_1_PRE_QUEST_1,
                    new QuestStateProcessor(context, QuestState.ACT_1_PRE_QUEST_1, new Dictionary<QuestStateMachineTransition, QuestState>
                    {
                        { QuestStateMachineTransition.NEXT_QUEST, QuestState.ACT_1_QUEST_1 }
                    })
                }
            };
        }

        public Dictionary<QuestState, QuestStateProcessor> GetTeaserQuestStateProcessors(QuestStateMachineContext context)
        {
            return new Dictionary<QuestState, QuestStateProcessor>
            {
                {
                    QuestState.TEASER,
                    new QuestStateProcessor(context, QuestState.TEASER, new Dictionary<QuestStateMachineTransition, QuestState>{})
                }
            };
        }

        public Dictionary<QuestState, Quest> GetQuests()
        {
            // TODO: Introduction

            // Act 1 Pre Quest 1
            ChallengeStateMachineFactory act1PreQuest1SMFactory = new ChallengeStateMachineFactory();
            act1PreQuest1SMFactory.RegisterNewState(ChallengeConsts.Act1PreQuest1SpeakWithParents,
                new Dictionary<ChallengeStateMachineTransition, string>
                {
                    { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.Act1PreQuest1SpeakWithOdette }
                }
            );
            act1PreQuest1SMFactory.RegisterNewState(ChallengeConsts.Act1PreQuest1SpeakWithOdette,
                new Dictionary<ChallengeStateMachineTransition, string>{}
            );
            // TODO: update Challenges initialization
            List<ChallengeInitializationData> act1PreQuest1Challenges = new List<ChallengeInitializationData>
            {
                new ChallengeInitializationData(ChallengeConsts.Act1PreQuest1SpeakWithParents),
                new ChallengeInitializationData(ChallengeConsts.Act1PreQuest1SpeakWithOdette)
            };
            ChallengeStateMachine act1PreQuest1SM = act1PreQuest1SMFactory.Make(ChallengeConsts.Act1PreQuest1SpeakWithParents, act1PreQuest1Challenges);

            QuestInitializationData act1PreQuest1InitData = new QuestInitializationData(QuestState.ACT_1_PRE_QUEST_1);
            Quest act1PreQuest1 = new Quest(QuestState.ACT_1_PRE_QUEST_1, act1PreQuest1SM, act1PreQuest1InitData);

            // AT BOTTOM:
            return new Dictionary<QuestState, Quest>
            {
                { QuestState.ACT_1_PRE_QUEST_1, act1PreQuest1 }
            };
        }

        public Dictionary<QuestState, Quest> GetTeaserQuests()
        {
            // Teaser
            ChallengeStateMachineFactory teaserFactory = new ChallengeStateMachineFactory();
            teaserFactory.RegisterNewState(ChallengeConsts.TeaserTutorial,
                new Dictionary<ChallengeStateMachineTransition, string>
                {
                    { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserStealWater }
                });
            teaserFactory.RegisterNewState(ChallengeConsts.TeaserStealWater,
                new Dictionary<ChallengeStateMachineTransition, string>
                {
                    { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserEscapeCanal },
                    { ChallengeStateMachineTransition.FAILED, ChallengeConsts.TeaserStealWater }
                }
            );
            teaserFactory.RegisterNewState(ChallengeConsts.TeaserEscapeCanal,
                new Dictionary<ChallengeStateMachineTransition, string>
                {
                    { ChallengeStateMachineTransition.FAILED, ChallengeConsts.TeaserInterrogation }
                }
            );
            teaserFactory.RegisterNewState(ChallengeConsts.TeaserInterrogation,
                new Dictionary<ChallengeStateMachineTransition, string>
                {
                    { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserCutscene }
                }
            );
            teaserFactory.RegisterNewState(ChallengeConsts.TeaserCutscene,
                new Dictionary<ChallengeStateMachineTransition, string>
                {
                    { ChallengeStateMachineTransition.COMPLETED, ChallengeConsts.TeaserStrandedOnIsland }
                }
            );
            teaserFactory.RegisterNewState(ChallengeConsts.TeaserStrandedOnIsland,
                new Dictionary<ChallengeStateMachineTransition, string>{}
            );

            ChallengeInitializationData teaserTutorial = new ChallengeInitializationData(ChallengeConsts.TeaserTutorial);
            teaserTutorial.StartChallengeWithDialogue("Tutorial1", PlayerControlInputState.ROWING);

            ChallengeInitializationData teaserStealWaterData = new ChallengeInitializationData(ChallengeConsts.TeaserStealWater);
            teaserStealWaterData.SetStealthGameplay(true, false);

            ChallengeInitializationData teaserEscapeCanalData = new ChallengeInitializationData(ChallengeConsts.TeaserEscapeCanal);
            teaserEscapeCanalData.SetStealthGameplay(true, true, true);
            teaserEscapeCanalData.BackgroundMusicFilename = "westlake-night-chase";
            teaserEscapeCanalData.ChangeSceneOnChallengeFailure(GameSceneName.INTERROGATION_ROOM);
            teaserEscapeCanalData.StartChallengeWithDialogue("EscapeCanalChallenge1", PlayerControlInputState.ROWING);

            ChallengeInitializationData teaserInterrogationData = new ChallengeInitializationData(ChallengeConsts.TeaserInterrogation);
            teaserInterrogationData.ChangeSceneOnChallengeComplete(GameSceneName.TEASER_CUTSCENE);
            teaserInterrogationData.StartChallengeWithDialogue("InterrogationChallenge1", PlayerControlInputState.ROWING);

            ChallengeInitializationData teaserCutsceneData = new ChallengeInitializationData(ChallengeConsts.TeaserCutscene);
            teaserCutsceneData.ChangeSceneOnChallengeComplete(GameSceneName.DOWNSTREAM_SETTLEMENT);

            ChallengeInitializationData teaserStrandedOnIslandData = new ChallengeInitializationData(ChallengeConsts.TeaserStrandedOnIsland);

            List<ChallengeInitializationData> teaserChallenges = new List<ChallengeInitializationData>
            {
                teaserTutorial,
                teaserStealWaterData,
                teaserEscapeCanalData,
                teaserInterrogationData,
                teaserCutsceneData,
                teaserStrandedOnIslandData
            };

            ChallengeStateMachine teaserSM = teaserFactory.Make(ChallengeConsts.TeaserTutorial, teaserChallenges);

            QuestInitializationData teaserInitData = new QuestInitializationData(QuestState.TEASER);
            Quest teaser = new Quest(QuestState.TEASER, teaserSM, teaserInitData);

            return new Dictionary<QuestState, Quest>
            {
                { QuestState.TEASER, teaser }
            };
        }
    }
}
