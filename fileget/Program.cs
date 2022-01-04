using filgetlib;
// See https://aka.ms/new-console-template for more information
string version = "0.1.0";

if (args.Length > 0 )
{
    HandleCommands(args);
} 
else
{
    Console.WriteLine("Please enter a command and press ENTER:");
    string[] arguments = Console.ReadLine().Split(' ');
    HandleCommands(arguments);  
}

void PrintInfo()
{
    Console.WriteLine( "======================================");
    Console.WriteLine( "| fileget Copyright (c) LuisTheOne   |");
    Console.WriteLine($"| Version {version}                      |");
    Console.WriteLine( "| github.com/LuisThe0ne/fileget      |");
    Console.WriteLine( "======================================");
}

void PrintHelp()
{
    Console.WriteLine($"fileget V.{version} Help:");
    Console.WriteLine("web remoteUrl localoutputPath");
    Console.WriteLine("Visit the Github Repo for more Info (github.com/LuisThe0ne/fileget)");
    Console.WriteLine("");
    Console.WriteLine("Please enter a command and press ENTER:");
    string[] arguments = Console.ReadLine().Split(' ');
    HandleCommands(arguments);
}

void HandleCommands(string[] arguments)
{
    if (arguments.Length > 0)
    {
        switch (arguments[0])
        {
            case "web":
                if (arguments.Count() == 3)
                {
                    Web web = new Web();
                    web.DownloadFile(arguments[1], arguments[2]);
                } 
                else
                {
                    Console.WriteLine("Invalid Arguments");
                }               
                break;

            case "info":
                PrintInfo();
                break;

            case "help":
                PrintHelp();
                break;

            default:
                PrintHelp();
                break;
        }
    }
    else 
    {
        PrintHelp();
    }
Console.ReadKey();
}

