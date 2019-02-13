using Quartz;
using Topshelf;
using Topshelf.Quartz;

namespace CreateService2
{
    static class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<MyService>(s =>
                {
                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());
                    s.ConstructUsing(()   => new MyService());

                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                                JobBuilder.Create<MyJob>().Build())
                            .AddTrigger(() => TriggerBuilder.Create()
                                .WithSimpleSchedule(b => b
                                    .WithIntervalInSeconds(Settings.Default.Intervall)
                                    .RepeatForever())
                                .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                x.SetServiceName("My Topshelf Service");
                x.SetDisplayName("My Topshelf Service");
                x.SetDescription("My Topshelf Service's description");
            });
        }
    }

    public class MyService
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }
    }
}
