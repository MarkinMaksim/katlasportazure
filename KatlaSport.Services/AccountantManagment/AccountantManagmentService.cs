using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.AccountantCatalogue;
using Microsoft.WindowsAzure.Storage.Blob;

namespace KatlaSport.Services.AccountantManagment
{
    internal class AccountantManagmentService : IAccountantService
    {
        IAccountantContext _context;

        public AccountantManagmentService(IAccountantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<AccountantRequest>> GetAccountantsAsync()
        {
            var dbaccountants = await _context.Accountants.OrderBy(h => h.Id).ToListAsync();
            var accountants = dbaccountants.Select(h => Mapper.Map<Accountant, AccountantRequest>(h)).ToList();

            return accountants;
        }

        public async Task<AccountantRequest> GetAccountantAsync(int accountantid)
        {
            var dbaccountants = await _context.Accountants.Where(h => h.Id == accountantid).ToArrayAsync();
            if (dbaccountants.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var accountants = Mapper.Map<Accountant, AccountantRequest>(dbaccountants[0]);
            return accountants;
        }

        public async Task<Accountant> CreateAccountantAsync(AccountantRequest accountantrequest, CloudBlobContainer blobContainer)
        {
            var dbAccountant = await _context.Accountants.Where(h => (h.Surname == accountantrequest.Surname && h.Name == accountantrequest.Name)).ToArrayAsync();
            if (dbAccountant.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            /*
            var fileName = GetRandomBlobName(file[0].FileName);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
            using (var fileStream = file[0].InputStream)
            {
                await blob.UploadFromStreamAsync(fileStream);
            }

            accountant.Photo = blob.Uri.ToString();
            */

            var accountant = Mapper.Map<AccountantRequest, Accountant>(accountantrequest);
            _context.Accountants.Add(accountant);

            await _context.SaveChangesAsync();

            return accountant;
        }

        public async Task<Accountant> UpdateAccountantAsync(int accountantId, AccountantRequest updateAccountant, CloudBlobContainer blobContainer)
        {
            var dbAccountants = await _context.Accountants.Where(p => p.Name == updateAccountant.Name && p.Id != accountantId).ToArrayAsync();
            if (dbAccountants.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            dbAccountants = _context.Accountants.Where(p => p.Id == accountantId).ToArray();
            if (dbAccountants.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbaccountant = dbAccountants[0];

            dbaccountant.Name = updateAccountant.Name;
            dbaccountant.Surname = updateAccountant.Surname;
            dbaccountant.Age = updateAccountant.Age;
            dbaccountant.ChiefId = updateAccountant.ChiefId;
            await _context.SaveChangesAsync();

            return dbaccountant;
        }

        public async Task DeleteAccaountantAsync(int accountantId)
        {
            var dbAccountants = await _context.Accountants.Where(p => p.Id != accountantId).ToArrayAsync();
            if (dbAccountants.Length == 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            _context.Accountants.Remove(dbAccountants[0]);
            await _context.SaveChangesAsync();
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
