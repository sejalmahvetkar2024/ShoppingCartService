
using ProfileServices.Models;

namespace ProfileService.Interface
{
    public interface IUser
    {
        Task<long> InsertUserAndMerchantAsync(string fullName, string emailId, string passwordUser, string mobileNo, string gender, DateTime? dateOfBirth, string role, string? merchantName, string? merchantAddress, string? contactNumber, string? houseNumber, string streetName, string? colonyName, string city, string state, string pinCode);

        Task<IEnumerable<User>> getAllProfiles();
        Task<User> getByProfileId(int id);
        Task<bool> UpdateProfile(int id, User user, Address address, Merchant merchant);
        Task<IEnumerable<User>> GetProfileByUserName(string userName);

        Task<IEnumerable<User>> findByMobileNo(string mobileNo);
        Task<bool> deleteProfile(int id);
    }
}