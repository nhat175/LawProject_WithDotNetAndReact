﻿using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private ISmsService _smsService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ISmsService smsService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _smsService = smsService;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                CellPhone = "+90" + userForRegisterDto.CellPhone,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                TitleTr = userForRegisterDto.TitleTr,
                TitleEn = userForRegisterDto.TitleEn,
                IsActive = true,
                IsApproved = false,
                SmsCode = new Random().Next(0, 1000000).ToString("D6"),
                ProfileImage = ""
            };
            _userService.Add(user);
            string smsMessage = $"Hesap onay kodunuz {user.SmsCode}.";
            _smsService.SendIndividualMessage(smsMessage, user.CellPhone);
            if (user.IsApproved)
                return new SuccessDataResult<User>(user, "Kayıt oldu");
            return new SuccessDataResult<User>(null, "Hesabınızı cep telefonuna gelen kod ile onaylayınız!");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByCellPhone(userForLoginDto.CellPhone);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Kullanıcı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Parola hatası");
            }

            return new SuccessDataResult<User>(userToCheck, "Başarılı giriş");
        }

        public IResult UserExists(string cellPhone)
        {
            if (_userService.GetByCellPhone(cellPhone) != null)
            {
                return new SuccessResult("Kullanıcı mevcut!");
            }
            return new ErrorResult("Kullanıcı mevcut değil!");
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }

        public IDataResult<User> ApprovingSelectedUser(ApprovingUserDto approvingUserDto)
        {
            var isUserExists = UserExists(approvingUserDto.CellPhone);
            if (!isUserExists.Success)
                return new ErrorDataResult<User>(isUserExists.Message);

            var user = _userService.GetByCellPhone(approvingUserDto.CellPhone);
            if (user.SmsCode != approvingUserDto.SmsCode)
                return new ErrorDataResult<User>("Sms kodu yanlış.");

            user.IsApproved = true;
            _userService.Update(user);
            return new SuccessDataResult<User>(user, "Hesap Doğrulandı!");
        }
    }
}
