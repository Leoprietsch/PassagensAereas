using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private PassagensAereasContext contexto;

        public UsuarioRepository(PassagensAereasContext contexto)
        {
            this.contexto = contexto;
        }

        public Usuario SalvarUsuario(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            return usuario;
        }

        public Usuario ObterUsuarioPorLogin(string email, string senha)
        {
            var senhaCriptografada = CriptografarSenha(senha);

            return contexto.Usuarios.AsNoTracking()
                .FirstOrDefault(u => u.Email == email && u.Senha == senhaCriptografada);
        }

        public Usuario Obter(int id)
        {
            return contexto.Usuarios.Include(u => u.Reservas).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public Usuario AtualizarUsuario(int id, Usuario usuario)
        {
            var usuarioCadastrado = contexto.Usuarios.Include(a => a.Reservas).FirstOrDefault(a => a.Id == id);

            if (usuarioCadastrado != null)
                usuarioCadastrado.Atualizar(usuario);

            return usuarioCadastrado;
        }

        public Usuario DeletarUsuario(int id)
        {
            var usuarioCadastrado = contexto.Usuarios.Include(a => a.Reservas).FirstOrDefault(a => a.Id == id);

            if (usuarioCadastrado != null)
                contexto.Usuarios.Remove(usuarioCadastrado);

            return usuarioCadastrado;
        }

        public List<Usuario> ListarUsuarios()
        {
            return contexto.Usuarios.Include(a => a.Reservas).AsNoTracking().ToList();
        }

        private string CriptografarSenha(string senha)
        {
            var inputBytes = Encoding.UTF8.GetBytes(senha);

            var hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}