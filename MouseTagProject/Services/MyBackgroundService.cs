using Microsoft.OpenApi.Writers;
using MouseTagProject.Interfaces;
using MouseTagProject.Models;
using MouseTagProject.Repository.Interfaces;
using System.Text;

namespace MouseTagProject.Services
{
    public class MyBackgroundService : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;

        public MyBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                ICandidate candidate = scope.ServiceProvider.GetRequiredService<ICandidate>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromHours(24)); // Production
                    //await Task.Delay(TimeSpan.FromSeconds(5)); // Testing
                    var candidates = candidate.GetCandidatesReminder().Where(c => c.Available == true && c.WillBeContacted > DateTime.Now && c.WillBeContacted < DateTime.Now.AddDays(2)).ToList();
                    var letter = emailService.GenerateLetter(candidates);
                    emailService.SendEmail(letter);
                    Console.WriteLine("Išsiūsta!");
                }
            }
        }
    }
}