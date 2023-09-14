using System;
using System.IO;

// See https://aka.ms/new-console-template for more informationusing System;
namespace SimpleFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello, World!");
            var caminho = System.AppDomain.CurrentDomain.BaseDirectory + "ftp.env";
            LoadEnvironmentVariables(caminho);

            string host = Environment.GetEnvironmentVariable("HOST") ?? "";
            string portString = Environment.GetEnvironmentVariable("PORT") ?? "";
            int port = 22;
            if (!string.IsNullOrEmpty(portString))
            {
                int.TryParse(portString, out port);
            }
            string username = Environment.GetEnvironmentVariable("USERNAME") ?? "";
            string password = Environment.GetEnvironmentVariable("PASSWORD") ?? "";

            var ftpConnection = new FTPConnection(host, port, username, password);
            ftpConnection.Connect();
            ftpConnection.SendFile();
        }

        private static void LoadEnvironmentVariables(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Arquivo de configuração não encontrado");
            }

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(
                        '=',
                        StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                        continue;

                    Environment.SetEnvironmentVariable(parts[0], parts[1]);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar arquivo de configuração", e);
            }
        }
    }
}