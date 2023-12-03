using Microsoft.AspNetCore.Mvc;
using Restaurante.Data;
using Restaurante.Model;
using Restaurante.Data.DTOs.User;
using Restaurante.Data.DTOs;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Restaurante.Migrations;
using System.Security.Cryptography;
using BCrypt.Net;
using System.Data.SqlTypes;

namespace Restaurante.Controllers;

[ApiController]
[Route("[controller]")]
//[EnableCors("customPolicy")]
public class UserController : ControllerBase
{
    private FoodContext _context;
    private readonly IConfiguration _configuration;

    public UserController(FoodContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Adiciona um food ao banco de dados
    /// </summary>
    /// <param name="userDto">Objeto com os campos necessários para criação de um food</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaUser(
        [FromBody] UserDto userDto)
    {
        if (validaUser(userDto, out string erro))
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string senhaCriptografada = HashSenhaComBcrypt(userDto.Senha, salt);

            User user = new User
            {
                Nome = userDto.Nome,
                CNPJ = userDto.CNPJ,
                Email = userDto.Email,
                Senha = senhaCriptografada,
                TipoUser = userDto.TipoCliente,
                Telefone = userDto.Telefone,
                PasswordHash = Convert.ToBase64String(salt)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }
        return BadRequest(erro);
    }

    private bool validaUser(UserDto userDto, out string erro)
    {
        erro = string.Empty;
        var useremail = _context.Users.FirstOrDefault(food => food.Email == userDto.Email);
        if (useremail != null)
        {
            erro = "Email já cadastrado: "+ userDto.Email;
            return false;
        }


        var usercnpj = _context.Users.FirstOrDefault(food => food.CNPJ == userDto.CNPJ);
        if (useremail != null)
        {
            erro = "CNPJ já cadastrado";
            return false;
        }


        return true;

    }


    /// <summary>
    /// Autentica Usuario no banco
    /// </summary>
    /// <param name="userDto">Objeto com os campos necessários para criação de um food</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost("/user/login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AutenticaUser(
        [FromBody] LoginDto loginDto)
    {
        // Aqui, você deve verificar as credenciais do usuário.
        // Se o login for bem-sucedido, você pode gerar um token JWT.
        int tipoUser;
        int userId;
        if (IsValidUser(loginDto.Email, loginDto.Senha, out tipoUser, out userId))
        {
            var token = GenerateJwtToken(loginDto.Email);

            return Ok(new { token, tipoUser, userId });
        }

        return Unauthorized();

        //bool logado = LogaUser(loginDto);
        //return NoContent();
    }

    private bool IsValidUser(string email, string senha, out int tipoUser, out int idUser)
    {
        var user = _context.Users.FirstOrDefault(food => food.Email == email);
        tipoUser = 0;
        idUser = 0;

        if (user == null)
            return false;

        tipoUser = user.TipoUser;
        idUser = user.Id;

        if (BCrypt.Net.BCrypt.Verify(senha, user.Senha))
        {
            return true;
        }

        return false;
    }

    private string GenerateJwtToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private User SaveHistory(UserDto userDto)
    {

        User user = new User();

        user.Nome = userDto.Nome;
        user.CNPJ = userDto.CNPJ;
        user.Email = userDto.Email;
        user.Senha = userDto.Senha;
        user.TipoUser = userDto.TipoCliente;
        user.Telefone = userDto.Telefone;

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }


    private string HashSenhaComBcrypt(string senha, byte[] salt)
    {
        const int custoTrabalho = 12;

        string hashedSenha = BCrypt.Net.BCrypt.HashPassword(senha, BCrypt.Net.BCrypt.GenerateSalt(custoTrabalho));

        return hashedSenha;
    }

    [HttpGet]
    public IEnumerable<UserDto> RecuperaUser([FromQuery] int skip = 0,
[FromQuery] int take = 50)
    {
        var user = _context.Users.Skip(skip).Take(take);
        List<UserDto> listaDto = new List<UserDto>();
        foreach (var item in user)
        {
            UserDto userDto = new UserDto();
            userDto.Nome = item.Nome;
            userDto.CNPJ = item.CNPJ;
            userDto.Email = item.Email;
            userDto.Senha = item.Senha;
            userDto.TipoCliente = item.TipoUser;
            userDto.ConfirmaSenha = item.Senha;
            userDto.Telefone = item.Telefone;
            listaDto.Add(userDto);
        }

        return listaDto;
    }


    [HttpPut("{id}")]
    public IActionResult AtualizaUser(int id,
        [FromBody] UserDto userDto)
    {
        var user = _context.Users.FirstOrDefault(
            p => p.Id == id);
        if (user == null) return NotFound();

        byte[] saltBytes = Convert.FromBase64String(user.PasswordHash);
        userDto.Senha = HashSenhaComBcrypt(userDto.Senha, saltBytes);

        SaveHistory(userDto);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult DeletaUser(int id)
    {
        var food = _context.Users.FirstOrDefault(
            food => food.Id == id);
        if (food == null) return NotFound();
        _context.Remove(food);
        _context.SaveChanges();
        return NoContent();
    }
}
