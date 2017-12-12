namespace AssessmentBot.Dialogs
{
    using AssessmentBot;
    using Microsoft.Bot.Builder.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;



    [Serializable]
    public class TestDialog : IDialog<object>
    {
        private Test test;
        private int currentQuestionIndex = 0;
        private int currentHintIndex = 0;
        public int numberOfMistakes = 0;
        private bool isQuestionShown = false;
        private List<Test.Question> currentQuestions;

        Random r = new Random();

        public TestDialog(Test _test)
        {
            test = _test;
            currentQuestions = test.questions;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await ShowCurrentQuestion(context);
            context.Wait(this.MessageReceivedAsync);
            //return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // Show question, wait for response
            // If correct, increment current question index
            // And so onz

            // call context.Wait(ProcessAnswer); -- checks answer

            var message = numberOfMistakes;

            if (currentQuestionIndex + 1 == currentQuestions[currentQuestionIndex].hints.Count)
            {
                /* Completes the dialog, removes it from the dialog stack, and returns the result to the parent/calling
                    dialog. */
                context.Done(message);
            }

            
            await ProcessAnswer(context, result);
            await ShowCurrentQuestion(context);
        }

        private async Task ProcessAnswer(IDialogContext context, IAwaitable<object> result)
        {
            // check answer here

            // call context.Wait(ProcessAnswer); if answer is incorrect
            // context.Wait(MessageReceivedAsync); -- ask another question

            if (result.Equals(currentQuestions[currentQuestionIndex].right_answer))
            {
                await context.PostAsync("Correct! :white_check_mark:");
                currentQuestionIndex++;
                context.Wait(MessageReceivedAsync);

            } 
            if (!result.Equals(currentQuestions[currentQuestionIndex].right_answer))
            {
                numberOfMistakes++;
                await context.PostAsync("Incorrect! :x:");
                await context.PostAsync(currentQuestions[currentQuestionIndex].hints[r.Next() * (currentQuestions[currentQuestionIndex].hints.Count)]);
                context.Wait(ProcessAnswer);
            }
        }

        private async Task ShowCurrentQuestion(IDialogContext context)
        {
            // print question and possible answers here
            if (currentQuestions[currentQuestionIndex].item_id == 1)
            {
                await context.PostAsync(currentQuestions[currentQuestionIndex].question);
                context.Wait(this.MessageReceivedAsync);
            }

            if (currentQuestions[currentQuestionIndex].item_id == 2)
            {
                PromptDialog.Choice(context, this.MessageReceivedAsync, currentQuestions[currentQuestionIndex].multiple_answers, currentQuestions[currentQuestionIndex].question, "Not a valid option", 3);
            }
        }
    }
}