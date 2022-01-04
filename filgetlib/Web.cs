using Luna.ConsoleProgressBar;
using System.ComponentModel;
using System.Net;

namespace filgetlib
{
    public class Web
    {

        private int windowWidth = Console.WindowWidth;
        private int Percentage;
        ConsoleProgressBar pb2;

        public void DownloadFile(string inputurl, string outputpath)
        {
            if (Uri.IsWellFormedUriString(inputurl, UriKind.Absolute))
            {
                Console.WriteLine("Valid Url");
                HttpWebResponse response = null;
                var request = (HttpWebRequest)WebRequest.Create(inputurl);
                request.Method = "HEAD";

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (response != null)
                    {
                        Console.WriteLine("Url is reachable");
                        response.Close();

                        if (!string.IsNullOrEmpty(outputpath))
                        {
                            string outputFolder = Path.GetDirectoryName(outputpath);
                            if (Directory.Exists(outputFolder))
                            {
                                Console.WriteLine("Output Path is valid");
                                ExecuteDownloadFile(inputurl, outputpath);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Arguments");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Arguments");
                        }
                    }
                }
            } 
            else
            {
                Console.WriteLine("Invalid Url");
            }
        }

        private void ExecuteDownloadFile(string inputurl, string outputpath)
        {
            Percentage = 0;

            pb2 = new ConsoleProgressBar
            {
                NumberOfBlocks = (int)(Console.WindowWidth / 1.5),
                ForegroundColor = ConsoleColor.White,
                StartBracket = "[",
                EndBracket = "]",
                CompletedBlock = "#",
                IncompleteBlock = "·",
                AnimationSequence = UniversalProgressAnimations.Default,
            };
            Console.WriteLine("Starting Download");
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);
            wc.DownloadFileAsync(new System.Uri(inputurl), outputpath);
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Percentage = e.ProgressPercentage;

            pb2.Report((double)Percentage / 100);
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
    }
}