using DemoSecurity_BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFCyberSecu_DemoSecurity_API.Models;
using TFCyberSecu_DemoSecurity_API.Tools;

namespace TFCyberSecu_DemoSecurity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleBLLService _articleBLLService;

        public ArticleController(IArticleBLLService articleBLLService)
        {
            _articleBLLService = articleBLLService;
        }

        [Authorize("connectedPolicy")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_articleBLLService.GetAll());
        }

        [Authorize("adminPolicy")]
        [HttpPost]
        public IActionResult Create([FromBody] ArticleFormDTO dto) 
        {
            if(!ModelState.IsValid) return BadRequest(dto);

            _articleBLLService.Create(dto.ToBLL());
            return Ok("Creation OK");
        }

        [Authorize("adminPolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _articleBLLService.Delete(id);
            return Ok();
        }


    }
}
