﻿using KatlaSport.DataAccess.AccountantCatalogue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.AccountantManagment
{
    /// <summary>
    /// Represents a product category service.
    /// </summary>
    public interface IAccountantService
    {
        Task<List<AccountantRequest>> GetAccountantsAsync();

        Task<AccountantRequest> GetAccountantAsync(int accountantid);

        Task<Accountant> CreateAccountantAsync(AccountantRequest accountant, CloudBlobContainer blobContainer);

        Task<Accountant> UpdateAccountantAsync(int accountantId, AccountantRequest updateAccountant, CloudBlobContainer blobContainer);

        Task DeleteAccaountantAsync(int accountantId);
    }
}
