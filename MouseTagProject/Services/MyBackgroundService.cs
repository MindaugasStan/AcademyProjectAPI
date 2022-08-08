using MouseTagProject.Interfaces;
using MouseTagProject.Repository.Interfaces;
using System.Text;

namespace MouseTagProject.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly IEmailService _emailService;
        private readonly ICandidate _candidate;

        public MyBackgroundService(IEmailService emailService, ICandidate candidate)
        {
            _emailService = emailService;
            _candidate = candidate;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromHours(24)); //Production
                //await Task.Delay(TimeSpan.FromSeconds(5)); //Testing
                var candidates = _candidate.GetCandidates().Where(c => c.Available == true && c.WillBeContacted > DateTime.Now && c.WillBeContacted < DateTime.Now.AddDays(2)).ToList();
                if (candidates.Count() != 0)
                {
                    var letter = _emailService.GenerateLetter(candidates);
                    _emailService.SendEmail(letter);
                    Console.WriteLine("Išsiūsta!");
                }
            }
        }
    }
}
