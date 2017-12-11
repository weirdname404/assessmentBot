using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace AssessmentBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private const string EasyTest = "Easy";
        private const string HardTest = "Hard";

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;


            if (message.Text == "info")
            {
                await context.PostAsync("Здесь пока нет info");
            }

            // return our reply to the user
            //else { await context.PostAsync("Привет! Чтобы начать тестирвоание напиши `/start`"); } 

            else { this.ShowOptions(context); }

            //context.Wait(MessageReceivedAsync);
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { EasyTest, HardTest }, "Are you looking for an easy or hard test?", "Not a valid option", 3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case EasyTest:
                        context.Call(new EasyTestDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case HardTest:
                        context.Call(new HardTestDialog(), this.ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        //private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        //{
        //    var ticketNumber = await result;

        //    await context.PostAsync($"Thanks for contacting our support team. Your ticket number is {ticketNumber}.");
        //    context.Wait(this.MessageReceivedAsync);
        //}

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}