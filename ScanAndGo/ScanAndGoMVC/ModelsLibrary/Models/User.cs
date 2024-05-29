using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelsLibrary.DtoModels;
using ModelsLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class User
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Email { get; private set; }
        public string Password { get; private set; }

        public string? Address { get; private set; }
        public string? ResetPasswordToken { get; private set; }
        public DateTime ResetPasswordExpire { get; private set; }
        public bool TemporalPassword { get; private set; }
        public UserTypes UserType { get; private set; }
        public int? WorkingInStoreId { get; private set; }
        public virtual Store? WorkingInStore { get; private set; }
        public List<Store>? OwnedStores { get; private set; }
        public string? VerificationCode { get; private set; }
        public DateTime VerifyEmailExpire { get; private set; }
        public bool IsVerified { get; private set; }

        public User() { }

        private User(int id, string name, string email, string password, UserTypes userType)
        {
            Id = id;
            Name = name; 
            Email = email;
            Password = password;
            UserType = userType;
        }

        public static User Create(int id, string name, string email, string password, UserTypes userType)
        {
            return new User(id, name, email, password, userType);
        }

        public void Update(string name, string email, UserTypes userType, int? workingInStoreId)
        {
            Name = name;
            Email = email;
            UserType = userType;
            WorkingInStoreId = workingInStoreId;
        }

        public void UpdateProfile(string address)
        {
            Address = address;
        }

        public void UpdatePassword(string password)
        {
            Password = password;    
        }

        public void UpdateResetPasswordParams(string resetPasswordToken, DateTime resetPasswordExpire)
        {
            ResetPasswordToken = resetPasswordToken;
            ResetPasswordExpire = resetPasswordExpire;
        }

        public void UpdateTemporalPassword(bool temporalPassword)
        {
            TemporalPassword = temporalPassword;
        }

        public void UpdateResetPasswordExpire(DateTime resetPasswordExpire)
        {
            ResetPasswordExpire = resetPasswordExpire;
        }

        public void UpdateVerificationParams(string verificationCode, DateTime verifyEmailExpire, bool isVerified)
        {
            VerificationCode = verificationCode;
            VerifyEmailExpire = verifyEmailExpire;
            IsVerified = isVerified;
        }

        public void UpdateIsVerified(bool isVerified)
        {
            IsVerified = isVerified;
        }

    }
}
