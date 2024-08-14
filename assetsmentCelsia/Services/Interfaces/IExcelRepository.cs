using assetsmentCelsia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace assetsmentCelsia.Services.Interfaces
{
    public interface IExcelRepository
    {
        //User
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUsersAsync(IEnumerable<User> users);
        Task<IEnumerable<User>> ImportUsersFromExcelAsync(IFormFile file);
        FileResult ExportUsersToExcel(string fileName, IEnumerable<User> users);

        //Transaction
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task AddTransactionsAsync(IEnumerable<Transaction> transactions);
        FileResult ExportTransactionsToExcel(string fileName, IEnumerable<Transaction> transactions);

        //Platforms

        Task<IEnumerable<Platform>> GetAllPlatformsAsync();
        Task AddPlatformsAsync(IEnumerable<Platform> platforms);
        FileResult ExportPlatformsToExcel(string fileName, IEnumerable<Platform> platforms);

        
        //Invoices

        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task AddInvoicesAsync(IEnumerable<Invoice> invoices);
        FileResult ExportInvoicesToExcel(string fileName, IEnumerable<Invoice> invoices);

    }
}