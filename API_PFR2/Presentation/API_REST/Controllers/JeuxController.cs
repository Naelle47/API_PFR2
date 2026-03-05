using API_PFR2.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using API_PFR2.BLL.Services.Interfaces; 
using API_PFR2.Presentation.API_REST.DTO.Responses;


namespace API_PFR2.Presentation.API_REST.Controllers;

/// <summary>
/// 
/// </summary>

[ApiController]
[Route("api/[controller]")]
public class JeuxController : APIBaseController
{
    private readonly IJeuService _jeuService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jeuService"></param>
    public JeuxController(IJeuService jeuService)
    {
        _jeuService = jeuService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<JeuResponse>> GetAllJeux()
    {
        var jeux = _jeuService.GetCatalogue()
        .Select(j => new JeuResponse
        {
            Id = j.id,
            Nom = j.nom
        }).ToList();
        return Ok(jeux);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<JeuResponse> GetById(int id)
    {
        try
        {
            var jeu = _jeuService.GetJeuById(id);
            var response = new JeuResponse
            {
                Id = jeu.id,
                Nom = jeu.nom
            };
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}
