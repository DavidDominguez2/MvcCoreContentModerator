using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
using System.Text;

namespace MvcCoreContentModerator.Services {
    public class ServiceContentModerator {

        private readonly ContentModeratorClient client;

        public ServiceContentModerator(IConfiguration configuration) {
            // Endpoint
            string key = configuration.GetValue<string>("AzureKeys:KeyModeration");
            string endpoint = configuration.GetValue<string>("AzureKeys:EndpointModeration");

            this.client = this.Authenticate(key, endpoint);
        }

        private ContentModeratorClient Authenticate(string key, string endpoint) {
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key));
            client.Endpoint = endpoint;

            return client;
        }

        public Screen ModerateText(string text, string lang = "eng") {
            // Create a Content Moderator client and evaluate the text.
            using (this.client) {
                return
                   this.client.TextModeration.ScreenText("text/plain", new MemoryStream(Encoding.UTF8.GetBytes(text)), lang, true, true, null, true);
            }
        }



    }
}
