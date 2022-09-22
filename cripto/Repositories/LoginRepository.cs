﻿using cripto.Data;
using cripto.Interfaces;
using cripto.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace cripto.Repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly CriptoContext ctx;
        public LoginRepository(CriptoContext _ctx)
        {
            ctx = _ctx;
        }

        public string Logar(string email, string senha)
        {
            //return ctx.Usuarios.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            var usuario = ctx.Usuarios.FirstOrDefault(x => x.Email == email);

            if(usuario != null)
            {
                bool confere = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
                if (confere)
                {
                    // Criar as credenciais do JWT
                    
                    // Definimos as claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Adm"),

                        new Claim("Cargo", "Adm")
                    };

                    // Criamos as chaves
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("cripto-chave-autenticacao"));

                    // Criamos as credenciais
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Geramos o token
                    var meuToken = new JwtSecurityToken(
                        issuer: "cripto.webAPI",
                        audience: "cripto.webAPI",
                        claims: minhasClaims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                    );

                    // Retornamos o token em formato de string
                    return new JwtSecurityTokenHandler().WriteToken(meuToken);

                }
                    
            }

            return null;
        }
    }
}
