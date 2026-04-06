using System.Security.Cryptography;
using System.Text;

namespace GerenciamentoPatrimonio.Applications.Autenticacao
{
    public class CriptografiaUsuario
    {
        public static byte[] CriptografarSenha(string senha)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bystesSenha = Encoding.UTF8.GetBytes(senha);
            byte[] senhaCriptografada = sha256.ComputeHash(bystesSenha);

            return senhaCriptografada;
        }
    }
}
