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
    public class LocalController : Controller
    {
        private ILocalRepository localRepository;

        private LocalService localService;

        private PassagensAereasContext contexto;

        public LocalController(ILocalRepository localRepository, LocalService localService, PassagensAereasContext contexto)
        {
            this.localRepository = localRepository;
            this.localService = localService;
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(localRepository.ListarLocais());
        }

        // GET api/values/5
        [Authorize(Roles = "Admin"), HttpGet("{id}", Name = "GetLocal")]
        public IActionResult Get(int id)
        {
            var local = localRepository.Obter(id);

            if (local == null) return NotFound("Local não encontrado");

            return Ok(local);
        }


        // POST api/values
        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Post([FromBody]LocalDto localRequest)
        {
            var local = MapearDtoParaDominio(localRequest);
            var mensagens = localService.Validar(local);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            localRepository.SalvarLocal(local);
            contexto.SaveChanges();
            return CreatedAtRoute("GetLocal", new { id = local.Id }, local);
        }

        // PUT api/values/5
        [Authorize(Roles = "Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]LocalDto localRequest)
        {
            var local = MapearDtoParaDominio(localRequest);
            var mensagens = localService.Validar(local);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            localRepository.AtualizarLocal(id, local);
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            localRepository.DeletarLocal(id);
            contexto.SaveChanges();
            return Ok();
        }

        private Local MapearDtoParaDominio(LocalDto localDto)
        {
            return new Local(localDto.Latitude, localDto.Longitude, localDto.Nome);
        }
    }
}