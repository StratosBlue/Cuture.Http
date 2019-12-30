using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Cuture.Http.Test.Server
{
    public class TestServer
    {
        #region �ֶ�

        public static bool HostByTestHost = true;

        public static string TestHost = $"http://{Resource.ServerAddress}:{Resource.ServerPort}";

        #endregion �ֶ�

        #region ����

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls(TestHost);
                });

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #endregion ����
    }
}