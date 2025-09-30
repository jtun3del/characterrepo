using NLog;
using System.Data.Common;
using System.Reflection;
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
        foreach (var i in marios)
        {
            Console.WriteLine(i.Display());
        }
    }
    else if (choice == "2")
    {
        // Add Mario Character
        //id making
        Mario mario = new()
        {
            Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
        };

        InputCharacter(mario);
        //add the char
        marios.Add(mario);
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"character created: {mario.Name}");
        
    }
    else if (choice == "3")
    {
        // Remove Mario Character
    }
    else if (string.IsNullOrEmpty(choice))
    {
        break;
    }
    else
    {
        logger.Info("Invalid choice");
    }
} while (true);
logger.Info("program ended");

static void InputCharacter(Character character)
{

  Type type = character.GetType();

  PropertyInfo[] properties = type.GetProperties();

  var props = properties.Where(p => p.Name != "Id");

  foreach (PropertyInfo prop in props)

  {

    if (prop.PropertyType == typeof(string))

    {

      Console.WriteLine($"Enter {prop.Name}:");

      prop.SetValue(character, Console.ReadLine());

    } else if (prop.PropertyType == typeof(List<string>)) {

      List<string> list = [];

      do {

        Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");

        string response = Console.ReadLine()!;

        if (string.IsNullOrEmpty(response)){

          break;

        }

        list.Add(response);

      } while (true);

      prop.SetValue(character, list);

    }
  }
}