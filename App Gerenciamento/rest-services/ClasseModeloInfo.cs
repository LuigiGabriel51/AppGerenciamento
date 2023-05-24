using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Gerenciamento.rest_services
{

    public class Login{
        public static int UserID {  get; set; }
        public static string AcessToken { get; set; }
        public static string Nome { get; set; }
        public static string Cpf { get; set; }
        public static string Email { get; set; }
        public static string DataN { get; set; }
        public static string Numero { get; set; }
        public static string Sexo { get; set; }
        public static string Cargo { get; set; }
    }
    public class InfoContato
    {
        public static string email { get; set; }
        public static string telefone { get; set; }
    }

    public class Servicos
    {
        public string Nome { get; set ; }
        public string dia { get; set; }
        public string horario { get; set; }
        public string descricao { get; set; }

    }
    public class Projetos
    {
        public string Nome { get; set; }
        public List<Fases> fases { get; set; }

    }
    public class Fases {
        public int id { get; set; }
        public int pertencente { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string data { get; set; }
        public int duracao { get; set; }
    }
}
