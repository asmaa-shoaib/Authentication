using BusinessObjects.Authentication;
using BusinessObjects.Helper;
using BusinessObjects.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data_Access_Layer.Repository
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JWT Jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> Jwt)
        {
           this.userManager = userManager;
            this.Jwt = Jwt.Value;
    

        }
        public async Task<AuthModel> RegisterAsync(RegisterMoldel model)
        {
            if(await userManager.FindByEmailAsync(model.Email)!=null)
            {
                return new AuthModel { Message = "Email is already registered" };
            }
            if (await userManager.FindByEmailAsync(model.Username) != null)
            {
                return new AuthModel { Message = "Username is already registered" };
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            var result=await userManager.CreateAsync(user,model.Password);
            if(!result.Succeeded)
            {

                var Errors = string.Empty;
                foreach(var error in result.Errors)
                {
                    Errors += error.Description;
                }
                return new AuthModel { Message = Errors };
            }
            await userManager.AddToRoleAsync(user, "Admin");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpireOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Admin" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }
        public async Task<JwtSecurityToken>CreateJwtToken(ApplicationUser user)
        {
            var userClaims=await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims= new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add((new Claim("roles", role)));
            var claims = new[]
            {

                //new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim (JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims) ;


          //  ValidIssuer = "https://localhost:44363",
            //    ValidAudience = "https://localhost:44363",
           //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@45"))

            var symmetricSecurityKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.key));
            var SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var JwtSecurityKey = new JwtSecurityToken(
                issuer: Jwt.Issuer,
                audience:Jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(Jwt.TokenExpiryTimeInHour),
                signingCredentials: SigningCredentials
                );
            return JwtSecurityKey;
        }
   

        public async Task<AuthModel> GetTokenAsync(TokenRquestMoldel model)
        {
            var authModel=new AuthModel();
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null || !await userManager.CheckPasswordAsync(user,model.Password))
            {
                authModel.Message = "email or password is incorrect";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await userManager.GetRolesAsync(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpireOn = jwtSecurityToken.ValidTo;
            authModel.Roles=rolesList.ToList();
            return (authModel);
        }
    }

}
