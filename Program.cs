using NLog;
using System.Text.Json;
String path = Directory.GetCurrentDirectory() + "//nlog.config";


//logger instance
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("program started");

//mario deserialization
string marioFileName = "mario.json";
List<Mario> marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName));

do
{
  // display choices to user
  Console.WriteLine("1) Display Mario Characters");
  Console.WriteLine("2) Add Mario Character");
  Console.WriteLine("3) Remove Mario Character");
  Console.WriteLine("Enter to quit");
  // input selection
  string? choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);
  if (choice == "1")
  {
    // Display Mario Characters
  }
  else if (choice == "2")
  {
    // Add Mario Character
  }
  else if (choice == "3")
  {
    // Remove Mario Character
  } else if (string.IsNullOrEmpty(choice)) {
        break;
  } else {
    logger.Info("Invalid choice");
  }
} while (true);
logger.Info("program ended");