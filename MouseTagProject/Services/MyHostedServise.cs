using MouseTagProject.Interfaces;
using MouseTagProject.Repository.Interfaces;
using System.Diagnostics;

namespace MouseTagProject.Services
{
    public class MyHostedServise : IHostedService
    {
        private readonly IEmailService _emailService;
        private readonly ICandidate _candidate;

        public MyHostedServise(IEmailService emailService, ICandidate candidate)
        {
            _emailService = emailService;
            _candidate = candidate;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    var candidates = _candidate.GetCandidates();
                    var candidatesToMakeOffer = candidates.Where(c => c.Available == true && c.WillBeContacted > DateTime.Now && c.WillBeContacted < DateTime.Now.AddDays(2));
                    _emailService.SendEmail("Labas");
                    Console.WriteLine("isiusta");

                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

