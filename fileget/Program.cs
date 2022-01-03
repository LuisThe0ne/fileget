using filgetlib;
// See https://aka.ms/new-console-template for more information

if (args[0] != null)
{
    switch (args[0])
    {
        case "web":
            Web web = new Web();
            web.DownloadFile(args[1], args[2]);
            break;

        case "debug":

            Web.DebugTest();
            break;
    }


    Console.ReadKey();
}

