using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Gerenciamento.rest_services
{
    public class TokenManager
    {
        private readonly string filePath;

        public TokenManager()
        {
            string modelFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models");

            // Certifica-se de que o diretório exista
            Directory.CreateDirectory(modelFolderPath);

            this.filePath = Path.Combine(modelFolderPath, "jsonToken.json");
            
        }

        public void AddToken(string token)
        {
            List<string> tokens = GetTokens();

            if (!tokens.Contains(token))
            {
                tokens.Add(token);
                SaveTokens(tokens);
                Console.WriteLine("Token adicionado com sucesso.");
            }
            else
            {
                Console.WriteLine("Token já existe na lista.");
            }
        }

        public void RemoveToken(string token)
        {
            List<string> tokens = GetTokens();

            if (tokens.Contains(token))
            {
                tokens.Remove(token);
                SaveTokens(tokens);
                Console.WriteLine("Token removido com sucesso.");
            }
            else
            {
                Console.WriteLine("Token não encontrado na lista.");
            }
        }

        public List<string> GetTokens()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<string> tokens = JsonConvert.DeserializeObject<List<string>>(json);
                return tokens ?? new List<string>();
            }
            else
            {
                return new List<string>();
            }
        }

        private void SaveTokens(List<string> tokens)
        {
            string json = JsonConvert.SerializeObject(tokens);
            File.WriteAllText(filePath, json);
        }
    }
}