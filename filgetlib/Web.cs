using Konsole;
using Luna.ConsoleProgressBar;
using System.ComponentModel;
using System.Net;

namespace filgetlib
{
    public class Web
    {
        //private DustInTheWind.ConsoleTools.Spinners.ProgressBar? progressBar;
        //ProgressBar pb = new ProgressBar(100);
        private int windowWidth = Console.WindowWidth;
        private int myPercentage;
        ConsoleProgressBar pb2 = new ConsoleProgressBar
        {
            NumberOfBlocks = (int)(Console.WindowWidth / 1.5),
            ForegroundColor = ConsoleColor.White,
            StartBracket = "[",
            EndBracket = "]",
            CompletedBlock = "#",
            IncompleteBlock = "·",
            AnimationSequence = UniversalProgressAnimations.Default
        };

        public void DownloadFile(string inputurl, string outputpath)
        {
            //progressBar = new DustInTheWind.ConsoleTools.Spinners.ProgressBar();
            //progressBar.EnsureBeginOfLine = true;
            ////progressBar.Length = 10;
            //progressBar.Display();

            myPercentage = 0;

            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);

            wc.DownloadFileAsync(new System.Uri (inputurl), outputpath);            
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            myPercentage = e.ProgressPercentage;

            //if ((progressBar != null) && (myPercentage % 10 == 0))
            //{
            //    progressBar.Value = e.ProgressPercentage;
            //}
            //Console.Write(e.ProgressPercentage);
            pb2.Report((double)myPercentage / 100);
            
            //pb.Refresh(myPercentage, "Downloading...");
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

            //if (progressBar != null)
            //{
            //    progressBar.Close();                
            //}
            Task.Delay(1000);
            Console.WriteLine("Download competed!");
        }

        public static void DebugTest()
        {
            DustInTheWind.ConsoleTools.Spinners.ProgressBar progressBar = new DustInTheWind.ConsoleTools.Spinners.ProgressBar();

            Task.Run<Task>(async () =>
            {
                progressBar.Display();

                for (int i = 0; i < 100; i++)
                {
                    await Task.Delay(100);
                    progressBar.Value++;
                }

            });

            progressBar.Close();
        }
    }
}