﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Persistence.Context;
using PruebaEjemploAPI.Transversal.Common.Settings;
using PruebaEjemploAPI.Transversal.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaEjemploAPI.Persistence.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IContextDB _contextDB;
        private readonly AppTokenSettings _appTokenSettings;

        public UsuarioRepository(IContextDB contextDB, IOptions<AppTokenSettings> appTokenSettings)
        {
            _contextDB = contextDB;
            _appTokenSettings = appTokenSettings.Value;
        }

        bool IUsuarioRepository.AddUsuario(Usuario usuario)
        {
            _contextDB.Usuarios.Add(usuario);

            var result = _contextDB.SaveChangesAsync()?.Result;

            return result > 0;
        }

        bool IUsuarioRepository.DeleteUsuario(int usuarioId)
        {
            var entity = GetUsuario(usuarioId);
            if (entity != null)
            {
                _contextDB.Usuarios.Remove(entity);
                var result = _contextDB.SaveChangesAsync()?.Result;
                return result > 0;
            }

            return true;
        }

        public Usuario? GetUsuario(int id)
        {
            return _contextDB.Usuarios.Find(id);
        }

        List<Usuario> IUsuarioRepository.GetUsuarios()
        {
            return _contextDB.Usuarios.ToList();
        }

        bool IUsuarioRepository.UpdateUsuario(Usuario usuario)
        {
            var cli = _contextDB.Usuarios.Update(usuario);
            if (cli != null)
            {
                var result = _contextDB.SaveChangesAsync()?.Result;
                return result > 0;
            }

            return false;
        }

        public Usuario Authenticate(string nombre, string password)
        {
            var users = _contextDB.Usuarios.Where(x => x.Nombre.Equals(nombre) && x.Password.Equals(password));

            try
            {
                if (users.Any())
                {
                    var completeUser = users.First();
                    completeUser.Token = BuildToken(completeUser.UsuarioId.ToString());
                    return completeUser;
                }
                else
                {
                    throw new UsuarioNotFoundException("Usuario no encontrado en la base de datos");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario> AuthenticateAsync(string nombre, string password)
        {
            var users = _contextDB.Usuarios.Where(x => x.Nombre.Equals(nombre) && x.Password.Equals(password));

            try
            {
                if (users.Any())
                {
                    var completeUser = users.First();
                    completeUser.Token = await BuildTokenAsync(completeUser.UsuarioId.ToString());
                    return completeUser;
                }
                else
                {
                    throw new UsuarioNotFoundException("Usuario no encontrado en la base de datos");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddUsuarioAsync(Usuario usuario)
        {
            await _contextDB.Usuarios.AddAsync(usuario);

            var result = await _contextDB.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Usuario?> GetUsuarioAsync(int id)
        {
            return await _contextDB.Usuarios.FindAsync(id);
        }

        public async Task<bool> DeleteUsuarioAsync(int usuarioId)
        {
            var entity = GetUsuario(usuarioId);
            if (entity != null)
            {
                _contextDB.Usuarios.Remove(entity);
                var result = await _contextDB.SaveChangesAsync();
                return result > 0;
            }

            return true;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _contextDB.Usuarios.ToListAsync();
        }

        public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
        {
            var cli = _contextDB.Usuarios.Update(usuario);
            if (cli != null)
            {
                var result = await _contextDB.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }

        private string BuildToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appTokenSettings.Secret);
            var tokenDescr = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appTokenSettings.Issuer,
                Audience = _appTokenSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescr);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private async Task<string> BuildTokenAsync(string userId)
        {
            return this.BuildToken(userId);
        }
    }
}
