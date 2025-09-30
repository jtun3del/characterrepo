using NLog;
using System.Data.Common;
using System.Reflection;
using System.Text.Json;
String path = Directory.GetCurrentDirectory() + "//nlog.config";


//logger instance
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("program started");
//grab user file name
Console.WriteLine("1: mario \n 2: sf2 \n dk");
string filename = "";
string num = Console.ReadLine();
Character placeholder = new Character();
switch (num)
{
    case "1":
        filename = "mario.json";
        Character tsar = new Mario();
        placeholder = tsar;
        break;
    case "2":
        filename = "sf2.json";
        Character tsad = new sf2();
        placeholder = tsad;
        break;
    case "3":
        filename = "dk.json";
        Character tmad = new dk();
        placeholder = tmad;
        break;
    default:
        logger.Error("unknown number");
        break;
}

{
    
}
List<Character> chars = [];

// for the character add thing later
if (true)
{
    Character tsar = new Mario();
}

// check if file exists
if (File.Exists(filename))
{
    //Character deserialization
    chars = JsonSerializer.Deserialize<List<Character>>(File.ReadAllText(filename))!;
    logger.Info($"File deserialized {filename}");
}


do
{
    // display choices to user
    Console.WriteLine("1) Display Character Characters");
    Console.WriteLine("2) Add Character Character");
    Console.WriteLine("3) Remove Character Character");
    Console.WriteLine("Enter to quit");
    // input selection
    string? choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);
    if (choice == "1")
    {
        // Display Character Characters
        foreach (var i in chars)
        {
            Console.WriteLine(i.Display());
        }
    }
    else if (choice == "2")
    {
        // Add Character 
        //id making
       
        placeholder.Id = chars.Count == 0 ? 1 : chars.Max(c => c.Id) + 1;
        

        InputCharacter(placeholder);
        //add the char
        chars.Add(placeholder);
        File.WriteAllText(filename, JsonSerializer.Serialize(chars));
        logger.Info($"character created: {placeholder.Name}");

    }
    else if (choice == "3")
    {
        // Remove Character Character
        Console.WriteLine("Enter the Id of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
        {
            Character? character = chars.FirstOrDefault(c => c.Id == Id);
            if (character == null)
            {
                logger.Error($"Character Id {Id} not found");
            }
            else
            {
                chars.Remove(character);
                // serialize list<CharacterCharacter> into json file
                File.WriteAllText(filename, JsonSerializer.Serialize(chars));
                logger.Info($"Character Id {Id} removed");
            }
        }
        else
        {
            logger.Error("Invalid Id");
        }
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