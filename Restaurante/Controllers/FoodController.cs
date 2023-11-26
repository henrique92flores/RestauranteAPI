
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurante.Data;
using Restaurante.Data.DTOs.Food;
using Restaurante.Model;

namespace Restaurante.Controllers;

[ApiController]
[Route("[controller]")]
//[EnableCors("customPolicy")]
public class FoodController : ControllerBase
{

    private FoodContext _context;

    public FoodController(FoodContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adiciona um food ao banco de dados
    /// </summary>
    /// <param name="foodDto">Objeto com os campos necessários para criação de um food</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFood(
        [FromBody] CreateFoodDto foodDto)
    {
        var food = SaveHistory(foodDto);

        return CreatedAtAction(nameof(RecuperaFoodPorId),
            new { id = food.Id },
            food);
    }

    private Food SaveHistory(CreateFoodDto foodDto)
    {
        Food food = new Food();
        food.Preco = foodDto.Preco;
        food.TagsId = foodDto.TagsId;
        food.Titulo = foodDto.Titulo;
        food.RestaurantId = foodDto.RestaurantId;
        food.Imagem = foodDto.Imagem;
        food.Description = foodDto.Description;
        _context.Foods.Add(food);
        _context.SaveChanges();
        return food;
    }

    [HttpGet]
    public IEnumerable<ReadFoodDto> RecuperaFoods([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        var food = _context.Foods.Skip(skip).Take(take);
        List<ReadFoodDto> listaDto = new List<ReadFoodDto>();
            foreach (var item in food)
        {
            ReadFoodDto foodDto = new ReadFoodDto();
            foodDto.Id = item.Id;
            foodDto.Preco = item.Preco;
            //foodDto.TagsId = item.TagsId;
            foodDto.Titulo = item.Titulo;
            foodDto.RestaurantId = item.RestaurantId;
            foodDto.Imagem = item.Imagem;
            foodDto.Description = item.Description;
            listaDto.Add(foodDto);
        }

        return listaDto;
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFoodPorId(int id)
    {
        var food = _context.Foods
            .FirstOrDefault(food => food.Id == id);

        if (food == null) return NotFound();
        ReadFoodDto filmeDto = new ReadFoodDto();
        filmeDto.Preco = food.Preco;
        //filmeDto.TagsId = food.Preco;
        filmeDto.Titulo = food.Titulo;
        filmeDto.RestaurantId = food.RestaurantId;
        filmeDto.Imagem = food.Imagem;
        filmeDto.Description = food.Description;
        //var filmeDto = _mapper.Map<ReadFoodDto>(food);
        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFood(int id,
        [FromBody] CreateFoodDto foodDto)
    {
        var food = _context.Foods.FirstOrDefault(
            food => food.Id == id);
        if (food == null) return NotFound();


        food.Preco = foodDto.Preco;
        food.TagsId = foodDto.TagsId;
        food.Titulo = foodDto.Titulo;
        food.Imagem = foodDto.Imagem;
        food.Description = foodDto.Description;
        _context.SaveChanges();
        return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult DeletaFood(int id)
    {
        var food = _context.Foods.FirstOrDefault(
            food => food.Id == id);
        if (food == null) return NotFound();
        _context.Remove(food);
        _context.SaveChanges();
        return Ok();
    }
}
