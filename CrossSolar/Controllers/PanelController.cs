using System.Threading.Tasks;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CrossSolar.Controllers
{
    [Route("[controller]")]
    public class PanelController : Controller
    {
        private readonly IPanelRepository _panelRepository;

        public PanelController(IPanelRepository panelRepository)
        {
            _panelRepository = panelRepository;
        }

        // POST api/panel
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PanelModel item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var panel = new Panel
            {
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                Serial = item.Serial,
                Brand = item.Brand
            };

            await _panelRepository.InsertAsync(panel);

            return Created($"panel/{panel.Id}", panel);
        }
    }
}