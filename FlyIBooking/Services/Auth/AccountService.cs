using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FlyIBooking.Converters;
using FlyIBooking.Dtos;
using FlyIBooking.Entities;
using FlyIBooking.Repositories;

namespace FlyIBooking.Services.Auth
{
    public sealed class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var account = await _accountRepository.GetAccountByEmailAsync(email);

            return account.ToDto();
        }

        public async Task<bool> AddAsync(AccountDto registeredAccount)
        {
            if (registeredAccount == null)
                throw new ArgumentNullException(nameof(registeredAccount));
            if (string.IsNullOrEmpty(registeredAccount.Email))
                return false;

            var account = registeredAccount.ToDal();

            account.Tickets = new List<TicketDal>();

            await _accountRepository.AddAsync(account);

            return true;
        }
    }
}