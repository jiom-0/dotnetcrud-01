using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Roles{
    public static string GerarToken(string email, string role, IConfiguration configuration){
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string ValidaCamposProduto(Produto produto){
        if(produto.Preco < 0) return "Preço do produto não pode ser menor que zero";
        if(produto.SldAtual < 0) return "Quantidade atual não pode ser menor que zero";
        if(string.IsNullOrWhiteSpace(produto.Nome)) return "Nome do produto não pode ser nulo";
        return "";
    }
}