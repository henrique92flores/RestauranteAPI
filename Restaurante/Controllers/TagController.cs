﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Restaurante.Data;
using Restaurante.Data.DTOs;
using Restaurante.Data.DTOs.Enum;
using Restaurante.Data.DTOs.Food;
using Restaurante.Data.DTOs.Restaurant;

namespace Restaurante.Controllers
{
    [Route("Tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private FoodContext _context;

        public TagController(FoodContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IEnumerable<TagDto> RecuperaTags()
        {
            var tag = _context.Tags.OrderBy(p => p.Id).ToList();
            List<TagDto> listaDto = new List<TagDto>();

            if (tag == null || !tag.Any())
                return listaDto;

            foreach (var item in tag)
            {
                TagDto foodDto = new TagDto();

                foodDto.Id = item.Id;
                foodDto.Descricao = item.Descricao;

                listaDto.Add(foodDto);
            }

            return listaDto;
        }
    }
}
