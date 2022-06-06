using System;
using System.Threading.Tasks;
using FlyIBooking.Dtos;
using FlyIBooking.Generators;

namespace FlyIBooking.Services.Auth
{
    public sealed class AuthService
    {
        public string SecurityKey { get; set; }

        private readonly AccountService _accountService;

        private readonly HashGenerator _hashGenerator;

        private readonly JwtGenerator _jwtGenerator;

        public AuthService(
            AccountService accountService,
            HashGenerator hashGenerator,
            JwtGenerator jwtGenerator)
        {
            _accountService = accountService;
            _hashGenerator = hashGenerator;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<string?> LoginAsync(AccountLoginDto accountLoginDto)
        {
            if (accountLoginDto == null)
                throw new ArgumentNullException(nameof(accountLoginDto));

            if (string.IsNullOrEmpty(accountLoginDto.Login) || string.IsNullOrEmpty(accountLoginDto.Password))
                return null;

            var accFromBase = await _accountService.GetByEmailAsync(accountLoginDto.Login);
            if (accFromBase == null)
                return null;

            if (!CheckPasswords(accFromBase.PasswordHash, accountLoginDto.Password))
                return null;

            return _jwtGenerator.GenerateJwt(accFromBase, SecurityKey);
        }

        public async Task<string?> RegisterAsync(AccountRegisterDto accountRegisterDto)
        {
            if (accountRegisterDto == null)
                throw new ArgumentNullException(nameof(accountRegisterDto));

            if (string.IsNullOrEmpty(accountRegisterDto.Email) || string.IsNullOrEmpty(accountRegisterDto.Password))
                return null;

            var existingAccount = await _accountService.GetByEmailAsync(accountRegisterDto.Email);
            if (existingAccount is not null)
                return null;

            var newId = Guid.NewGuid();
            var accountForRegister = new AccountDto
            {
                Id = newId,
                PasswordHash = _hashGenerator.GenerateHash(accountRegisterDto.Password),
                Email = accountRegisterDto.Email
            };

            if (await _accountService.AddAsync(accountForRegister))
                return _jwtGenerator.GenerateJwt(accountForRegister, SecurityKey);

            return null;
        }

        private bool CheckPasswords(string passwordHash, string passwordFromLogin)
        {
            return _hashGenerator.GenerateHash(passwordFromLogin) == passwordHash;
        }
    }
}