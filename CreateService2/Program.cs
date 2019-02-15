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
                    s.WhenStarted(service => MyService.OnStart());
                    s.WhenStopped(service => MyService.OnStop());
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

                x.SetServiceName("copySourceFile");
                x.SetDisplayName("copySourceFile");
                x.SetDescription("Copy file from source to target ...");
            });
        }
    }

    public class MyService
    {
        public static void OnStart()
        {
        }

        public static void OnStop()
        {
        }
    }
}
