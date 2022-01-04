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

        case "info":

            PrintInfo();
            break;
    }



    Console.ReadKey();

     void PrintInfo()
    {
        Console.WriteLine("=====================================");
        Console.WriteLine("| fileget Copyright(c) LuisTheOne   |");
        Console.WriteLine("| Version 1.0.0                     |");
        Console.WriteLine("| github.com/LuisThe0ne/fileget     |");
        Console.WriteLine("=====================================");
    }
}

