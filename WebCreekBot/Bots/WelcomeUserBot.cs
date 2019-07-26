using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebCreekBot.Bots
{
    public class WelcomeUserBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Usted escribió  [{turnContext.Activity.Text.Length}] carácteres"), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            await SendIntroCardAsync(turnContext, cancellationToken);
        }

        private static async Task SendIntroCardAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var card = new HeroCard
            {
                Title = "Bienvenidos a WebCreek!",
                Text = @"Este es un ejemplo de un bot realizado con Bot Framework de Microsoft.",
                Images = new List<CardImage>
                {
                    new CardImage("https://media.licdn.com/dms/image/C560BAQFEQiIpCKZ3Rw/company-logo_400_400/0?e=1572480000&v=beta&t=o07RA89BaDgBFXeYtX3uPXgtC2e6eoMu5tpTZfz98bY")
                },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, "Sitio Web", null, "Sitio Web", "Sitio Web", "https://webcreek.com/es/"),
                    new CardAction(ActionTypes.OpenUrl, "Trabaja con nosotros", null, "Trabaja con nosotros", "Trabaja con nosotros", "https://webcreek.com/es/careers/"),
                    new CardAction(ActionTypes.OpenUrl, "LinkedIn", null, "LinkedIn", "LinkedIn", "https://www.linkedin.com/company/webcreek-technology/")
                }
            };

            var response = MessageFactory.Attachment(card.ToAttachment());
            await turnContext.SendActivityAsync(response, cancellationToken);
        }
    }
}