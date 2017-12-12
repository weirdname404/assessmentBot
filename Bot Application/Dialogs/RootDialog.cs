namespace AssessmentBot.Dialogs
{

    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private const string EasyTest = "Easy";
        private const string HardTest = "Hard";
        private string user_result;

        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
            *  to process that message. */
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            // Показать варианты тестов: изи, хард
            // Ждать, пока юзер не введёт правильное название, после чего создать TestDialog(), передать в конструктор экземпляр теста

            /* When MessageReceivedAsync is called, it's passed an IAwaitable<IMessageActivity>. To get the message,
             *  await the result. */
            var message = await result;

            if (message.Text.Equals("/start"))
            {
                await this.SendWelcomeMessageAsync(context);
            }
            else {
                await context.PostAsync("Type `/start` to start Assessment Bot.");
                //context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to assessment test and get ready!");

            context.Call(new TestDialog(LoadJson()), this.TestDialogResumeAfter);
        }

        //private Task TestDialogResumeAfter(IDialogContext context, IAwaitable<object> result)
        //{
        //    throw new NotImplementedException();
        //}

        private async Task TestDialogResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                //user_result = result.toString;

                await context.PostAsync($"You finished the test with { result } mistakes.");

            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("I'm sorry, I'm having issues understanding you. Let's try again.");
            }
            finally
            {
                await this.SendWelcomeMessageAsync(context);
            }
        }

        public static Test LoadJson()
        {
            using (StreamReader r = new StreamReader("testsVar2.json"))
            {
                string json = r.ReadToEnd();
                Test test = JsonConvert.DeserializeObject<Test>(json);
                return test;
            }
        }
    }
}