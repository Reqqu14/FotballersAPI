using FotballersAPI.Application.Infrastructure.Authentication;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FotballersAPI.Application.Functions.Users.Commands.LoginCommand
{
    public class LoginUserCommand
    {
        public class ModelValidator : LoginUserCommandModelValidator<LoginUserCommandRequest>
        {

        }

        public class BusinessValidator : LoginUserCommandBusinessValidator<LoginUserCommandRequest>
        {
            public BusinessValidator(
                IUserRepository userRepository, 
                IPasswordHasher<User> passwordHasher) : base(userRepository, passwordHasher)
            {

            }
        }

        public class Handler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly AuthenticationSettings _authenticationSettings;

            public Handler(IUserRepository userRepository, AuthenticationSettings authenticationSettings)
            {
                _userRepository = userRepository;
                _authenticationSettings = authenticationSettings;
            }

            public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByLoginAsync(request.Login, cancellationToken);

                var claims = PrepareClaims(user);

                string token = GenerateJwtToken(claims);

                return new LoginUserCommandResponse() { Token = token };
            }            

            private List<Claim> PrepareClaims(User user)
            {
                return new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                };
            }

            private string GenerateJwtToken(List<Claim> claims)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiresIn = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

                var token = new JwtSecurityToken(
                    _authenticationSettings.JwtIssuer,
                    _authenticationSettings.JwtIssuer,
                    claims,
                    expires: expiresIn,
                    signingCredentials: credentials);

                var tokenHandler = new JwtSecurityTokenHandler();

                return tokenHandler.WriteToken(token);
            }
        }
    }
}
