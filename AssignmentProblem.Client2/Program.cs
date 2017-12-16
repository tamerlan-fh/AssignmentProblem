using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AssignmentProblem.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = string.Empty;
            PrintCommands();

            var client = new AgentClient();

            while(true)
            {
                command = Console.ReadLine().ToLower();

                if(command.StartsWith("help"))
                {
                    PrintCommands();
                    continue;
                }

                if(command.StartsWith("disconnect"))
                {
                    client.Disconnect();
                    continue;
                }

                if(command.StartsWith("exit"))
                {
                    client.Dispose();
                    break;
                }

                if(command.StartsWith("connect"))
                {
                    var reg = new Regex(@"(?<host>[^:\s]+)[\s]*:[\s]*(?<port>\d+)", RegexOptions.Compiled | RegexOptions.Singleline);

                    var match = reg.Match(command);
                    if(!match.Success
                        || string.IsNullOrEmpty(match.Groups["host"].Value)
                        || !int.TryParse(match.Groups["port"].Value, out int port))
                    {
                        Console.WriteLine("не верный формат адреса");
                        continue;
                    }

                    var host = match.Groups["token"].Value;

                    if(client.Connect(host, port))
                        client.WaitOperations();

                    continue;
                }

                Console.WriteLine("неизвестная команда");
            }
        }

        static void PrintCommands()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("доступные команды:");
            sb.AppendLine(string.Format("{0, 17} - вызыв подсказки", "help"));
            sb.AppendLine(string.Format("{0, 17} - подключение агента к серверу по указанному адресу", "connect host:port"));
            sb.AppendLine(string.Format("{0, 17} - разорвать имеющееся соединение", "disconnect"));
            sb.AppendLine(string.Format("{0, 17} - выход", "exit"));
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }

        public static void AddMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} : {message}");
        }
    }
}
