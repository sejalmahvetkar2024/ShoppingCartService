using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Interface;
using ProfileServices.Models;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;

        // Constructor injection of UserRepository
        public UserController(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("GetAllProfiles")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var res = await _userRepository.getAllProfiles(); ;
            return Ok(res);
        }

        [HttpGet("getProfile/{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var result = await _userRepository.getByProfileId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Id not found");
        }
        [HttpPut("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] UserRequestModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                // Convert the request model to User, Address, and Merchant entities
                var user = new User
                {
                    FullName = request.FullName,
                    EmailId = request.EmailId,
                    PasswordUser = request.PasswordUser,
                    MobileNo = request.MobileNo,
                    Gender = request.Gender,
                    DateOfBirth = DateOnly.FromDateTime((DateTime)request.DateOfBirth)
                };

                var address = new Address
                {
                    HouseNumber = request.HouseNumber,
                    StreetName = request.StreetName,
                    ColonyName = request.ColonyName,
                    City = request.City,
                    State = request.State,
                    Pincode = request.Pincode
                };

                Merchant merchant = null;
                if (request.Role == "Merchant")
                {
                    merchant = new Merchant
                    {
                        MerchantName = request.MerchantName,
                        MerchantAddress = request.MerchantAddress,
                        ContactNumber = request.ContactNumber
                    };
                }

                // Call the repository method to update the user, address, and merchant
                var result = await _userRepository.UpdateProfile(id, user, address, merchant);

                if (result)
                {
                    return Ok(new { Message = "Profile updated successfully." });
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        [HttpGet("getProfileByUserName/{userName}")]
        public async Task<IActionResult> GetProfileByUserName(string userName)
        {
            var result = await _userRepository.GetProfileByUserName(userName);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Username not Found");
        }
        [HttpDelete("deleteProfile/{id}")]
        public async Task<IActionResult> deleteProfiles(int id)
        {
            bool res = await _userRepository.deleteProfile(id);
            if (!res)
            {
                return BadRequest();
            }
            return Ok("delete successfully");
        }

        [HttpGet("GetUsersByMobileNo/{mobileNo}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByMobileNo(string mobileNo)
        {
            var users = await _userRepository.findByMobileNo(mobileNo);

            if (users == null || !users.Any())
            {
                return NotFound(new { Message = "No users found with the given mobile number." });
            }

            return Ok(users);
        }

        [HttpPost("addNewProfile")]
        public async Task<IActionResult> AddUserAndMerchantAsync([FromBody] UserRequestModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                // Pass the necessary parameters based on the role
                long userId = await _userRepository.InsertUserAndMerchantAsync(
                    request.FullName,
                    request.EmailId,
                    request.PasswordUser,
                    request.MobileNo,
                    request.Gender,
                    request.DateOfBirth,
                    request.Role,
                    request.Role == "Merchant" ? request.MerchantName : null,
                    request.Role == "Merchant" ? request.MerchantAddress : null,
                    request.Role == "Merchant" ? request.ContactNumber : null,
                    request.HouseNumber,  // Address Fields
                    request.StreetName,
                    request.ColonyName,
                    request.City,
                    request.State,
                    request.Pincode
                );

                return Ok(new { Message = "User and merchant added successfully", UserId = userId });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }
    }

    //userrequestmodel to fetch data when user entering
    public class UserRequestModel
    {
        // Fields for the Users table
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string PasswordUser { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Role { get; set; }  // Role determines if the user is a Merchant or not

        // Fields for the Merchants table (only used if Role is Merchant)
        public string? MerchantName { get; set; }
        public string? MerchantAddress { get; set; }
        public string? ContactNumber { get; set; }

        // Address Fields
        public string? HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string? ColonyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
    }
}