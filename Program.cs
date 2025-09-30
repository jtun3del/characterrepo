using NLog;
using System.Text.Json;
String path = Directory.GetCurrentDirectory() + "//nlog.config";


//logger instance
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("program started");

//mario deserialization
string marioFileName = "mario.json";
List<Mario> marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName));


logger.Info("program ended");