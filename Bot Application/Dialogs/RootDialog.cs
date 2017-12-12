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
            // TODO:
            // Показать варианты тестов: изи, хард
            // Ждать, пока юзер не введёт правильное название, после чего создать TestDialog(), передать в конструктор экземпляр теста
        }
    }
}