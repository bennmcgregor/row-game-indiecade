using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class GameQuests
    {
        private DialogueYarnProjectScriptableObject _dialogueYarnProjectScriptableObject;

        public GameQuests(DialogueYarnProjectScriptableObject dialogueYarnProjectScriptableObject)
        {
            _dialogueYarnProjectScriptableObject = dialogueYarnProjectScriptableObject;
        }

        public Dictionary<QuestState, QuestStateProcessor> GetQuestStateProcessors(QuestStateMachineContext context)
        {
            return new Dictionary<QuestState, QuestStateProcessor>
            {
                // TODO: fix entry transition
                // TODO: add other state processors
                {
                    QuestState.ENTRY,
                    new QuestStateProcessor(context, QuestState.ENTRY, new Dictionary<QuestStateMachineTransition, QuestState>
                    {
                        { QuestStateMachineTransition.ENTRY, QuestState.ACT_1_PRE_QUEST_1 }
                    })
                },
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
                    QuestState.ENTRY,
                    new QuestStateProcessor(context, QuestState.ENTRY, new Dictionary<QuestStateMachineTransition, QuestState>
                    {
                        { QuestStateMachineTransition.ENTRY, QuestState.TEASER }
                    })
                },
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

            ChallengeInitializationData teaserStealWaterData = new ChallengeInitializationData(ChallengeConsts.TeaserStealWater);
            teaserStealWaterData.StartChallengeWithDialogue(_dialogueYarnProjectScriptableObject.TestProject, "1A", PlayerControlInputState.ROWING);

            ChallengeInitializationData teaserEscapeCanalData = new ChallengeInitializationData(ChallengeConsts.TeaserEscapeCanal);
            teaserEscapeCanalData.BackgroundMusicFilename = "gameplay_loop_music";

            ChallengeInitializationData teaserInterrogationData = new ChallengeInitializationData(ChallengeConsts.TeaserInterrogation);
            ChallengeInitializationData teaserCutsceneData = new ChallengeInitializationData(ChallengeConsts.TeaserCutscene);
            ChallengeInitializationData teaserStrandedOnIslandData = new ChallengeInitializationData(ChallengeConsts.TeaserStrandedOnIsland);

            List<ChallengeInitializationData> teaserChallenges = new List<ChallengeInitializationData>
            {
                teaserStealWaterData,
                teaserEscapeCanalData,
                teaserInterrogationData,
                teaserCutsceneData,
                teaserStrandedOnIslandData
            };

            ChallengeStateMachine teaserSM = teaserFactory.Make(ChallengeConsts.TeaserStealWater, teaserChallenges);

            QuestInitializationData teaserInitData = new QuestInitializationData(QuestState.TEASER);
            Quest teaser = new Quest(QuestState.TEASER, teaserSM, teaserInitData);

            return new Dictionary<QuestState, Quest>
            {
                { QuestState.TEASER, teaser }
            };
        }
    }
}
