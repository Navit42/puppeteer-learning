using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using PuppeteerSharp;

namespace pupetter.Training
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var training = new PuppetteerTrain();
            await training.PuppeteerTrain();
        }
    }
    public class PuppetteerTrain
    {
        public async Task PuppeteerTrain()
        {
            /*
             * aprender a se conectar com o chromium
             */

            //verificar e baixar o chromium
            var chromiumPath = Path.Combine(Directory.GetCurrentDirectory(), "Chromium");
            Console.WriteLine($"Verificando o diretório do chromium: {chromiumPath}");
            if (!Directory.Exists(chromiumPath))
            {
                Directory.CreateDirectory(chromiumPath);

            }
            Console.WriteLine("OK");
            var browserFetcherOptions = new BrowserFetcherOptions { Path = chromiumPath };
            var browserFetcher = new BrowserFetcher(browserFetcherOptions);
            var executablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);
            Console.WriteLine($"Verificando a existencia do chromium: {executablePath}");
            if (!File.Exists(executablePath))
            {
                Console.WriteLine("baixando chromium");
                await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            }
            Console.WriteLine("OK");






        }


    }



    class Sample {
        public async Task PuppeteerSample()
        {
            Console.WriteLine("download chromium into a non default location");
            var currentDirectory = Directory.GetCurrentDirectory();
            var downloadPath = Path.Combine(currentDirectory, "..", "..", "CustomChromium");
            Console.WriteLine($"Tenteando inserir o chromium no caminho a seguir: {downloadPath}");
            if (!Directory.Exists(downloadPath))
            {
                Console.WriteLine("Diretório não encontrado. Criando diretório.");
                Directory.CreateDirectory(downloadPath);
            }
            Console.WriteLine("Baixando Chromiun");
            var browserFetcherOptions = new BrowserFetcherOptions { Path = downloadPath };
            var browserFetcher = new BrowserFetcher(browserFetcherOptions);
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            var executablepath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);

            if (string.IsNullOrEmpty(executablepath))
            {
                Console.WriteLine("chromium location is empty. Unable to start Chromium");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"iniciando chromium no caminho: {executablepath}");
            var options = new LaunchOptions { Headless = true, ExecutablePath = executablepath };
            using (var browser = await Puppeteer.LaunchAsync(options))
            using (var page = await browser.NewPageAsync())
            {
                await page.GoToAsync("http://www.google.com");
                var jsSelectAllAnchors = @"Array.from(document.querySelectorAll('a')).map(a => a.href);";
                var urls = await page.EvaluateExpressionAsync<string[]>(jsSelectAllAnchors);
                foreach (string url in urls)
                {
                    Console.WriteLine($"Url: {url}");
                }
                Console.WriteLine("Aperte qualquer botão para finalizar...");
                Console.ReadLine();
            }
            return;
        }
    
    }

}