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
    public class ClasseDeVooController : Controller
    {
        private IClasseDeVooRepository classeDeVooRepository;

        private ClasseDeVooService classeDeVooService;

        private PassagensAereasContext contexto;

        public ClasseDeVooController(IClasseDeVooRepository classeDeVooRepository, ClasseDeVooService classeDeVooService, PassagensAereasContext contexto)
        {
            this.classeDeVooRepository = classeDeVooRepository;
            this.classeDeVooService = classeDeVooService;
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(classeDeVooRepository.ListarClassesDeVoo());
        }

        // GET api/values/5
        [Authorize(Roles = "Admin"), HttpGet("{id}", Name = "GetClasseDeVoo")]
        public IActionResult Get(int id)
        {
            var classeDeVoo = classeDeVooRepository.Obter(id);

            if (classeDeVoo == null) return NotFound("Classe De Voo não encontrada");

            return Ok(classeDeVoo);
        }


        // POST api/values
        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Post([FromBody]ClasseDeVooDto classeDeVooRequest)
        {
            var classeDeVoo = MapearDtoParaDominio(classeDeVooRequest);
            var mensagens = classeDeVooService.Validar(classeDeVoo);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            classeDeVooRepository.SalvarClasseDeVoo(classeDeVoo);
            contexto.SaveChanges();
            return CreatedAtRoute("GetClasseDeVoo", new { id = classeDeVoo.Id }, classeDeVoo);
        }

        // PUT api/values/5
        [Authorize(Roles = "Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ClasseDeVooDto classeDeVooRequest)
        {
            var classeDeVoo = MapearDtoParaDominio(classeDeVooRequest);
            var mensagens = classeDeVooService.Validar(classeDeVoo);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            classeDeVooRepository.AtualizarClasseDeVoo(id, classeDeVoo);
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            classeDeVooRepository.DeletarClasseDeVoo(id);
            contexto.SaveChanges();
            return Ok();
        }

        private ClasseDeVoo MapearDtoParaDominio(ClasseDeVooDto classeDeVooDto)
        {
            return new ClasseDeVoo(
                classeDeVooDto.Descricao,
                classeDeVooDto.ValorFixoDoVoo,
                classeDeVooDto.ValorPorMilha
                );
        }
    }
}