using assetsmentCelsia.Data;
using assetsmentCelsia.Models;
using assetsmentCelsia.Services.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace assetsmentCelsia.Services.Repositories
{
    public class ExcelRepository : IExcelRepository
    {
        private readonly BaseContext _context;

        public ExcelRepository(BaseContext context)
        {
            _context = context;
        }

        // Users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUsersAsync(IEnumerable<User> users)
        {
            _context.AddRange(users);
            await _context.SaveChangesAsync();
        }

public async Task<IEnumerable<User>> ImportUsersFromExcelAsync(IFormFile file)
{
    var workbook = new XLWorkbook(file.OpenReadStream());
    var worksheet = workbook.Worksheet(1);
    var primeraFilaUsada = worksheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
    var ultimaFilaUsada = worksheet.LastRowUsed().RangeAddress.LastAddress.RowNumber;

    var users = new List<User>();

    for (int i = primeraFilaUsada + 1; i <= ultimaFilaUsada; i++)
    {
        var fila = worksheet.Row(i);
        var user = new User
        {
            Names = fila.Cell(6).GetValue<string>(),
            Document = fila.Cell(7).TryGetValue<int>(out int document) ? document : 0,
            Address = fila.Cell(8).GetValue<string>(),
            Phone = fila.Cell(9).GetValue<string>(),
            Email = fila.Cell(10).GetValue<string>(),
            Password = "defaultPassword"
        };

        var transaction = new Transaction
        {
            Id = fila.Cell(1).GetValue<string>(),
            DateTimeTransaction = fila.Cell(2).GetValue<DateTime>(),
            Amount = fila.Cell(3).GetValue<int>(),
            State = fila.Cell(4).GetValue<string>(),
            Type = fila.Cell(5).GetValue<string>(),
            Platform = new Platform
            {
                Name = fila.Cell(11).GetValue<string>()
            },
            Invoice = new Invoice
            {
                InvoiceNumber = fila.Cell(12).GetValue<string>(),
                PeriodInvoicing = fila.Cell(13).GetValue<string>(),
                InvoicedAmount = fila.Cell(14).GetValue<int>(),
                AmountPaid = fila.Cell(15).GetValue<int>()
            }
        };

        users.Add(user);
    }

    return users;
}



        public FileResult ExportUsersToExcel(string fileName, IEnumerable<User> users)
        {
            DataTable dataTable = new DataTable("Users");
            dataTable.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn("Names", typeof(string)),
                new DataColumn("Document", typeof(int)),
                new DataColumn("Address", typeof(string)),
                new DataColumn("Phone", typeof(string)),
                new DataColumn("Email", typeof(string)),
                new DataColumn("Password", typeof(string))
            });

            foreach (User user in users)
            {
                dataTable.Rows.Add(user.Names, user.Document, user.Address, user.Phone, user.Email, user.Password);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dataTable);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }

        // Invoices
        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task AddInvoicesAsync(IEnumerable<Invoice> invoices)
        {
            _context.AddRange(invoices);
            await _context.SaveChangesAsync();
        }

        

        public FileResult ExportInvoicesToExcel(string fileName, IEnumerable<Invoice> invoices)
        {
            DataTable dataTable = new DataTable("Invoices");
            dataTable.Columns.AddRange(new DataColumn[5]
            {
                new DataColumn("InvoiceNumber", typeof(string)),
                new DataColumn("PeriodInvoicing", typeof(string)),
                new DataColumn("InvoicedAmount", typeof(int)),
                new DataColumn("AmountPaid", typeof(int)),
                new DataColumn("UserId", typeof(int))
            });

            foreach (Invoice invoice in invoices)
            {
                dataTable.Rows.Add(invoice.InvoiceNumber, invoice.PeriodInvoicing, invoice.InvoicedAmount, invoice.AmountPaid, invoice.UserId);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dataTable);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }

        // Platforms
        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return await _context.Platforms.ToListAsync();
        }

        public async Task AddPlatformsAsync(IEnumerable<Platform> platforms)
        {
            _context.AddRange(platforms);
            await _context.SaveChangesAsync();
        }

        

        public FileResult ExportPlatformsToExcel(string fileName, IEnumerable<Platform> platforms)
        {
            DataTable dataTable = new DataTable("Platforms");
            dataTable.Columns.AddRange(new DataColumn[1]
            {
                new DataColumn("Name", typeof(string))
            });

            foreach (Platform platform in platforms)
            {
                dataTable.Rows.Add(platform.Name);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dataTable);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }

        // Transactions
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddTransactionsAsync(IEnumerable<Transaction> transactions)
        {
            _context.AddRange(transactions);
            await _context.SaveChangesAsync();
        }



        public FileResult ExportTransactionsToExcel(string fileName, IEnumerable<Transaction> transactions)
        {
            DataTable dataTable = new DataTable("Transactions");
            dataTable.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("Id", typeof(string)),
                new DataColumn("DateTimeTransaction", typeof(DateTime)),
                new DataColumn("Amount", typeof(int)),
                new DataColumn("State", typeof(string)),
                new DataColumn("Type", typeof(string)),
                new DataColumn("PlatformId", typeof(int)),
                new DataColumn("InvoiceId", typeof(int))
            });

            foreach (Transaction transaction in transactions)
            {
                dataTable.Rows.Add(transaction.Id, transaction.DateTimeTransaction, transaction.Amount, transaction.State, transaction.Type, transaction.PlatformId, transaction.InvoiceId);
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dataTable);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }
    }
}