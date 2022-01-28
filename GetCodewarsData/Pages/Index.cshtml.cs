using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GetCodewarsData.Models;
using System.Text.Json;
using System.Net;

namespace GetCodewarsData.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public string UserNickNameMessage { get; set; }
        public string UserNameMessage { get; set; }
        public string UserHonorMessage { get; set; }
        public string UserRankMessage { get; set; }


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost(string? nickName)
        {
            if (nickName == null)
                Message = "Write correct Nick Name";
            else
            {
                WebClient webClient = new();
                try
                {
                    string data = webClient.DownloadString($"https://www.codewars.com/api/v1/users/{nickName}");
                    Codewars codewars = JsonSerializer.Deserialize<Codewars>(data);
                    Message = "All common information: ";
                    UserNickNameMessage = $"Nickname - {codewars.username};";
                    UserNameMessage = $"Name - {codewars.name};";
                    UserHonorMessage = $"Honor - {codewars.honor};";
                    UserRankMessage = $"Rank - {codewars.ranks.overall.name}.";
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }                      
        }
    }
}