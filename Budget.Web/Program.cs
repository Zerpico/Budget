using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Budget.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class Utils
    {
        public static string GetGravatarUrl(string email)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("https://www.gravatar.com/avatar/");
            sb.Append(Md5EncodeText(email ?? string.Empty));

            // Size
            sb.Append($"?s={128}");
            //return if empty
            sb.Append($"&d=https://ui-avatars.com/api/{email}/128");

            return sb.ToString();
        }

        private static string Md5EncodeText(string text)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            byte[] ss = System.Security.Cryptography.MD5.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            foreach (byte b in ss)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
    }
}



/* <!--     <div class="form-group border-top bd-default pt-2">
             <a asp-controller="Account" asp-action="Register" class="d-block">Зарегистрироваться</a>
         </div>-->*/