using System;
using Zenject;

namespace IndieCade
{
    public class UpdateQuestCommands : CommandRegistrar
    {
        private QuestRunner _questRunner;

        [Inject]
        public void Initialize(QuestRunner questRunner)
        {
            _questRunner = questRunner;
        }

        protected override void RegisterCommand()
        {
            _dialogueRunner.AddCommandHandler(
                "complete_challenge",
                CompleteChallenge
            );

            _dialogueRunner.AddCommandHandler(
                "fail_challenge",
                FailChallenge
            );
        }

        private void CompleteChallenge()
        {
            _questRunner.CurrentQuest.CompleteCurrentChallenge();
        }

        private void FailChallenge()
        {
            _questRunner.CurrentQuest.FailCurrentChallenge();
        }
    }
}
