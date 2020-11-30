
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Api.Modelos;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;
using PassagensAereasAPI.Dominio.Servicos;
using PassagensAereasAPI.Infra;

namespace PassagensAereasAPI.Api.Controllers
{
    [Authorize, Route("api/usuario")]
    public class ReservasController : Controller
    {
        private IReservaRepository reservaRepository;

        private ReservaService reservaService;

        private PassagensAereasContext contexto;

        public ReservasController(IReservaRepository reservaRepository, ReservaService reservaService, PassagensAereasContext contexto)
        {
            this.reservaRepository = reservaRepository;
            this.reservaService = reservaService;
            this.contexto = contexto;
        }

        // GET api/values
        [HttpGet("{idUsuario}/reserva")]
        public IActionResult GetLista(int idUsuario)
        {
            var usuario = contexto.Usuarios.AsNoTracking().FirstOrDefault(u => u.Id == idUsuario);
            if (usuario == null) return NotFound("Usuário não encontrado");

            var listaReservas = reservaRepository.ListarReservas(idUsuario);
            if (listaReservas == null) return NotFound("Não existem reservas deste usuário");

            return Ok(reservaRepository.ListarReservas(idUsuario));
        }

        // GET api/values/5
        [HttpGet("reserva/{id}", Name = "GetReserva")]
        public IActionResult Get(int id)
        {
            var reserva = reservaRepository.Obter(id);

            if (reserva == null) return NotFound("Reserva não encontrada");

            return Ok(reserva);
        }

        [HttpPost("reserva/valor")]

        public IActionResult GetValor([FromBody]ReservaDto reservaRequest)
        {
            var reserva = MapearDtoParaDominio(reservaRequest);

            return Ok(reserva.Valor);
        }

        // POST api/values
        [HttpPost("{idUsuario}/reserva")]
        public IActionResult Post(int idUsuario, [FromBody]ReservaDto reservaRequest)
        {
            var usuario = contexto.Usuarios.Include(u => u.Reservas).AsNoTracking().FirstOrDefault(u => u.Id == idUsuario);
            if (usuario == null) return NotFound("Usuário não encontrado");

            var reserva = MapearDtoParaDominio(reservaRequest);

            var mensagens = reservaService.Validar(usuario, reserva);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            reservaRepository.SalvarReserva(idUsuario, reserva);
            contexto.SaveChanges();
            return CreatedAtRoute("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE api/values/5
        [HttpDelete("{idUsuario}/reserva/{id}")]
        public IActionResult Delete(int idUsuario, int id)
        {
            var usuario = contexto.Usuarios.AsNoTracking().FirstOrDefault(u => u.Id == idUsuario);
            if (usuario == null) return NotFound("Usuário não encontrado");

            reservaRepository.DeletarReserva(id);
            contexto.SaveChanges();
            return Ok();
        }

        private Reserva MapearDtoParaDominio(ReservaDto reservaDto)
        {
            var trecho = contexto.Trechos.Include(t => t.LocalOrigem).Include(t => t.LocalDestino).FirstOrDefault(t => t.Id == reservaDto.IdTrecho);
            var classeDeVoo = contexto.ClassesDeVoo.FirstOrDefault(t => t.Id == reservaDto.IdClasseDeVoo);

            var opcionais = contexto.Opcionais
            .Where(o => reservaDto.IdsOpcionais.Contains(o.Id))
            .Select(o => o).ToList();

            var reservaOpcional = opcionais.Select(o => new ReservaOpcional(o)).ToList();

            return new Reserva(trecho, classeDeVoo, reservaOpcional);
        }
    }
}