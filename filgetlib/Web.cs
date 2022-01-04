using Luna.ConsoleProgressBar;
using System.ComponentModel;
using System.Net;

namespace filgetlib
{
    public class Web
    {

        private int windowWidth = Console.WindowWidth;
        private int Percentage;
        ConsoleProgressBar pb2 = new ConsoleProgressBar
        {
            NumberOfBlocks = (int)(Console.WindowWidth / 1.5),
            ForegroundColor = ConsoleColor.White,
            StartBracket = "[",
            EndBracket = "]",
            CompletedBlock = "#",
            IncompleteBlock = "·",
            AnimationSequence = UniversalProgressAnimations.Default,
        };

        public void DownloadFile(string inputurl, string outputpath)
        {

            Percentage = 0;
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);
            wc.DownloadFileAsync(new System.Uri (inputurl), outputpath);            
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Percentage = e.ProgressPercentage;
            if (Percentage != null)
            {
                pb2.Report((double)Percentage / 100);
            }
        }

        private void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("File download cancelled.");
                return;
            }

            if (e.Error != null)
            {
                Console.WriteLine(e.Error.ToString());
                return;
            }
            
            Task.Delay(5000);
            pb2.Dispose();
            Console.WriteLine("");
            Console.WriteLine("Download completed!");
        }

        public static void DebugTest()
        {

        }
    }
}