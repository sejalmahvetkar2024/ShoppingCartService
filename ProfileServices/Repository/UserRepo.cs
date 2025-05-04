using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProfileService.Interface;
using ProfileServices.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProfileService.Repository
{
    public class UserRepo : IUser
    {
        //dependency ibjection for context file
        private readonly ShoppingCartContext _context;
        public UserRepo(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> getAllProfiles()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User> getByProfileId(int id)
        {
            var existingProfile = await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);
            if (existingProfile != null)
            {
                return existingProfile;
            }
            return null;
        }

        public async Task<long> InsertUserAndMerchantAsync(string fullName, string emailId, string passwordUser, string mobileNo, string gender, DateTime? dateOfBirth, string role, string? merchantName, string? merchantAddress, string? contactNumber, string? houseNumber, string streetName, string? colonyName, string city, string state, string pinCode)
        {
            // Define a parameterized SQL command to call the stored procedure
            var parameters = new[]
            {
            new SqlParameter("@FullName", fullName),
            new SqlParameter("@EmailId", emailId),
            new SqlParameter("@PasswordUser", passwordUser),
            new SqlParameter("@MobileNo", mobileNo),
            new SqlParameter("@Gender", gender),
            new SqlParameter("@DateOfBirth", (object)dateOfBirth ?? DBNull.Value), // Handling nullable DateOfBirth
            new SqlParameter("@Role", role),
            new SqlParameter("@MerchantName", (object)merchantName ?? DBNull.Value), // MerchantName is optional
            new SqlParameter("@MerchantAddress", (object)merchantAddress ?? DBNull.Value), // MerchantAddress is optional,
            new SqlParameter("@ContactNumber", (object)contactNumber ?? DBNull.Value) ,// ContactNumber is optional
            new SqlParameter("@HouseNumber", (object)houseNumber ?? DBNull.Value),
            new SqlParameter("@StreetName", streetName),
            new SqlParameter("@ColonyName", (object)colonyName ?? DBNull.Value),
            new SqlParameter("@City", city),
            new SqlParameter("@State", state),
            new SqlParameter("@PinCode", pinCode)

        };

            // Execute the stored procedure using ExecuteSqlRawAsync and capture the UserId returned by the stored procedure
            var userId = await _context.Database.ExecuteSqlRawAsync("EXEC InsertUserAndMerchant @FullName, @EmailId, @PasswordUser, @MobileNo, @Gender, @DateOfBirth, @Role, @MerchantName, @MerchantAddress, @ContactNumber, @HouseNumber, @StreetName, @ColonyName, @City, @State, @PinCode", parameters);

            // Return the userId, which is the result of the stored procedure
            return userId;
        }

        public async Task<bool> UpdateProfile(int id, User user, Address address, Merchant merchant)
        {
            var res = await _context.Users
                .Include(u => u.Addresses)
                .Include(u => u.Merchant)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (res != null)
            {
                // Update user profile details
                res.FullName = user.FullName;
                res.EmailId = user.EmailId;
                res.PasswordUser = user.PasswordUser;
                res.MobileNo = user.MobileNo;
                res.Gender = user.Gender;
                res.DateOfBirth = user.DateOfBirth;

                // Update Address if provided
                if (address != null)
                {
                    if (res.Addresses.Any())
                    {
                        var existingAddress = res.Addresses.FirstOrDefault();
                        if (existingAddress != null)
                        {
                            existingAddress.HouseNumber = address.HouseNumber;
                            existingAddress.StreetName = address.StreetName;
                            existingAddress.ColonyName = address.ColonyName;
                            existingAddress.City = address.City;
                            existingAddress.State = address.State;
                            existingAddress.Pincode = address.Pincode;
                        }
                    }
                    else
                    {
                        // Add a new address if none exist
                        res.Addresses.Add(new Address
                        {
                            HouseNumber = address.HouseNumber,
                            StreetName = address.StreetName,
                            ColonyName = address.ColonyName,
                            City = address.City,
                            State = address.State,
                            Pincode = address.Pincode
                        });
                    }
                }

                // Update Merchant details if provided
                if (merchant != null)
                {
                    var userMerchant = res.Merchant;
                    if (userMerchant != null)
                    {
                        userMerchant.MerchantName = merchant.MerchantName;
                        userMerchant.MerchantAddress = merchant.MerchantAddress;
                        userMerchant.ContactNumber = merchant.ContactNumber;
                    }
                    else
                    {
                        res.Merchant = new Merchant
                        {
                            MerchantName = merchant.MerchantName,
                            MerchantAddress = merchant.MerchantAddress,
                            ContactNumber = merchant.ContactNumber
                        };
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        //get user profile by username
        public async Task<IEnumerable<User>> GetProfileByUserName(string userName)
        {
            var existingProfiles = await _context.Users
                .Where(user => user.FullName == userName)
                .ToListAsync();

            return existingProfiles;
        }

        public async Task<bool> deleteProfile(int id)
        {
            var res = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (res == null)
            {
                return false;
            }
            _context.Users.Remove(res);
            await _context.SaveChangesAsync();
            return true;

        }

        //get user profioe by mobile no
        public async Task<IEnumerable<User>> findByMobileNo(string mobileNo)
        {
            // Return a collection of users matching the mobile number
            return await _context.Users
                                 .Where(u => u.MobileNo == mobileNo)
                                 .ToListAsync();
        }
    }
}

//Stored procedure for sql server management
//use EshoppingCart
//drop procedure InsertUserAndMerchant;

//CREATE PROCEDURE InsertUserAndMerchant
 
//    @FullName NVARCHAR(100),

//    @EmailId NVARCHAR(100),

//    @PasswordUser NVARCHAR(100),

//    @MobileNo NVARCHAR(100),

//    @Gender NVARCHAR(50),

//    @DateOfBirth Date = NULL,

//    @Role NVARCHAR(50),

//    @MerchantName NVARCHAR(100) = NULL,

//    @MerchantAddress NVARCHAR(200) = NULL,

//    @ContactNumber NVARCHAR(15) = NULL,
 
//    -- Address parameters
//	@HouseNumber NVARCHAR(200) =null,
//    @StreetName NVARCHAR(200),
//    @ColonyName NVARCHAR(200) = null,
//    @City NVARCHAR(200) ,
//    @State NVARCHAR(200),
//    @PinCode NVARCHAR(200)
 
//AS
 
//BEGIN
 
//    SET NOCOUNT ON; --Prevents the "rows affected" message from being sent to the client.
//    -- Insert into Users table for both User and Merchant roles
 
//    INSERT INTO Users (FullName, EmailId, PasswordUser, MobileNo, Gender, DateOfBirth, Role)
 
//    VALUES (@FullName, @EmailId, @PasswordUser, @MobileNo, @Gender, @DateOfBirth, @Role);
//--Get the generated UserId for the newly inserted User
 
//    DECLARE @UserId BIGINT = SCOPE_IDENTITY();
//--Insert Address information if provided
 
//    IF @StreetName IS NOT NULL AND @City IS NOT NULL AND @State IS NOT NULL AND  @PinCode IS NOT NULL
 
//    BEGIN
 
//        INSERT INTO Addresses(UserId, HouseNumber, StreetName, ColonyName, City, State, Pincode)
 
//        VALUES (@UserId, @HouseNumber, @StreetName, @ColonyName, @City, @State, @Pincode);

//END
//-- Conditional logic based on Role
 
//    IF @Role = 'Merchant'
 
//    BEGIN
 
//            -- Insert into Merchants table
 
//            INSERT INTO Merchants (UserId, MerchantName, MerchantAddress, ContactNumber)
 
//            VALUES (@UserId, @MerchantName, @MerchantAddress, @ContactNumber);

//END
//-- Return the generated UserId
 
//    SELECT @UserId AS UserId;

//END
 

//select * from Users;
//select* from Merchants;
//select* from Addresses;