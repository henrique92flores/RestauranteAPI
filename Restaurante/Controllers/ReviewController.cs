using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.DTOs.Restaurant;
using Restaurante.Data;
using Restaurante.Data.DTOs;
using Restaurante.Model;
using Restaurante.Data.DTOs.Enum;
using Restaurante.Data.DTOs.Food;

namespace Restaurante.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private FoodContext _context;

        public ReviewController(FoodContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um review ao banco de dados
        /// </summary>
        /// <param name="ReviewDto">Objeto com os campos necessários para criação de uma avaliacao</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaReview(
            [FromBody] ReviewDto reviewDto)
        {
            Review review = new Review();
            review.avaliacao = reviewDto.avaliacao;
            review.RestaurantId = reviewDto.RestaurantId;
            review.nota = reviewDto.nota;
            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Ok(reviewDto);
        }

        /// <summary>
        /// Recupera todos os review do banco de dados
        /// </summary>
        /// <param name="ReviewDto">Objeto com os campos necessários para criação de uma avaliacao</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpGet("{id}")]
        public IActionResult RecuperaReviwPorRestaurantPorId(int id)
        {
            var review = _context.Reviews
                .Where(review => review.RestaurantId == id);

            List<ReviewDto> ListaReviewDto = new List<ReviewDto>();

            if (review == null) return NotFound();

            foreach (var item in review)
            {
                ReviewDto reviewDto = new ReviewDto();
                reviewDto.nota = item.nota;
                reviewDto.avaliacao = item.avaliacao;
                reviewDto.Id = item.Id;
                reviewDto.RestaurantId = item.RestaurantId;
                ListaReviewDto.Add(reviewDto);
            }

            return Ok(ListaReviewDto);
        }

        //[HttpGet("/order/restaurant/{id}")]
        //public IActionResult RecuperaOrderByRestaurantId(int id)
        //{
        //    var order = _context.Orders
        //        .Where(order => order. == id);

        //    List<ReadFoodDto> ListafoodDto = new List<ReadFoodDto>();

        //    if (food == null) return NotFound();

        //    foreach (var item in food)
        //    {
        //        ReadFoodDto foodDto = new ReadFoodDto();
        //        foodDto.Id = item.Id;
        //        foodDto.Titulo = item.Titulo;
        //        foodDto.Imagem = item.Imagem;
        //        foodDto.Description = item.Description;
        //        foodDto.Preco = item.Preco;
        //        foodDto.TagsDescription = ((TagsFood)item.TagsId).ToString();
        //        foodDto.RestaurantId = item.RestaurantId;
        //        ListafoodDto.Add(foodDto);
        //    }


        //    //var filmeDto = _mapper.Map<ReadFoodDto>(food);
        //    return Ok(ListafoodDto);
        //}
    }
}
