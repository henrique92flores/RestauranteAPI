using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.DTOs.Food;
using Restaurante.Data;
using Restaurante.Model;
using Restaurante.Data.DTOs.Restaurant;
using Restaurante.Data.DTOs;
using Microsoft.AspNetCore.Cors;
using Restaurante.Data.DTOs.Enum;

namespace Restaurante.Controllers
{

    [ApiController]
    [Route("[controller]")]
    //[EnableCors("customPolicy")]
    public class RestauntController: ControllerBase
    {
        private FoodContext _context;

        public RestauntController(FoodContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um food ao banco de dados
        /// </summary>
        /// <param name="RestaurantDto">Objeto com os campos necessários para criação de um food</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaRestaurant(
            [FromBody] RestaurantDto foodDto)
        {
            var food = SaveHistory(foodDto);

            //return CreatedAtAction(nameof(RecuperaRestaurantPorId),
            //    new { id = food.Id },
            //    food);

            return Ok(foodDto);
        }

        private Restaurant SaveHistory(RestaurantDto foodDto)
        {
            Restaurant food = new Restaurant();
            food.Addres = new Addres();
            food.Addres.Rua = foodDto.AddresDto.Rua;
            food.Addres.CEP = foodDto.AddresDto.CEP;
            food.Addres.Numero = foodDto.AddresDto.Numero;
            food.Addres.Estado = foodDto.AddresDto.Estado;
            food.Addres.Complemento = foodDto.AddresDto.Complemento;
            food.CNPJ = foodDto.CNPJ;
            food.NomeFantasia = foodDto.NomeFantasia;
            food.Imagem = foodDto.Imagem;
            food.Avalicao = foodDto.Avalicao;
            food.Nome = foodDto.Nome;
            _context.Restaurants.Add(food);
            _context.SaveChanges();
            return food;
        }

        [HttpGet]
        public IEnumerable<RestaurantDto> RecuperaRestaurant([FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            var food = _context.Restaurants.Skip(skip).Take(take).OrderBy(p => p.Id).ToList();
            List<RestaurantDto> listaDto = new List<RestaurantDto>();

            if (food == null || !food.Any())
                return listaDto;

            foreach (var item in food)
            {
                RestaurantDto foodDto = new RestaurantDto();
                //foodDto.AddresDto = new AddresDto();
                //foodDto.AddresDto.Rua = item.Addres.Rua;
                //foodDto.AddresDto.CEP = item.Addres.CEP;
                //foodDto.AddresDto.Numero = item.Addres.Numero;
                //foodDto.AddresDto.Complemento = item.Addres.Complemento;
                //foodDto.AddresDto.Estado = item.Addres.Estado;
                foodDto.Id = item.Id;
                foodDto.Nome = item.Nome;
                foodDto.CNPJ = item.CNPJ;
                foodDto.Imagem = item.Imagem;
                foodDto.Avalicao = item.Avalicao;
                foodDto.NomeFantasia = item.NomeFantasia;
                listaDto.Add(foodDto);
            }

            return listaDto;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaRestaurantPorId(int id)
        {
            var food = _context.Restaurants
                .FirstOrDefault(food => food.Id == id);

            RestaurantDto foodDto = new RestaurantDto();
            foodDto.AddresDto = new AddresDto();

            if (food == null) return NotFound();

            foodDto.AddresDto.Rua = food.Addres.Rua;
            foodDto.AddresDto.CEP = food.Addres.CEP;
            foodDto.AddresDto.Numero = food.Addres.Numero;
            foodDto.AddresDto.Complemento = food.Addres.Complemento;
            foodDto.AddresDto.Estado = food.Addres.Estado;
            foodDto.Nome = food.Nome;
            foodDto.CNPJ = food.CNPJ;
            foodDto.Imagem = food.Imagem;
            foodDto.Avalicao = food.Avalicao;
            foodDto.NomeFantasia = food.NomeFantasia;
            //var filmeDto = _mapper.Map<ReadFoodDto>(food);
            return Ok(foodDto);
        }
        [HttpGet("/restaurant/food/{id}")]
        public IActionResult RecuperaFoodPorRestaurantPorId(int id)
        {
            var food = _context.Foods
                .Where(food => food.RestaurantId == id);

            List<ReadFoodDto> ListafoodDto = new List<ReadFoodDto>();

            if (food == null) return NotFound();

            foreach (var item in food)
            {
                ReadFoodDto foodDto = new ReadFoodDto();
                foodDto.Id = item.Id;
                foodDto.Titulo = item.Titulo;
                foodDto.Imagem = item.Imagem;
                foodDto.Description = item.Description;
                foodDto.Preco = item.Preco;
                foodDto.TagsDescription = ((TagsFood)item.TagsId).ToString();
                foodDto.RestaurantId = item.RestaurantId;
                ListafoodDto.Add(foodDto);
            }


            //var filmeDto = _mapper.Map<ReadFoodDto>(food);
            return Ok(ListafoodDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaRestaurante(int id,
            [FromBody] RestaurantDto foodDto)
        {
            var food = _context.Restaurants.FirstOrDefault(
                food => food.Id == id);
            if (food == null) return NotFound();

            food.Addres.Rua = foodDto.AddresDto.Rua;
            food.Addres.CEP = foodDto.AddresDto.CEP;
            food.Addres.Numero = foodDto.AddresDto.Numero;
            food.Addres.Estado = foodDto.AddresDto.Estado;
            food.Addres.Complemento = foodDto.AddresDto.Complemento;
            food.CNPJ = foodDto.CNPJ;
            food.NomeFantasia = foodDto.NomeFantasia;
            food.Imagem = foodDto.Imagem;
            food.Avalicao = foodDto.Avalicao;
            food.Nome = foodDto.Nome;

            _context.SaveChanges();
  
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaRestaurante(int id)
        {
            var food = _context.Restaurants.FirstOrDefault(
                food => food.Id == id);
            if (food == null) return NotFound();
            _context.Remove(food);
            _context.SaveChanges();
            return Ok();
        }
    }
}
