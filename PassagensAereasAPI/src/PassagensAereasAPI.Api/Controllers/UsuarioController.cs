using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PassagensAereasAPI.Api.Modelos;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;
using PassagensAereasAPI.Dominio.Servicos;
using PassagensAereasAPI.Infra;


namespace PassagensAereasAPI.Api.Controllers
{

    [Authorize, Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository;

        private UsuarioService usuarioService;

        private IOptions<SecuritySettings> settings;

        private PassagensAereasContext contexto;

        public UsuarioController(IUsuarioRepository usuarioRepository, UsuarioService usuarioService, PassagensAereasContext contexto, IOptions<SecuritySettings> settings)
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioService = usuarioService;
            this.contexto = contexto;
            this.settings = settings;
        }

        [Authorize(Roles = "Admin"), HttpGet]
        public IActionResult Get()
        {
            var usuariosResponse = usuarioRepository.ListarUsuarios()
            .Select(u => MapearDominioParaResponse(u)).ToList();

            return Ok(usuariosResponse);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult Get(int id)
        {
            var usuario = usuarioRepository.Obter(id);

            if (usuario == null) return NotFound("Usuário não encontrado");
            var usuarioResponse = MapearDominioParaResponse(usuario);
            return Ok(usuarioResponse);
        }


        // POST api/values
        [AllowAnonymous, HttpPost]
        public IActionResult Post([FromBody]UsuarioDto usuarioRequest)
        {
            var usuarioJaCadastrado = contexto.Usuarios.FirstOrDefault(u => u.Email == usuarioRequest.Email);

            if (usuarioJaCadastrado != null)
                return BadRequest("Esse email já possui um cadastro.");

            var usuario = MapearDtoParaDominio(usuarioRequest);
            var mensagens = usuarioService.Validar(usuario);

            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.SalvarUsuario(usuario);
            contexto.SaveChanges();

            var usuarioResponse = MapearDominioParaResponse(usuario);
            return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuarioResponse);
        }

        [AllowAnonymous, HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto dadosLogin)
        {
            var usuario = usuarioRepository.ObterUsuarioPorLogin(dadosLogin.Email, dadosLogin.Senha);

            if (usuario == null) return BadRequest("Usuario ou senha inválidos");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.SigningKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] {
                    new Claim(ClaimTypes.Name, $"{usuario.PrimeiroNome} {usuario.UltimoNome}"),
                    new Claim(ClaimTypes.Role, usuario.Admin? "Admin" : "Usuario Comum"),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                idUsuario = (int)usuario.Id,
                role = usuario.Admin ? "Admin" : "Usuario Comum"
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var mensagens = usuarioService.Validar(usuario);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.AtualizarUsuario(id, usuario);
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            usuarioRepository.DeletarUsuario(id);
            contexto.SaveChanges();
            return Ok();
        }

        private Usuario MapearDtoParaDominio(UsuarioDto usuarioDto)
        {
            return new Usuario(
                usuarioDto.PrimeiroNome,
                usuarioDto.UltimoNome,
                usuarioDto.Cpf,
                usuarioDto.DataDeNascimento,
                usuarioDto.Email,
                usuarioDto.Senha
            );
        }
        private UsuarioResponseDto MapearDominioParaResponse(Usuario usuario)
        {
            var usuarioResponse = new UsuarioResponseDto();
            usuarioResponse.PrimeiroNome = usuario.PrimeiroNome;
            usuarioResponse.UltimoNome = usuario.UltimoNome;
            usuarioResponse.Cpf = usuario.Cpf;
            usuarioResponse.DataDeNascimento = usuario.DataDeNascimento;
            usuarioResponse.Email = usuario.Email;
            usuarioResponse.Id = usuario.Id;

            return usuarioResponse;
        }
    }
}