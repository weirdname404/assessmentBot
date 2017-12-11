namespace AssessmentBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Connector;
    using System.IO;
    using Newtonsoft.Json;
    using System.Diagnostics;

    [Serializable]
    public class EasyTestDialog : IDialog<object>
    {
        public static int counter = 0;
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to easy assessment test and get ready!");
            await context.PostAsync(Tests.easyTest.Keys.ToList()[counter]);
            

            var easyTestFormDialog = new FormDialog<Questions>(new Questions(), BuildSurveyForm, FormOptions.PromptInStart);

            //context.Call(easyTestFormDialog, this.ResumeAfterEasyTestFormDialog);
            context.Call(easyTestFormDialog, this.OnSurveyCompleted);
        }
        private static IForm<Questions> BuildSurveyForm()
        {
            counter++;
            return new FormBuilder<Questions>()
                .AddRemainingFields()
                .Build();
        }


        private async Task OnSurveyCompleted(IDialogContext context, IAwaitable<Questions> result)
        {
            try
            {
                var answers = await result;

                await context.PostAsync($"Got it... your answers are: \n{answers.firstQuestion},\n{answers.secondQuestion},\n{answers.thirdQuestion}.");
            }
            catch (FormCanceledException<Questions> e)
            {
                string reply;

                if (e.InnerException == null)
                {
                    reply = "You have canceled the survey";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {e.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }

            context.Done(string.Empty);
        }
        //private IForm<Questions> BuildTestForm()
        //{
        //    OnCompletionAsyncDelegate<Questions> processHotelsSearch = async (context, answer) =>
        //    {
        //        await context.PostAsync($"Checking your answers: \n{answer.first},\n{answer.second},\n{answer.third}...");
        //    };

        //    return new FormBuilder<Questions>()
        //        .Field(nameof(Questions.first))
        //        .Message("Looking for hotels in {FirstQuestion}...")
        //        .AddRemainingFields()
        //        .OnCompletion(processHotelsSearch)
        //        .Build();
        //}

        //private async Task ResumeAfterEasyTestFormDialog(IDialogContext context, IAwaitable<Questions> result)
        //{
        //    try
        //    {
        //        var searchQuery = await result;

        //        var hotels = await this.GetHotelsAsync(searchQuery);

        //        await context.PostAsync($"I found in total {hotels.Count()} hotels for your dates:");

        //        var resultMessage = context.MakeMessage();
        //        resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        //        resultMessage.Attachments = new List<Attachment>();

        //        foreach (var hotel in hotels)
        //        {
        //            HeroCard heroCard = new HeroCard()
        //            {
        //                Title = hotel.Name,
        //                Subtitle = $"{hotel.Rating} starts. {hotel.NumberOfReviews} reviews. From ${hotel.PriceStarting} per night.",
        //                Images = new List<CardImage>()
        //                {
        //                    new CardImage() { Url = hotel.Image }
        //                },
        //                Buttons = new List<CardAction>()
        //                {
        //                    new CardAction()
        //                    {
        //                        Title = "More details",
        //                        Type = ActionTypes.OpenUrl,
        //                        Value = $"https://www.bing.com/search?q=hotels+in+" + HttpUtility.UrlEncode(hotel.Location)
        //                    }
        //                }
        //            };

        //            resultMessage.Attachments.Add(heroCard.ToAttachment());
        //        }

        //        await context.PostAsync(resultMessage);
        //    }
        //    catch (FormCanceledException ex)
        //    {
        //        string reply;

        //        if (ex.InnerException == null)
        //        {
        //            reply = "You have canceled the operation. Quitting from the HotelsDialog";
        //        }
        //        else
        //        {
        //            reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
        //        }

        //        await context.PostAsync(reply);
        //    }
        //    finally
        //    {
        //        context.Done<object>(null);
        //    }
        //}

        //private async Task<IEnumerable<Hotel>> GetHotelsAsync(HotelsQuery searchQuery)
        //{
        //    var hotels = new List<Hotel>();

        //    // Filling the hotels results manually just for demo purposes
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        var random = new Random(i);
        //        Hotel hotel = new Hotel()
        //        {
        //            Name = $"{searchQuery.Destination} Hotel {i}",
        //            Location = searchQuery.Destination,
        //            Rating = random.Next(1, 5),
        //            NumberOfReviews = random.Next(0, 5000),
        //            PriceStarting = random.Next(80, 450),
        //            Image = $"https://placeholdit.imgix.net/~text?txtsize=35&txt=Hotel+{i}&w=500&h=260"
        //        };

        //        hotels.Add(hotel);
        //    }

        //    hotels.Sort((h1, h2) => h1.PriceStarting.CompareTo(h2.PriceStarting));

        //    return hotels;
        //}
    }
}