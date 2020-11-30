using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassagensAereasAPI.Api.Modelos;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;
using PassagensAereasAPI.Dominio.Servicos;
using PassagensAereasAPI.Infra;

namespace PassagensAereasAPI.Api.Controllers
{

    [Authorize, Route("api/[controller]")]
    public class TrechoController : Controller
    {
        private ITrechoRepository trechoRepository;

        private TrechoService trechoService;

        private PassagensAereasContext contexto;

        public TrechoController(ITrechoRepository trechoRepository, TrechoService trechoService, PassagensAereasContext contexto)
        {
            this.trechoRepository = trechoRepository;
            this.trechoService = trechoService;
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(trechoRepository.ListarTrechos());
        }

        // GET api/values/5
        [Authorize(Roles = "Admin"), HttpGet("{id}", Name = "GetTrecho")]
        public IActionResult Get(int id)
        {
            var trecho = trechoRepository.Obter(id);

            if (trecho == null) return NotFound("Trecho não encontrado");

            return Ok(trecho);
        }

        // POST api/values
        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Post([FromBody]TrechoDto trechoRequest)
        {
            var trecho = MapearDtoParaDominio(trechoRequest);

            var mensagens = trechoService.Validar(trecho);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            trechoRepository.SalvarTrecho(trecho);
            contexto.SaveChanges();
            return CreatedAtRoute("GetTrecho", new { id = trecho.Id }, trecho);
        }

        // PUT api/values/5
        [Authorize(Roles = "Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TrechoDto trechoRequest)
        {
            var trecho = MapearDtoParaDominio(trechoRequest);
            var mensagens = trechoService.Validar(trecho);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            trechoRepository.AtualizarTrecho(id, trecho);
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            trechoRepository.DeletarTrecho(id);
            contexto.SaveChanges();
            return Ok();
        }

        private Trecho MapearDtoParaDominio(TrechoDto trechoDto)
        {
            var localOrigem = contexto.Locais.FirstOrDefault(t => t.Id == trechoDto.IdLocalOrigem);
            var localDestino = contexto.Locais.FirstOrDefault(t => t.Id == trechoDto.IdLocalDestino);

            return new Trecho(localOrigem, localDestino);
        }
    }
}