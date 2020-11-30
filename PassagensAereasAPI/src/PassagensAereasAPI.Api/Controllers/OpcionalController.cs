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
    public class OpcionalController : Controller
    {
        private IOpcionalRepository opcionalRepository;

        private OpcionalService opcionalService;

        private PassagensAereasContext contexto;

        public OpcionalController(IOpcionalRepository opcionalRepository, OpcionalService opcionalService, PassagensAereasContext contexto)
        {
            this.opcionalRepository = opcionalRepository;
            this.opcionalService = opcionalService;
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(opcionalRepository.ListarOpcionals());
        }

        // GET api/values/5
        [Authorize(Roles = "Admin"), HttpGet("{id}", Name = "GetOpcional")]
        public IActionResult Get(int id)
        {
            var opcional = opcionalRepository.Obter(id);

            if (opcional == null) return NotFound("Opcional não encontrado");

            return Ok(opcional);
        }


        // POST api/values
        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Post([FromBody]OpcionalDto opcionalRequest)
        {
            var opcional = MapearDtoParaDominio(opcionalRequest);
            var mensagens = opcionalService.Validar(opcional);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            opcionalRepository.SalvarOpcional(opcional);
            contexto.SaveChanges();
            return CreatedAtRoute("GetOpcional", new { id = opcional.Id }, opcional);
        }

        // PUT api/values/5
        [Authorize(Roles = "Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]OpcionalDto opcionalRequest)
        {
            var opcional = MapearDtoParaDominio(opcionalRequest);
            var mensagens = opcionalService.Validar(opcional);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            opcionalRepository.AtualizarOpcional(id, opcional);
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            opcionalRepository.DeletarOpcional(id);
            contexto.SaveChanges();
            return Ok();
        }

        private Opcional MapearDtoParaDominio(OpcionalDto opcionalDto)
        {
            return new Opcional(opcionalDto.Nome, opcionalDto.Descricao, opcionalDto.Valor);
        }
    }
}