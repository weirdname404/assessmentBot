using AssessmentBot;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bot_Application.Dialogs
{
    public class TestDialog : IDialog<object>
    {
        private Test test;
        private int currentQuestion = 0;
        private bool isQuestionShown = false;

        public TestDialog(Test _test)
        {
            test = _test;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // Show question, wait for response
            // If correct, increment current question index
            // And so on
        }

        private async Task ShowCurrentQuestion()
        {

        }
    }
}