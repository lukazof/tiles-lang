using System;
using System.IO;
using System.Xml.Schema;

public class Program
{
    private static string version = "1.0.0";

    private static Tokenizer tokenizer;
    private static Parser parser;
    private static Compiler compiler;
    

    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            switch (args[0])
            { 
                case "new":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Wrong usage! Use: \n tiles new <package_name>");
                        return;
                    }
                    CreateNewPackage(args[1]);
                    break;
                case "build":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Wrong usage! Use: \n tiles build <path>");
                        return;
                    }
                    Build(args[1]);
                    break;
                case "test":
                    Console.WriteLine(BuildFromSrc("//teste\n exit(0);"));
                    break;
                case "help":
                    ShowHelp();
                    break;
                case "version":
                    ShowVersion();
                    break;
                default:
                    Console.WriteLine("Unknown command. Available commands: new, help, version");
                    break;
            }
        }
        else
        {
            Console.WriteLine("No command specified. Available commands: new, help, version");
        }
    }

    private static string BuildFromSrc(string src) {

        tokenizer = new Tokenizer(src);
        var tokens = tokenizer.Tokenize();

        parser = new Parser(tokens);
        var program = parser.Parse();

        compiler = new Compiler(program);
        var asm = compiler.GenerateAssembly();

        return asm;
    }

    private static void Build(string path) {

        var src = File.ReadAllText(path);

        BuildFromSrc(src);

    }

    private static void CreateNewPackage(string packageName)
    {
        try
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), packageName);
            Directory.CreateDirectory(directoryPath);

            // Creating package.tlconfig file
            string configFilePath = Path.Combine(directoryPath, "package.tlconfig");
            using (StreamWriter writer = File.CreateText(configFilePath))
            {
                writer.WriteLine("#[package]");
                writer.WriteLine($"name: \"{packageName}\"");
                writer.WriteLine("version: \"0.1.0\"");
            }

            // Creating src directory
            string srcDirectoryPath = Path.Combine(directoryPath, "src");
            Directory.CreateDirectory(srcDirectoryPath);

            // Creating main.tl file inside src directory
            string mainFilePath = Path.Combine(srcDirectoryPath, "main.tl");
            using (StreamWriter writer = File.CreateText(mainFilePath))
            {
                writer.WriteLine("func main() {");
                writer.WriteLine("    print(\"Hello, World!\")");
                writer.WriteLine("}");
            }

            Console.WriteLine($"New package \"{packageName}\" created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }

    private static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  new <package_name> : Creates a new package");
        Console.WriteLine("  help                : Shows this help message");
        Console.WriteLine("  version             : Shows the version of the program");
    }

    private static void ShowVersion()
    {
        Console.WriteLine($"Version: {version}");
    }
}
