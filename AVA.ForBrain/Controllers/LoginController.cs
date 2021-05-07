using AVA.ForBrain.ApplicationCore.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories.Common;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories;
using AVA.ForBrain.ApplicationCore.Entities;
using AVA.ForBrain.ApplicationCore.AppSetting;
using Microsoft.Extensions.Options;
using AVA.ForBrain.Validators;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using AVA.ForBrain.Models;
using Newtonsoft.Json;
using System.Net;
using AVA.ForBrain.ApplicationCore.Enum;

namespace AVA.ForBrain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuariosRepository _repository;
        private readonly ILogger<Usuarios> _logger;
        private readonly IUnitOfWork _uow;
        private readonly AppSettingsSecret _appSettings;

        public LoginController(ILogger<Usuarios> logger, IOptions<AppSettingsSecret> appSettings,
            IUsuariosRepository repository, IUnitOfWork unitOf)
        {
            _repository = repository;
            _logger = logger;
            _uow = unitOf;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> Get()
        {
            var usuarios = await _repository.GetAll();

            return Ok(usuarios);
        }

        [HttpPost("autenticate")]
        public async Task<ActionResult> Login([FromBody] object param)
        {
            try
            {
                var user = DynamicJsonConverter.Deserialize<Usuarios>(param.ToString());

                var result = await AuthenticateViewModel(user.User, user.Password);

                var authencticate = result == null ? Retorno.Error : Retorno.Sucesso;

                switch (authencticate)
                {
                    case Retorno.Sucesso:
                        return ReturnObjecResult(string.Empty, true, "Sucesso", HttpStatusCode.OK, result);
                    case Retorno.Error:
                        return ReturnObjecResult("Login ou senha invalido.", false, "Login ou senha invalido.", HttpStatusCode.NoContent, result);
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                return ReturnObjecResult(ex.Message, false, ex.StackTrace, HttpStatusCode.NotFound, ex);
            }
        }

        [HttpPost("registrar")]
        public ActionResult Registrar([FromForm] Usuarios entity)
        {
            var validator = UsuariosValidator.Instancia.Validate(entity);

            var result = new ModelResult();

            if (validator.IsValid)
            {
                var model = _repository.Insert(entity);

                if (!(model is null))
                    return ReturnObjecResult(string.Empty, validator.IsValid, "Aluno cadastrado com sucesso.", HttpStatusCode.OK);
                else
                    return ReturnObjecResult(string.Empty, validator.IsValid, "Nao foi possivel cadastrar o aluno.", HttpStatusCode.NoContent);
            }
            else
                return ReturnObjecResult(string.Empty, validator.IsValid, "Nao foi possivel cadastrar o aluno.", HttpStatusCode.NotFound, DynamicJsonConverter.Serialize(validator.Errors));
        }

        #region Metodos privados
        private ObjectResult ReturnObjecResult(string error, bool isvalid, string message, HttpStatusCode status, Object obj = null)
        {
            var result = new ModelResult
            {
                Data = obj,
                Error = error,
                Valid = isvalid,
                Message = message,
                StatusCode = status,
            };
            return new ObjectResult(result);
        }
        private ObjectResult ObjectResult(object param)
        {
            return new ObjectResult(param);
        }
        private async Task<AuthenticateModel> AuthenticateViewModel(string username, string password)
        {
            var user = _repository.Authenticate(username, password);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var result = new AuthenticateModel();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
               }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                   new SymmetricSecurityKey(key),
                   SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Token = tokenHandler.WriteToken(token);

            result.Id = user.IdUsuario;
            result.FirstName = user.User;
            result.LastName = user.Sobrenome;
            result.Username = string.Concat(user.User, user.Sobrenome);
            result.Password = string.Empty;

            return await Task.FromResult(result);
        }

        #endregion
    }
}
