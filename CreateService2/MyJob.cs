using System;
using System.IO;
using Quartz;

namespace CreateService2
{
    public class MyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var pathTarget  = Settings.Default.PathTarget;
            var pathSource  = Settings.Default.PathSource;
            var filePattern = Settings.Default.FilePattern;
            
            Console.WriteLine($"[{DateTime.Now}] Welcome jmac2050 file has been copied ...");

            var files = Directory.GetFiles(pathSource, filePattern);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fullFileNameTarget = Path.Combine(pathTarget, fileName);

                if (!File.Exists(fullFileNameTarget))
                    File.Copy(file, fullFileNameTarget);
            }
        }
    }
}