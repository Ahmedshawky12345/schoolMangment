using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace schoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly ITeacher repoteacher;
        private readonly IStudent repostudent;

        public UserController(UserManager<AppUser> userManager,IMapper mapper, IConfiguration config,IStudent repostudent,ITeacher repoteacher)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.config = config;
            this.repoteacher = repoteacher;
            this.repostudent = repostudent;
        }
        [HttpPost("Regster")]
        public async Task<IActionResult> Regstration([FromBody] RegsterDTO regsterDTO)
        {
            var response = new GenralResponse<RegsterDTO>();

            // Validation check
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Validation failed";

                // Collect all errors from ModelState
                response.Errors = ModelState.Values.SelectMany(v => v.Errors)
                                                    .Select(e => e.ErrorMessage)
                                                    .ToList();

                return BadRequest(response);
            }

            // Map RegisterDTO to AppUser
            var user = mapper.Map<AppUser>(regsterDTO);

            // Attempt to create the user
            IdentityResult result = await userManager.CreateAsync(user, regsterDTO.Password);

            if (result.Succeeded)
            {
                // Add user to the specified role
                await userManager.AddToRoleAsync(user, regsterDTO.Role);

                // Handle role-based registration
                if (regsterDTO.Role == "Student")
                {
                    // Call AddStudent method
                    await repostudent.AddStudent(regsterDTO, user.Id);
                }
                else if (regsterDTO.Role == "Teacher")
                {
                    // Call AddTeacher method
                    await repoteacher.AddTeacher(regsterDTO, user.Id);
                }

                response.Success = true;
                response.Message = "User created successfully";
                return Ok(response);
            }

            // If creation failed, return errors
            response.Success = false;
            response.Message = "User creation failed";
            response.Errors = result.Errors.Select(e => e.Description).ToList();

            return BadRequest(response);
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = new GenralResponse<LoginDTO>();
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Valdation falid";
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(response);
            }
          var user=await  userManager.FindByNameAsync(loginDTO.username);
            if (user != null)
            {
                bool done = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (done)
                {
                   

                    //-------------------------------------- Genrate Token --------------------------
                    var _claims = new List<Claim>();
                    _claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    _claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    _claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    // add rols => taks list and foter add this list in claims
                    var role = await userManager.GetRolesAsync(user);
                    foreach (var i in role)
                    {
                        _claims.Add(new Claim(ClaimTypes.Role, i));

                    }
                    // singing credintioal

                    SecurityKey _securitrykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:secret"].PadRight(48)));

                    SigningCredentials _singingcre = new SigningCredentials(_securitrykey, SecurityAlgorithms.HmacSha384);

                    // this token as a json

                    JwtSecurityToken mytoken = new JwtSecurityToken(
                        issuer: config["JWT:valid_issur"],
                        audience: config["JWT:valdid_audiance"],
                        claims: _claims,
                        // token end in the hour from now
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: _singingcre


                        );
                    return Ok(
                        new
                        {
                            // creat token as a compact =>   json=>>>>> compact
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo,
                            userId = user.Id,
                            role = role.FirstOrDefault()
                        }
                        );

                }
            }
            response.Success = false;
            response.Message = "Invalid login attempt";
            return Unauthorized(response);
        }
    }

}
 