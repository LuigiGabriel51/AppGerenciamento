
using App_Gerenciamento.Telas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace App_Gerenciamento.rest_services
{
    public class RestService : IRestService
    {
        const string Url = "https://00df-2804-5e7c-f831-8d00-2c32-edfe-8d2b-fd3d.ngrok-free.app";
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<bool> SaveLogin(string CPF, string SENHA)
        {
            const string url = Url + "/login";
            Uri uri = new Uri(string.Format(url, string.Empty));

            try
            {
                modeloLogin pessoa = new modeloLogin
                {
                    cpf = CPF,
                    senha = SENHA
                };
                string json = JsonConvert.SerializeObject(pessoa);
                StringContent dadosLogin = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {
                    response = await _client.PostAsync(uri, dadosLogin);
                    Debug.WriteLine(@"\t Requisição bem sucedida");
                }
                catch (Exception) { Debug.WriteLine(@"\t Requisição falhou."); }

                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
                {
                    Debug.WriteLine(@"\tLogin feito");
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JsonDocument jsonDocument = JsonDocument.Parse(jsonResponse);
                    JsonElement root = jsonDocument.RootElement;
                    Login.AcessToken = root.GetProperty("AccessToken").GetString();
                    Login.UserID = root.GetProperty("id").GetInt32();
                    Login.Nome = root.GetProperty("Nome").GetString();
                    Login.Cpf = root.GetProperty("Cpf").GetString();
                    Login.Email = root.GetProperty("Email").GetString();
                    Login.DataN = root.GetProperty("Data").GetString();
                    Login.Numero = root.GetProperty("Numero").GetString();
                    Login.Sexo = root.GetProperty("Sexo").GetString();
                    Login.Cargo = root.GetProperty("Cargo").GetString();

                    Debug.WriteLine(@"\" + Login.AcessToken);
                    return true;
                }


                }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;

        }
        public async Task<string> SaveContato(string cpf)
        {
            const string url = Url + "/pega_email_sms";
            Uri uri = new Uri(string.Format(url, string.Empty));

            try
            {
                modeloLogin pessoa = new modeloLogin
                {
                    cpf = cpf,
                };
                string jsonString = JsonConvert.SerializeObject(pessoa);
                StringContent dadosLogin = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {

                    response = await _client.PostAsync(uri, dadosLogin);
                    Debug.WriteLine(@"\t Requisição bem sucedida"+jsonString);
                }
                catch (Exception)
                {
                    Debug.WriteLine(@"\t Requisição falhou.");
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\t email ou telefone recebido");
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JsonDocument jsonDocument = JsonDocument.Parse(jsonResponse);
                    JsonElement root = jsonDocument.RootElement;
                    InfoContato.email = root.GetProperty("email").GetString();
                    InfoContato.telefone = root.GetProperty("telefone").GetString();
                    return "200";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<bool> SendMsgEmail(string email, string codigo)
        {
            const string url = Url + "/envia_email";
            Uri uri = new Uri(string.Format(url, string.Empty));

            try
            {
                modeloSendemail dados = new modeloSendemail
                {
                    email = email,
                    codigo = codigo
                };

                string json = JsonConvert.SerializeObject(dados);
                StringContent dadosLogin = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {
                    response = await _client.PostAsync(uri, dadosLogin);
                    Debug.WriteLine(@"\t Requisição bem sucedida");
                }
                catch (Exception) { Debug.WriteLine(@"\t Requisição falhou."); }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tcodigo enviado");
                    
                    return true;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;

        }

        public async Task<bool> SendMsgTel(string telefone1, string codigo)
        {
            const string url = Url + "/envia_sms";
            Uri uri = new Uri(string.Format(url, string.Empty));

            try
            {
                modeloSendtel dados = new modeloSendtel
                {
                    telefone = telefone1,
                    codigo = codigo
                };

                string json = JsonConvert.SerializeObject(dados);
                StringContent dadosLogin = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {
                    response = await _client.PostAsync(uri, dadosLogin);
                    Debug.WriteLine(@"\t Requisição bem sucedida");
                }
                catch (Exception) { Debug.WriteLine(@"\t Requisição falhou."); }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tcodigo enviado");

                    return true;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;

        }


        public async Task<bool> AlterSenha(string cpf, string SENHA)
        {
            const string url = Url + "/NovaSenha";
            Uri uri = new Uri(string.Format(url, string.Empty));

            try
            {
                modeloLogin pessoa = new modeloLogin
                {
                    cpf =  cpf,
                    senha = SENHA
                };
                string json = JsonConvert.SerializeObject(pessoa);
                StringContent dadosLogin = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {
                    response = await _client.PostAsync(uri, dadosLogin);
                    Debug.WriteLine(@"\t Requisição bem sucedida");
                }
                catch (Exception) { Debug.WriteLine(@"\t Requisição falhou."); }

                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
                {
                    Debug.WriteLine(@"\tSenha Alterada com Sucesso");
                    return true;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;

        }


        public async Task<List<Servicos>> RequestAgenda(string data)
        {
            const string url = Url + "/Agenda";
            Uri uri = new Uri(string.Format(url, string.Empty));
            Debug.WriteLine("asxsasxsxasxsax        "+data);
            try
            {
                modeloLogin pessoa = new modeloLogin
                {
                    dia = data,
                };
                string jsonString = JsonConvert.SerializeObject(pessoa);
                StringContent dadosAgenda = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {

                    response = await _client.PostAsync(uri, dadosAgenda);
                    Debug.WriteLine(@"\t Requisição bem sucedida");
                }
                catch (Exception)
                {
                    Debug.WriteLine(@"\t Requisição falhou.");
                }

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (!responseString.StartsWith("["))
                    {
                        responseString = "[" + responseString + "]";
                    }
                    var servicos = JsonConvert.DeserializeObject<List<Servicos>>(responseString);
                    Console.WriteLine(@"\t agenda recebida com sucesso: " + servicos[0].Nome + servicos[0].dia + servicos[0].descricao);
                    return servicos;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return new List<Servicos>();
        }


        public async Task<List<Projetos>> requestProjetos()
        {
            using HttpClient client = new HttpClient();
            string url = Url + "/listaDeServicos";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                jsonString = jsonString.ToString();


                Console.WriteLine(" " + jsonString.ToString());
                var projetosList = new List<Projetos>();

                var jsonObject = JObject.Parse(jsonString);

                foreach (var projetoProperty in jsonObject.Properties())
                {
                    var projeto = new Projetos();
                    projeto.Nome = projetoProperty.Name;
                    projeto.fases = new List<Fases>();

                    var fasesArray = projetoProperty.Value as JArray;

                    foreach (var faseArray in fasesArray)
                    {
                        var fase = new Fases();
                        fase.id = faseArray[0].ToObject<int>();
                        fase.pertencente = faseArray[1].ToObject<int>();
                        fase.nome = faseArray[2].ToString();
                        fase.descricao = faseArray[3].ToString();
                        fase.data = faseArray[4].ToString();
                        fase.duracao = faseArray[5].ToObject<int>();

                        projeto.fases.Add(fase);
                    }

                    projetosList.Add(projeto);
                }

                Console.WriteLine("***************************requisicao feita com exito************************ ");
                return projetosList;
            }
            else
            {
                Console.WriteLine("requisicao feita sem exito \n");
                return null;
            }
        }
        public async Task<bool> UpdateData(string CPF, string EMAIL, string TELEFONE, string SEXO, string CARGO)
        {
            const string url = Url + "/editData";
            Uri uri = new Uri(string.Format(url, string.Empty));

            try
            {
                modeloDAta pessoa = new modeloDAta
                {
                    cpf = CPF,
                    email = EMAIL,
                    telefone = TELEFONE,
                    sexo = SEXO,
                    cargo = CARGO
                };
                string json = JsonConvert.SerializeObject(pessoa);
                StringContent dadosLogin = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {
                    response = await _client.PostAsync(uri, dadosLogin);
                    Debug.WriteLine(@"\t Requisição bem sucedida");
                }
                catch (Exception) { Debug.WriteLine(@"\t Requisição falhou."); }

                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
                { 
                    return true;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;

        }

        public async Task<string> SuporteTecnico(MensagemSuporte sup)
        {
            const string url = Url + "/suporteForms";
            Uri uri = new Uri(string.Format(url, string.Empty));

            try
            {

                string jsonString = JsonConvert.SerializeObject(sup);
                StringContent dadosLogin = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                try
                {

                    response = await _client.PostAsync(uri, dadosLogin);
                    Debug.WriteLine(@"\t Requisição bem sucedida" + jsonString);
                }
                catch (Exception)
                {
                    Debug.WriteLine(@"\t Requisição falhou.");
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\t email enviado");
                    return "200";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }


        public async Task<List<Msg>> ChatpGet()
        {
            const string url = Url + "/chat";
            Uri uri = new Uri(string.Format(url, string.Empty));

            HttpResponseMessage response = null;
            response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                List<List<Msg>> messages = JsonConvert.DeserializeObject<List<List<Msg>>>(result);

                List<Msg> allMessages = new List<Msg>();
                foreach (var messageList in messages)
                {
                    allMessages.AddRange(messageList);
                }

                return allMessages;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Msg>> ChatPost(string user, string message)
        {
            const string url = Url + "/chat";
            Uri uri = new Uri(string.Format(url, string.Empty));

            
            Msg data = new Msg
            {
                Nome = user,
                Msgs = message
            };

            string jsonString = JsonConvert.SerializeObject(data);
            StringContent data2 = new StringContent(jsonString, Encoding.UTF8, "application/json");


            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, data2);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                List<List<Msg>> messages = JsonConvert.DeserializeObject<List<List<Msg>>>(result);

                List<Msg> allMessages = new List<Msg>();
                foreach (var messageList in messages)
                {
                    allMessages.AddRange(messageList);
                }

                return allMessages;
            }
            else
            {
                return null;
            }
        }


    }
}
    public interface IRestService
    {
    }

public class modeloLogin
{
    public int id { get; set; } 
    public string email { get; set; }
    public string cpf { get; set; }
    public string senha { get; set; }
    public string dia { get; set; }
}

public class modeloDAta
{
    public string email { get; set; }
    public string cpf { get; set; }
    public string nome { get; set; }
    public string telefone { get; set; }
    public string sexo { get; set; }
    public string cargo { get; set; }
}
public class modeloSendemail
{
    public string email { get; set; }
    public string codigo { get; set; }
}
public class modeloSendtel
{
    public string telefone { get; set; }
    public string codigo { get; set; }
}


public class Msg
{
    public int id { get; set; } 
    public string Nome { get; set; }
    public string Msgs { get; set; }
    public string Horario { get; set; }
}