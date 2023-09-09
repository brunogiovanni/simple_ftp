using System;
using System.IO;
using Renci.SshNet;

namespace ArarajubaFTP
{
    class FTPConnection
    {
        private readonly SftpClient client;

        public FTPConnection(string host, int port, string username, string password)
        {
            this.client = this.client = new SftpClient(host, port, username, password);;
        }

        public void Connect()
        {
            try
            {
                this.client.Connect();
                Console.WriteLine("Conectado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar! Confira as informações de conexão no arquivo de configuração", ex);
            }
        }

        public void SendFile()
        {
            if (this.client == null)
            {
                throw new Exception("Erro ao enviar arquivo! Cliente não inicializado");
            }
            
            Console.WriteLine("Iniciando envio do arquivo...");
            
            // Obtem as variáveis de ambiente para realizar o envio do arquivo.
            // Caso não existam, retorna uma string vazia.
            // Caso existam, mas não sejam preenchidas, retorna uma string vazia.
            // Caso existam e sejam preenchidas, retorna a string preenchida.
            
            // Exemplo:
            // LOCAL_PATH=C:\Users\usuario\Documents\arquivo.txt
            // SERVER_PATH=/home/usuario/arquivo.txt
            // FILENAME=arquivo.txt
            
            // Para testar, utilize as variáveis de ambiente abaixo:
            // LOCAL_PATH=C:\Users\usuario\Documents\arquivo.txt
            // SERVER_PATH=/home/usuario/arquivo
            try
            {
                var localPath = Environment.GetEnvironmentVariable("LOCAL_PATH") ?? "";
                var remotePath = Environment.GetEnvironmentVariable("SERVER_PATH") ?? "";
                var filename = Environment.GetEnvironmentVariable("FILENAME") ?? "";

                using (var fileStream = new FileStream(localPath + filename, FileMode.Open))
                {
                    this.client.UploadFile(fileStream, remotePath + filename);
                    Console.WriteLine("Arquivo enviado com sucesso!");
                }
                this.client.Disconnect();
                Console.WriteLine("Desconectado com sucesso!");
                this.client.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar arquivo! Confira as informaçães de conexão no arquivo de configuração", ex);
            }
        }
    }
}