using AutoMapper;
using Coupon_Minimal_API_Project.Data;
using Coupon_Minimal_API_Project.Models;
using Coupon_Minimal_API_Project.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Coupon_Minimal_API_Project.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly string _secretKey;
    
    public AuthRepository(
        ApplicationDbContext db, 
        IMapper mapper, 
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        
        _secretKey = _configuration.GetValue<string>("ApiSettings:Secret") 
            ?? throw new InvalidOperationException("JWT secret key not configured");
    }

    public bool IsUniqueUser(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;
            
        var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
        return user == null;
    }

    public async Task<LoginResponseDTO?> Login(LoginRequestDTO loginRequestDTO)
    {
        if (loginRequestDTO == null || 
            string.IsNullOrWhiteSpace(loginRequestDTO.UserName) || 
            string.IsNullOrWhiteSpace(loginRequestDTO.Password))
        {
            return null;
        }

        var user = _db.ApplicationUsers.SingleOrDefault(x => x.UserName == loginRequestDTO.UserName);
        if (user == null)
        {
            return null;
        }

        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
        if (!isValid)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateJwtToken(user, roles.FirstOrDefault() ?? "user");

        return new LoginResponseDTO
        {
            User = _mapper.Map<UserDTO>(user),
            Token = token
        };
    }

    public async Task<UserDTO?> Register(RegisterationRequestDTO requestDTO)
    {
        if (requestDTO == null || 
            string.IsNullOrWhiteSpace(requestDTO.UserName) || 
            string.IsNullOrWhiteSpace(requestDTO.Name) || 
            string.IsNullOrWhiteSpace(requestDTO.Password))
        {
            return null;
        }

        var userObj = new ApplicationUser
        {
            UserName = requestDTO.UserName,
            Name = requestDTO.Name,
            NormalizedEmail = requestDTO.UserName.ToUpper(),
            Email = requestDTO.UserName,
        };

        try
        {
            var result = await _userManager.CreateAsync(userObj, requestDTO.Password);
            if (!result.Succeeded)
            {
                return null;
            }

            // Ensure roles exist
            await EnsureRolesExistAsync();
            
            // Add user to admin role by default
            await _userManager.AddToRoleAsync(userObj, "admin");

            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == requestDTO.UserName);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }
        catch
        {
            return null;
        }
    }

    private async Task EnsureRolesExistAsync()
    {
        if (!await _roleManager.RoleExistsAsync("admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("admin"));
        }
        
        if (!await _roleManager.RoleExistsAsync("customer"))
        {
            await _roleManager.CreateAsync(new IdentityRole("customer"));
        }
    }

    private string GenerateJwtToken(ApplicationUser user, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(ClaimTypes.Role, role),
                new(ClaimTypes.NameIdentifier, user.Id)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
