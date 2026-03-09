
using Dapper;
using DashboardProject.iRepository;
using DashboardProject.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DashboardProject.Repository
{
    public class DailyTransactionRepo : IDailyTransactionRepo
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DailyTransactionRepo> _logger;

        public DailyTransactionRepo(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ILogger<DailyTransactionRepo> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private string GetConnectionString(string name = "DefaultConnection")
        {
            var cs = _configuration.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(cs))
            {
                cs = _configuration.GetSection("ConnectionStrings").GetSection(name).Value;
            }
            return cs;
        }

        // ---------- Accounts opened (counts) ----------
        public async Task<int> AccountOpenedYesterdayBankwide()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM VwAccMaster 
                          WHERE ProdCatCode IN ('0','0') 
                            AND DateAdded = @DateAdded";
            using var conn = new SqlConnection(constring);
            return await conn.ExecuteScalarAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> GetDailyOpenedAccount()
        {
            var constring = GetConnectionString("DefaultConnection");
            var constring2 = GetConnectionString("DefaultConnectionBarChat");

            var query = @"SELECT COUNT(*) 
                          FROM VwAccMaster 
                          WHERE ProdCatCode IN ('0','0') 
                            AND DateAdded = @DateAdded";

            int value;
            using (var connection = new SqlConnection(constring))
            {
                value = await connection.ExecuteScalarAsync<int>(query, new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") });
            }

            if (!string.IsNullOrWhiteSpace(constring2))
            {
                using var connection2 = new SqlConnection(constring2);
                var savequery = "UPDATE MonitorBarChat SET Value = @Value WHERE Category = @Category";
                await connection2.ExecuteAsync(savequery, new { Category = "DailyAccountOpened", Value = value });
            }

            return value;
        }

        private async Task<int> GetBranchAccountOpened(int branchCode, DateTime date)
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM VwAccMaster 
                          WHERE ProdCatCode IN (1,2) 
                            AND BranchCode = @BranchCode 
                            AND DateAdded = @DateAdded";
            using var conn = new SqlConnection(constring);
            return await conn.ExecuteScalarAsync<int>(query, new { BranchCode = branchCode, DateAdded = date.ToString("yyyy-MM-dd") });
        }

        public Task<int> GetDailyOpenedAccountByAdeolaHopewell() => GetBranchAccountOpened(109 , DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByOgba() => GetBranchAccountOpened(00, DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByCamp() => GetBranchAccountOpened(00, DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByAbuja() => GetBranchAccountOpened(00, DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByOkooba() => GetBranchAccountOpened(00, DateTime.Now.Date);

        public Task<int> GetDailyOpenedAccountByAdeolaHopewellYesterday() => GetBranchAccountOpened(00, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByOgbaYesterday() => GetBranchAccountOpened(00, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByCampYesterday() => GetBranchAccountOpened(00, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByAbujaYesterday() => GetBranchAccountOpened(00, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByOkoobaYesterday() => GetBranchAccountOpened(00, DateTime.Now.Date.AddDays(-1));



        private async Task<int> GetEChannelAccountsOpened(DateTime date)
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*)
                  FROM VwAccMaster 
                  WHERE AddedBy IN ('eChannels', 'Trans')
                  AND CAST(DateAdded AS DATE) = @DateAdded";

            using var conn = new SqlConnection(constring);
            return await conn.ExecuteScalarAsync<int>(query, new { DateAdded = date });
        }

        public Task<int> GetDailyOpenedAccountByEchannelToday()
            => GetEChannelAccountsOpened(DateTime.Now.Date);

        public Task<int> GetDailyOpenedAccountByEchannelYesterday()
            => GetEChannelAccountsOpened(DateTime.Now.Date.AddDays(-1));

        public async Task<int> AtmAndTransferTransaction()
        {
            var constring = GetConnectionString("DefaultConnection");
            var constring2 = GetConnectionString("DefaultConnectionBarChat");

            var query = @"SELECT COUNT(DISTINCT TransID) 
                          FROM TransLines 
                          WHERE TransCode IN ('00','00') 
                            AND ValueDate = @ValueDate";

            try
            {
                int count;
                using (var connection = new SqlConnection(constring))
                {
                    count = await connection.ExecuteScalarAsync<int>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") });
                }

                if (!string.IsNullOrWhiteSpace(constring2))
                {
                    using var connection2 = new SqlConnection(constring2);
                    var savequery = "UPDATE MonitorBarChat SET Value = @Value WHERE Category = @Category";
                    await connection2.ExecuteAsync(savequery, new { Category = "AtmAndTransferTransaction", Value = count });
                }

                return count;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error while fetching AtmAndTransferTransaction");
                return -1;
            }
        }

        public async Task<int> AtmAndTransferTransactionYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(DISTINCT TransID) 
                          FROM TransHist 
                          WHERE TransCode IN ('00','00') 
                            AND ValueDate = @ValueDate";
            using var conn = new SqlConnection(constring);
            return await conn.ExecuteScalarAsync<int>(query, new { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> RemitaTransactionsYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransHist WHERE ValueDate = @DateAdded AND TransCode = 00";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> RemitaTransactions()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransLines WHERE ValueDate = @DateAdded AND TransCode = 00";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<string> RemitaVolumeYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT SUM(Credit) FROM TransHist WHERE ValueDate = @DateAdded AND TransCode = 00";
            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query,
                new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); 
        }

        public async Task<string> RemitaVolume()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT SUM(Credit) FROM TransLines WHERE ValueDate = @DateAdded AND TransCode = 00";
            using var connection = new SqlConnection(constring);
            
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query,
                new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); 
        }


        public async Task<int> GetDailyTransactionCounts()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransLines WHERE TransCode IN ('00','00','00') AND ValueDate = @ValueDate";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<int> GetYesterdayTransactionCounts()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransHist WHERE DateAdded = @DateAdded AND TransCode IN ('00','00','00')";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> InwardTransactionPerDay()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(Credit) 
                          FROM VwTransLines 
                          WHERE ValueDate = @ValueDate  
                            AND Credit <> '0.00' 
                            AND TransCode = '00' 
                            AND AccountNo NOT IN ('000','00','00','00','00','00','00','00') 
                            AND WorkstationAdded = 'System NIPinwards'";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<string> InwardTransactionPerDayValue()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT SUM(CAST(Credit AS decimal(18,2))) 
                  FROM VwTransLines 
                  WHERE ValueDate = @ValueDate  
                    AND Credit <> '0.00' 
                    AND TransCode = '00' 
                    AND AccountNo NOT IN ('00','00','00','00','00','00','00','00') 
                    AND WorkstationAdded = 'System NIPinwards'";
            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); 
        }

        public async Task<string> YesterdayTransactionValue()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT SUM(CAST(Credit AS decimal(18,2))) 
                  FROM VwTransHist 
                  WHERE ValueDate = @ValueDate 
                    AND Credit <> '0.00' 
                    AND TransCode = '00' 
                    AND AccountNo NOT IN (
                        '00','00','00','00','00','00','00','00'
                    )";
            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query, new { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); 
        }

        public async Task<int> OutwardTransactionPerDay()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(Debit) 
                          FROM VwTransLines 
                          WHERE ValueDate = @ValueDate  
                            AND Debit <> '0.00' 
                            AND TransCode = '00' 
                            AND AccountNo NOT IN ('00','00','00','00','00')";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<int> OutwardTransactionYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(Debit) 
                          FROM TransHist 
                          WHERE ValueDate = @ValueDate  
                            AND Debit <> '0.00' 
                            AND TransCode = '00' 
                            AND AccountNo NOT IN ('00','00','00','00','00')";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }


        public async Task<string> OutwardTransactionPerDayValue()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT SUM(Debit) 
                  FROM VwTransLines 
                  WHERE CAST(ValueDate AS DATE) = @ValueDate  
                    AND Debit <> 0 
                    AND TransCode = '00' 
                    AND AccountNo NOT IN ('00','00','00','00','00')";

            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(
                query,
                new { ValueDate = DateTime.Now.Date } 
            ) ?? 0m;

            return result.ToString("N2");
        }

        public async Task<string> OutwardTransactionPerDayValueYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT SUM(Debit) 
                  FROM TransHist 
                  WHERE CAST(ValueDate AS DATE) = @ValueDate  
                    AND Debit <> 0 
                    AND TransCode = '00' 
                    AND AccountNo NOT IN ('00','00','00','00','00')";

            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(
                query,
                new { ValueDate = DateTime.Now.Date.AddDays(-1) }  
            ) ?? 0m;

            return result.ToString("N2");
        }





        public async Task<int> ReversedTransactions()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM TransLines 
                          WHERE ValueDate = @ValueDate 
                            AND UserNarrative LIKE ('%RVS%') 
                            AND TransCode = '00' 
                            AND Credit <> '0.00'";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<int> SentMailPerDay()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM MessagesLog 
                          WHERE EmailAddr <> '' 
                            AND DateAdded = @DateAdded 
                            AND EmailAddr <> 'email'";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<int> SentMailYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM MessagesLog 
                          WHERE EmailAddr <> '' 
                            AND DateAdded = @DateAdded 
                            AND EmailAddr <> 'email'";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> SentSMSPerDay()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM MessagesLog 
                          WHERE PhoneNo <> '' 
                            AND DateAdded = @DateAdded";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<int> SentSMSYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM MessagesLog 
                          WHERE PhoneNo <> '' 
                            AND DateAdded = @DateAdded";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> YesterdayInwardTransaction()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(Credit) 
                          FROM VwTransHist 
                          WHERE ValueDate = @ValueDate 
                            AND Credit <> '0.00' 
                            AND TransCode = '00' 
                            AND AccountNo NOT IN (
                                '00','00','00','00','00','00','00','00'
                            )";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> YesterdayReversedTransactions()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM TransHist 
                          WHERE DateAdded = @DateAdded 
                            AND UserNarrative LIKE ('%RVS%') 
                            AND TransCode = '00' 
                            AND Credit <> '0.0'";
            using var connection = new SqlConnection(constring);
            return await connection.QueryFirstOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> ViewAccountPostStatus()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(transid) 
                          FROM VwAcctMaintDetails 
                          WHERE MaintType = '03' 
                            AND PostingDate = @DateAdded";
            using var connection = new SqlConnection(constring);
            return await connection.QueryFirstOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<int> SuccessfulTransactionToday()
        {
            var constring1 = GetConnectionString("MoneytorDbConnection");
            var constring2 = GetConnectionString("MoneytorCorporateConnection");
            var query = @"SELECT COUNT(*) FROM transfer WHERE transaction_status = 'successful' AND CONVERT(date, time_added) = @DateAdded";
            var param = new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };

            int result1 = 0, result2 = 0;
            using (var c1 = new SqlConnection(constring1))
            {
                await c1.OpenAsync();
                result1 = await c1.QueryFirstOrDefaultAsync<int>(query, param);
            }
            using (var c2 = new SqlConnection(constring2))
            {
                await c2.OpenAsync();
                result2 = await c2.QueryFirstOrDefaultAsync<int>(query, param);
            }
            return result1 + result2;
        }

        public async Task<int> SuccessfulTransactionYesterday()
        {
            var constring1 = GetConnectionString("MoneytorDbConnection");
            var constring2 = GetConnectionString("MoneytorCorporateConnection");
            var query = @"SELECT COUNT(*) FROM transfer WHERE transaction_status = 'successful' AND CONVERT(date, time_added) = @DateAdded";
            var param = new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };

            int result1 = 0, result2 = 0;
            using (var c1 = new SqlConnection(constring1))
            {
                await c1.OpenAsync();
                result1 = await c1.QueryFirstOrDefaultAsync<int>(query, param);
            }
            using (var c2 = new SqlConnection(constring2))
            {
                await c2.OpenAsync();
                result2 = await c2.QueryFirstOrDefaultAsync<int>(query, param);
            }
            return result1 + result2;
        }

        public async Task<int> FailedTransactionToday()
        {
            var constring1 = GetConnectionString("MoneytorDbConnection");
            var constring2 = GetConnectionString("MoneytorCorporateConnection");
            var query = @"SELECT COUNT(*) FROM transfer WHERE transaction_status = 'failed' AND CONVERT(date, time_added) = @DateAdded";
            var param = new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };

            int result1 = 0, result2 = 0;
            using (var c1 = new SqlConnection(constring1))
            {
                await c1.OpenAsync();
                result1 = await c1.QueryFirstOrDefaultAsync<int>(query, param);
            }
            using (var c2 = new SqlConnection(constring2))
            {
                await c2.OpenAsync();
                result2 = await c2.QueryFirstOrDefaultAsync<int>(query, param);
            }
            return result1 + result2;
        }

        public async Task<int> FailedTransactionYesterday()
        {
            var constring1 = GetConnectionString("MoneytorDbConnection");
            var constring2 = GetConnectionString("MoneytorCorporateConnection");
            var query = @"SELECT COUNT(*) FROM transfer WHERE transaction_status = 'failed' AND CONVERT(date, time_added) = @DateAdded";
            var param = new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };

            int result1 = 0, result2 = 0;
            using (var c1 = new SqlConnection(constring1))
            {
                await c1.OpenAsync();
                result1 = await c1.QueryFirstOrDefaultAsync<int>(query, param);
            }
            using (var c2 = new SqlConnection(constring2))
            {
                await c2.OpenAsync();
                result2 = await c2.QueryFirstOrDefaultAsync<int>(query, param);
            }
            return result1 + result2;
        }

        // ---------- Bar chart data ----------
        public async Task<BarResponse> GetValueByCategories()
        {
            var constring = GetConnectionString("DefaultConnectionBarChat");
            var query = "SELECT Category, Value FROM MonitorBarChat GROUP BY Category, Value";

            var response = new BarResponse();
            using var connection = new SqlConnection(constring);
            var result = await connection.QueryAsync<BarResonseData>(query);
            var data = result.ToList();

            response.categories = data.Select(x => x.Category).ToArray();
            response.Values = data.Select(x => x.Value).ToArray();
            return response;
        }

        // ---------- Retrieve DataTable and download Excel ----------
        public async Task<DataTable> RetrieveDataFromSQLAsync()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(transid) as CountTransId
                          FROM VwAcctMaintDetails
                          WHERE MaintType = '03' AND PostingDate = @DateAdded";

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constring))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                await con.OpenAsync();
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now.Date.ToString("yyyy-MM-dd"));
                using var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
            }
            return dt;
        }

        public async Task<object> DownloadSqlDataAsExcel()
        {
            DataTable dt = await RetrieveDataFromSQLAsync();

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customers");
                worksheet.Cells.LoadFromDataTable(dt, true);

                HttpContext context = _httpContextAccessor.HttpContext;
                context.Response.Clear();

                using (MemoryStream stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    stream.Position = 0;
                    await stream.CopyToAsync(context.Response.Body);
                }

                await context.Response.Body.FlushAsync();
            }

            return null;
        }
    }
}
