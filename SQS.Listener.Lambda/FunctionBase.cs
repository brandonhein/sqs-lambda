using Amazon.Lambda.SQSEvents;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SQS.Listener.Lambda
{
    public class FunctionBase
    {
        private readonly AppSettings _settings;
        public FunctionBase()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(env))
            {
                env = "dev";
            }

            env = env.ToLower();
            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + string.Format("/appsettings.{0}.json", env)))
            {
                var json = reader.ReadToEnd();
                _settings = JsonConvert.DeserializeObject<AppSettings>(json);
            }
        }

        protected async Task Handle(SQSEvent.SQSMessage message)
        {
            try
            {
                var result = message.ForwardTo(_settings.ForwardingUrl);
                if (result == HttpStatusCode.OK)
                {
                    Console.WriteLine("Successful Handling");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await Task.CompletedTask;
            }
        }
    }
}
