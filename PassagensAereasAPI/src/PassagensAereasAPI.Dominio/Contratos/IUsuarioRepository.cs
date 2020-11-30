using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Contratos
{
    public interface IUsuarioRepository
    {
        Usuario SalvarUsuario(Usuario usuario);
        Usuario ObterUsuarioPorLogin(string email, string senha);
        Usuario Obter(int id);
        Usuario AtualizarUsuario(int id, Usuario usuario);
        Usuario DeletarUsuario(int id);
        List<Usuario> ListarUsuarios();
    }
}