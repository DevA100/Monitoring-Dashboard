//using Dapper;
//using DashboardProject.iRepository;
//using DashboardProject.Models;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using OfficeOpenXml;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
//using Microsoft.AspNetCore.Mvc;

//namespace DashboardProject.Repository
//{
//    public class DailyTransactionRepo : IDailyTransactionRepo
//    {
//        private readonly IConfiguration _configuration;

//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly ILogger<DailyTransactionRepo> _logger;


//        public DailyTransactionRepo(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<DailyTransactionRepo> logger)
//        {
//            _configuration = configuration;
//            _httpContextAccessor = httpContextAccessor;
//            BarResponse response = new BarResponse();
//            _logger = logger;
//        }

//        public async Task<object> AccountOpenedYesterdayBankwide()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            //var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select count (*) from VwAccMaster where prodcatCode in ('1','2') and DateAdded = @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//                ////int value = 0;
//                ////foreach (var item in result)
//                ////{
//                ////    value = item;
//                ////}
//                ////connection.Close();


//                ////using (var connection2 = new SqlConnection(constring2))
//                ////{
//                ////    connection2.Open();
//                ////    var updateparameters = new ParameterModel { Category = "DailyAccountOpened", Value = value };
//                ////    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                ////    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                ////}
//                ////return await Task.FromResult(result);
//            }
//        }

//        //public async Task<object> AtmAndTransferTransaction()
//        //{
//        //    var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//        //    var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//        //    var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//        //    var query = "select count (distinct TransID) from TransLines where transcode in ('551','552') and ValueDate = @ValueDate";
//        //    using (var connection = new SqlConnection(constring))
//        //    {
//        //        connection.Open();
//        //        var result = await connection.QueryAsync<int>(query, paramvalues);
//        //        int value = 0;
//        //        foreach (var item in result)
//        //        {
//        //            value = item;
//        //        }
//        //        connection.Close();


//        //        using (var connection2 = new SqlConnection(constring2))
//        //        {
//        //            connection2.Open();
//        //            var updateparameters = new ParameterModel { Category = "AtmAndTransferTransaction", Value = value };
//        //            var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//        //            var response2 = await connection2.QueryAsync(savequery, updateparameters);
//        //        }
//        //        return await Task.FromResult(result);
//        //    }
//        //}


//        public async Task<int> AtmAndTransferTransaction()
//        {
//            var constring = _configuration.GetConnectionString("DefaultConnection");
//            var constring2 = _configuration.GetConnectionString("DefaultConnectionBarChat");

//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "SELECT COUNT(DISTINCT TransID) FROM TransLines WHERE transcode IN ('551','552') AND ValueDate = @ValueDate";

//            int count = 0;

//            try
//            {
//                using (var connection = new SqlConnection(constring))
//                {
//                    count = await connection.ExecuteScalarAsync<int>(query, paramvalues);
//                }

//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    var updateparameters = new ParameterModel { Category = "AtmAndTransferTransaction", Value = count };
//                    var savequery = "UPDATE MonitorBarChat SET Value = @Value WHERE Category = @Category";
//                    await connection2.ExecuteAsync(savequery, updateparameters);
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log error and return -1 to indicate failure
//                Console.WriteLine("⚠ Error fetching ATM/Transfer transactions: " + ex.Message);
//                return -1;
//            }

//            return count;
//        }


//        public async Task<object> AtmAndTransferTransactionYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            //var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select count (distinct TransID) from TransHist where transcode in ('551','552') and ValueDate =  @ValueDate";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//                //int value = 0;
//                //foreach (var item in result)
//                //{
//                //    value = item;
//                //}
//                //connection.Close();


//                //using (var connection2 = new SqlConnection(constring2))
//                //{
//                //    connection2.Open();
//                //    var updateparameters = new ParameterModel { Category = "AtmAndTransferTransaction", Value = value };
//                //    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                //    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                //}
//                //return await Task.FromResult(result);
//            }
//        }

//        public async Task<object> GetDailyOpenedAccount()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count (*) from VwAccMaster where prodcatCode in ('1','2') and DateAdded = @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                int value = 0;
//                foreach (var item in result)
//                {
//                    value = item;
//                }
//                connection.Close();


//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    connection2.Open();
//                    var updateparameters = new ParameterModel { Category = "DailyAccountOpened", Value = value };
//                    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                }
//                return await Task.FromResult(result);
//            }
//        }

//        //abuja
//        public async Task<object> GetDailyOpenedAccountByAbuja()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=201 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //abuja yesterday
//        public async Task<object> GetDailyOpenedAccountByAbujaYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=201 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //account by AH
//        public async Task<object> GetDailyOpenedAccountByAdeolaHopewell()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=109 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //AH yesterday
//        public async Task<object> GetDailyOpenedAccountByAdeolaHopewellYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=109 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //camp
//        public async Task<object> GetDailyOpenedAccountByCamp()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=302 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        // camp yesterday
//        public async Task<object> GetDailyOpenedAccountByCampYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=302 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //ogba
//        public async Task<object> GetDailyOpenedAccountByOgba()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=107 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //ogba yesterday
//        public async Task<object> GetDailyOpenedAccountByOgbaYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=107 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //oko-oba
//        public async Task<object> GetDailyOpenedAccountByOkooba()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=104 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        //oko-oba yesterday
//        public async Task<object> GetDailyOpenedAccountByOkoobaYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select count(*) from VwAccMaster where ProdCatCode in (1,2) and BranchCode=104 and DateAdded= @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//public async Task<object> RemitaTransactionsYesterday()
//{
//    var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//    var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//    var query = "select count (distinct TransID) from TransHist where ValueDate = @DateAdded AND transcode=141";
//    using (var connection = new SqlConnection(constring))
//    {
//        connection.Open();
//        var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        public async Task<object> RemitaTransactions()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count (distinct TransID) from TransLines where ValueDate = @DateAdded AND transcode=141";

//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }
//        public async Task<object> RemitaVolumeYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select sum(credit) from TransHist where ValueDate = @DateAdded AND transcode=141";

//            if (query == "NULL")
//            {
//                return 0;
//            }
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        public async Task<object> RemitaVolume()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select sum(credit) from TransLines where ValueDate = @DateAdded AND transcode=141";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        public async Task<object> GetDailyTransactionCounts()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd")};
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            var query = "select count (distinct TransID) from TransLines where transcode in ('868','551','552') and ValueDate = @ValueDate";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                int value = 0;
//                foreach (var item in result)
//                {
//                    value = item;
//                }
//                connection.Close();
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    connection2.Open();
//                    var updateparameters = new ParameterModel { Category = "DailyTransaction", Value = value };
//                    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                }
//                return await Task.FromResult(result);
//            }
//        }

//        public async Task<BarResponse> GetValueByCategories()
//        {
//           BarResponse response = new BarResponse();

//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;

//            var query = "Select Category, Value from MonitorBarChat group by Category,Value";
//            //var query = "Select Category, COUNT(*) as count from IncidentForm group by Category";

//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();

//                var result = await connection.QueryAsync<BarResonseData>(query);

//                var data = result.ToList();

//                response.categories = data.Select(x => x.Category).ToArray();
//                response.Values = data.Select(x => x.Value).ToArray();

//                return response;
//            }
//        }

//        public async Task<object> GetYesterdayTransactionCounts()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            //var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            var query = "select count  (distinct transid) from TransHist where DateAdded = @DateAdded and transcode in ('868','551','552')";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//                //int value = 0;
//                //foreach (var item in result)
//                //{
//                //    value = item;
//                //}
//                //connection.Close();
//                //using (var connection2 = new SqlConnection(constring2))
//                //{
//                //    connection2.Open();
//                //    var updateparameters = new ParameterModel { Category = "DailyTransaction", Value = value };
//                //    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                //    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                //}
//                //return await Task.FromResult(result);
//            }
//        }

//        public async Task<object> InwardTransactionPerDay()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            //var query = "select count (Distinct TransID) from TransLines where transcode = '868' and ValueDate = @ValueDate and UserNarrative not like ('%CIB%')";
//            var query = "select count (credit) from VwTransLines where ValueDate = @ValueDate  and Credit <> '0.00' and transcode = '868' and AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430') and WorkstationAdded = 'System NIPinwards'";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                int value = 0;
//                foreach (var item in result)
//                {
//                    value = item;
//                }
//                connection.Close();
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    connection2.Open();
//                    var updateparameters = new ParameterModel { Category = "InwardTransaction", Value = value };
//                    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                }
//                return await Task.FromResult(result);
//            }
//        }

//        public async Task<object> InwardTransactionPerDayValue()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            //var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select sum (credit) from VwTransLines where ValueDate = @ValueDate  and Credit <> '0.00' and transcode = '868' and AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430') and WorkstationAdded = 'System NIPinwards'";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;

//            }
//        }

//        //public async Task<object> OutwardOne()
//        //{
//        //    var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionForOutward").Value;
//        //    //var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//        //    var query = "select count(distinct transaction_ref) from completed_accounts_transfer_transaction where request_time between '"+ DateTime.Now.Date.ToString("yyyy-MM-dd") + "' and '"+ DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd") + "'";
//        //    using (var connection = new SqlConnection(constring))
//        //    {
//        //        connection.Open();
//        //        var result = await connection.QueryAsync<int>(query);
//        //        return result;

//        //    }
//        //}

//        public async Task<object> OutwardTransactionPerDay()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            //var query = "select count (distinct OtherRefNo) from TransLines where UserNarrative like ('%CIB%') and ValueDate = @ValueDate";
//            var query = "select count (debit) from VwTransLines where ValueDate = @ValueDate and debit <> '0.00' and transcode = '868' and AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095') ";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query,paramvalues);
//                return result;
//            }
//        }

//        public async Task<object> OutwardTransactionYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            //var query = "select count (Debit) from TransHist where DateAdded = @DateAdded AND WorkstationAdded = 'System NIP' AND debit <> '0.00'";
//            var query = "select count (debit) from TransHist where ValueDate = @ValueDate and debit <> '0.00' and transcode = '868' and AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095') ";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        public async Task<object> ReversedTransactions()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            var query = "select   count (*) from TransLines where ValueDate = @ValueDate and UserNarrative like ('%JLMB-RVS%') and TransCode = '868' and credit <> '0.00'";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                int value = 0;
//                foreach (var item in result)
//                {
//                    value = item;
//                }
//                connection.Close();
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    connection2.Open();
//                    var updateparameters = new ParameterModel { Category = "ReversedTransaction", Value = value };
//                    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                }
//                return await Task.FromResult(result);
//            }
//        }

//        public async Task<object> SentMailPerDay()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            //var query = "Select count (*) from MessagesLog where DateAdded = @DateAdded AND emailsent = '1'";
//            var query = "select  count (*) from MessagesLog where EmailAddr <> '' and DateAdded = @DateAdded and EmailAddr <> 'itsupport@jubileelifeng.com'";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                int value = 0;
//                foreach (var item in result)
//                {
//                    value = item;
//                }
//                connection.Close();
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    connection2.Open();
//                    var updateparameters = new ParameterModel { Category = "EmailSent", Value = value };
//                    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                }                   
//                return await Task.FromResult(result);

//            }

//        }

//        public async Task<object> SentMailYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            //var query = "Select count (*) from MessagesLog where DateAdded = @DateAdded AND emailsent = '1'";
//            var query = "select  count (*) from MessagesLog where EmailAddr <> '' and DateAdded = @DateAdded and EmailAddr <> 'itsupport@jubileelifeng.com'";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;


//            }
//        }

//        public async Task<object> SentSMSPerDay()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//            //var query = "Select  count (*) from MessagesLog where DateAdded = @DateAdded AND msgemail_status = '0'";
//            var query = "select  count (*) from MessagesLog where PhoneNo <> '' and DateAdded = @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                int value = 0;
//                foreach (var item in result)
//                {
//                    value = item;
//                }
//                connection.Close();


//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    connection2.Open();
//                    var updateparameters = new ParameterModel { Category = "SMSsent", Value = value };
//                    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                }
//                return await Task.FromResult(result);
//            }
//        }

//        public async Task<object> SentSMSYesterday()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            //var query = "Select  count (*) from MessagesLog where DateAdded = @DateAdded AND msgemail_status = '0'";
//            var query = "select  count (*) from MessagesLog where PhoneNo <> '' and DateAdded = @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
//        }

//        public Task<object> Test()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<object> YesterdayInwardTransaction()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            //var query = "select count (Credit) from TransHist where DateAdded = @DateAdded AND WorkstationAdded = 'System NIPinwards' AND credit <> '0.00'";
//            var query = "select count (credit) from VwTransHist where ValueDate = @ValueDate and Credit <> '0.00' and transcode = '868' and AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430') ";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//            }
////        }

//public async Task<object> YesterdayReversedTransactions()
//{
//    var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//    // var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//    //var query = "select count (*) from TransHist where DateAdded = @DateAdded";
//    //var query = "select  count (*) from TransLines where ValueDate = @ValueDate and UserNarrative like ('%JLMB-RVS%') and credit <> '0.00'";
//            var query = "select  count (*) from TransHist where DateAdded = @DateAdded AND UserNarrative like ('%JLMB-RVS%') and TransCode = '868' AND Credit <> '0.0'";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;
//                //int value = 0;
//                //foreach (var item in result)
//                //{
//                //    value = item;
//                //}
//                //connection.Close();
//                //using (var connection2 = new SqlConnection(constring2))
//                //{
//                //    connection2.Open();
//                //    var updateparameters = new ParameterModel { Category = "ReversedTransaction", Value = value };
//                //    var savequery = "Update  MonitorBarChat set Value = @Value where Category = @Category";
//                //    var response2 = await connection2.QueryAsync(savequery, updateparameters);
//                //}
//                //return await Task.FromResult(result);
//            }
//        }

//        public async Task<object> YesterdayTransactionValue()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            //var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") };
//            var query = "select sum (credit) from VwTransHist where ValueDate = @ValueDate and Credit <> '0.00' and transcode = '868' and AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430') ";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;

//            }
//        }

//        public async Task<object> ViewAccountPostStatus()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
//            //var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count (transid) from VwAcctMaintDetails where MaintType = '03' and PostingDate = @DateAdded";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;

//            }
//        }


//        /*public async Task<object> AccountsOnboardedRetail()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionForOutward").Value;
//            //var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionBarChat").Value;
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd"), DateAddedPlus = DateTime.Now.Date.AddDays(+1).ToString("yyyy-MM-dd") };
//            var query = "select * from corporate_institution where time_added between @DateAdded and @DateAddedPlus";
//            using (var connection = new SqlConnection(constring))
//            {
//                connection.Open();
//                var result = await connection.QueryAsync<int>(query, paramvalues);
//                return result;

//            }
//        }*/


//        public async Task<object> SuccessfulTransactionToday()
//        {
//            // Retrieve connection strings from configuration
//            var constring1 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorDbConnection").Value;

//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorCorporateConnection").Value;

//            // Create an instance of ParameterModel with DateAdded as a formatted DateTime
//            var paramvalues = new ParameterModel
//            {
//                DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd")
//            };

//            // Define the query to count successful transactions
//            var query = "SELECT COUNT(*) FROM transfer WHERE transaction_status = 'successful' AND CONVERT(date, time_added) = @DateAdded";

//            // Initialize results for both databases
//            int result1 = 0;
//            int result2 = 0;

//            try
//            {
//                // Query the first database
//                using (var connection1 = new SqlConnection(constring1))
//                {
//                    await connection1.OpenAsync();
//                    result1 = await connection1.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Query the second database
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    await connection2.OpenAsync();
//                    result2 = await connection2.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Return the total of both results
//                var total = result1 + result2;
//                return total;
//            }
//            catch (Exception ex)
//            {

//                return new
//                {
//                    StatusCode = 500,
//                    Message = $"Internal server error: {ex.Message}"
//                };
//            }
//        }

//        public async Task<object> SuccessfulTransactionYesterday()
//        {
//            // Get connection strings
//            var constring1 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorDbConnection").Value;

//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorCorporateConnection").Value;


//            // Create an instance of ParameterModel with the date set to yesterday (as DateTime)
//            var paramvalues = new ParameterModel
//            {
//                DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") // Use DateTime for Yesterday's date
//            };

//            // Use parameterized query with @DateAdded
//            var query = "SELECT COUNT(*) FROM transfer WHERE transaction_status = 'successful' AND CONVERT(date, time_added) = @DateAdded";

//            // Initialize counts for both databases
//            int result1 = 0;
//            int result2 = 0;

//            try
//            {
//                // Query the first database
//                using (var connection1 = new SqlConnection(constring1))
//                {
//                    await connection1.OpenAsync();
//                    result1 = await connection1.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Query the second database
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    await connection2.OpenAsync();
//                    result2 = await connection2.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Return the total of both results
//                var total = result1 + result2;
//                return total;
//            }
//            catch (Exception ex)
//            {

//                return new
//                {
//                    StatusCode = 500,
//                    Message = $"Internal server error: {ex.Message}"
//                };
//            }
//        }



//        public async Task<object> FailedTransactionToday()
//        {
//            // Fetch the connection strings from configuration
//            var constring1 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorDbConnection").Value;

//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorCorporateConnection").Value;


//            // Set today's date in the parameter model (as a DateTime)
//            var paramvalues = new ParameterModel
//            {
//                DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd")  // Use DateTime for today's date
//            };

//            // Parameterized query using @DateAdded
//            var query = "SELECT COUNT(*) FROM transfer WHERE transaction_status = 'failed' AND CONVERT(date, time_added) = @DateAdded";

//            // Initialize counts for both databases
//            int result1 = 0;
//            int result2 = 0;

//            try
//            {
//                // Query the first database
//                using (var connection1 = new SqlConnection(constring1))
//                {
//                    await connection1.OpenAsync();
//                    result1 = await connection1.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Query the second database
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    await connection2.OpenAsync();
//                    result2 = await connection2.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Return the total of both results
//                var total = result1 + result2;
//                return total;
//            }
//            catch (Exception ex)
//            {

//                return new
//                {
//                    StatusCode = 500,
//                    Message = $"Internal server error: {ex.Message}"
//                };
//            }

//        }



//        public async Task<object> FailedTransactionYesterday()
//        {
//            // Fetch the connection strings from configuration
//            var constring1 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorDbConnection").Value;

//            var constring2 = _configuration.GetSection("ConnectionStrings").GetSection("MoneytorCorporateConnection").Value;

//            // Create an instance of ParameterModel with yesterday's date
//            var paramvalues = new ParameterModel
//            {
//                DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd")  // Use DateTime for yesterday's date
//            };

//            // Parameterized query using @DateAdded
//            var query = "SELECT COUNT(*) FROM transfer WHERE transaction_status = 'failed' AND CONVERT(date, time_added) = @DateAdded";

//            // Initialize counts for both databases
//            int result1 = 0;
//            int result2 = 0;

//            try
//            {
//                // Query the first database
//                using (var connection1 = new SqlConnection(constring1))
//                {
//                    await connection1.OpenAsync();
//                    result1 = await connection1.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Query the second database
//                using (var connection2 = new SqlConnection(constring2))
//                {
//                    await connection2.OpenAsync();
//                    result2 = await connection2.QueryFirstOrDefaultAsync<int>(query, paramvalues);
//                }

//                // Return the total of both results
//                var total = result1 + result2;
//                return total;
//            }
//            catch (Exception ex)
//            {

//                return new
//                {
//                    StatusCode = 500,
//                    Message = $"Internal server error: {ex.Message}"
//                };
//            }
//        }


//        public async Task<DataTable> RetrieveDataFromSQLAsync()
//        {
//            var constring = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value; // Replace with your connection string
//            var paramvalues = new ParameterModel { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") };
//            var query = "select count (transid) from VwAcctMaintDetails where MaintType = '03' and PostingDate = @DateAdded"; // Replace with your SQL query

//            DataTable dt = new DataTable();

//            using (SqlConnection con = new SqlConnection(constring))
//            {
//                using (SqlCommand cmd = new SqlCommand(query, con))
//                {
//                    await con.OpenAsync();
//                    cmd.Parameters.AddWithValue("@DateAdded", paramvalues.DateAdded);
//                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
//                    dt.Load(reader);
//                }
//            }

//            return dt;
//        }

//        public async Task<object> DownloadSqlDataAsExcel()
//        {
//            // Retrieve SQL data and store it in a DataTable
//            DataTable dt = await RetrieveDataFromSQLAsync();

//            // Create an Excel package using EPPlus
//            using (ExcelPackage package = new ExcelPackage())
//            {
//                // Create a worksheet
//                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customers");

//                // Populate the worksheet with SQL data
//                worksheet.Cells.LoadFromDataTable(dt, true);

//                // Set response headers for Excel file download
//                HttpContext context = _httpContextAccessor.HttpContext;
//                context.Response.Clear();
//                //context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                //context.Response.Headers.Add("content-disposition", "attachment;filename=SqlExport.xlsx");

//                // Write the Excel package to the response output stream
//                using (MemoryStream stream = new MemoryStream())
//                {
//                    package.SaveAs(stream);
//                    stream.Position = 0;
//                    await stream.CopyToAsync(context.Response.Body);
//                }

//                // Flush the response
//                await context.Response.Body.FlushAsync();
//            }

//            return null; // Return an object or modify the return type as needed
//}

//    }
//}


















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

        // helper: read connection string by name (wrapped for convenience)
        private string GetConnectionString(string name = "DefaultConnection")
        {
            // prefer GetConnectionString helper (works with "ConnectionStrings": { "DefaultConnection": "..." })
            var cs = _configuration.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(cs))
            {
                // fallback to GetSection path in case config is structured differently
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
                          WHERE ProdCatCode IN ('1','2') 
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
                          WHERE ProdCatCode IN ('1','2') 
                            AND DateAdded = @DateAdded";

            int value;
            using (var connection = new SqlConnection(constring))
            {
                value = await connection.ExecuteScalarAsync<int>(query, new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") });
            }

            // update bar chat DB if available
            if (!string.IsNullOrWhiteSpace(constring2))
            {
                using var connection2 = new SqlConnection(constring2);
                var savequery = "UPDATE MonitorBarChat SET Value = @Value WHERE Category = @Category";
                await connection2.ExecuteAsync(savequery, new { Category = "DailyAccountOpened", Value = value });
            }

            return value;
        }

        // Branch-specific helpers (branch codes from your SQL; adjust codes if they differ)
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

        public Task<int> GetDailyOpenedAccountByAdeolaHopewell() => GetBranchAccountOpened(109 /*use 109 if that's actual branch code in your DB*/, DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByOgba() => GetBranchAccountOpened(107, DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByCamp() => GetBranchAccountOpened(302, DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByAbuja() => GetBranchAccountOpened(201, DateTime.Now.Date);
        public Task<int> GetDailyOpenedAccountByOkooba() => GetBranchAccountOpened(104, DateTime.Now.Date);

        public Task<int> GetDailyOpenedAccountByAdeolaHopewellYesterday() => GetBranchAccountOpened(109, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByOgbaYesterday() => GetBranchAccountOpened(107, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByCampYesterday() => GetBranchAccountOpened(302, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByAbujaYesterday() => GetBranchAccountOpened(201, DateTime.Now.Date.AddDays(-1));
        public Task<int> GetDailyOpenedAccountByOkoobaYesterday() => GetBranchAccountOpened(104, DateTime.Now.Date.AddDays(-1));


        //echannel account opened

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

        // ---------- ATM & Transfer ----------
        public async Task<int> AtmAndTransferTransaction()
        {
            var constring = GetConnectionString("DefaultConnection");
            var constring2 = GetConnectionString("DefaultConnectionBarChat");

            var query = @"SELECT COUNT(DISTINCT TransID) 
                          FROM TransLines 
                          WHERE TransCode IN ('551','552') 
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
                          WHERE TransCode IN ('551','552') 
                            AND ValueDate = @ValueDate";
            using var conn = new SqlConnection(constring);
            return await conn.ExecuteScalarAsync<int>(query, new { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        // ---------- Remita (counts & volume) ----------
        public async Task<int> RemitaTransactionsYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransHist WHERE ValueDate = @DateAdded AND TransCode = 141";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        public async Task<int> RemitaTransactions()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransLines WHERE ValueDate = @DateAdded AND TransCode = 141";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<string> RemitaVolumeYesterday()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT SUM(Credit) FROM TransHist WHERE ValueDate = @DateAdded AND TransCode = 141";
            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query,
                new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); // ✅ Adds comma formatting
        }

        public async Task<string> RemitaVolume()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT SUM(Credit) FROM TransLines WHERE ValueDate = @DateAdded AND TransCode = 141";
            using var connection = new SqlConnection(constring);
            
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query,
                new { DateAdded = DateTime.Now.Date.ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); // ✅ Adds comma formatting
        }


        // ---------- Daily transaction counts ----------
        public async Task<int> GetDailyTransactionCounts()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransLines WHERE TransCode IN ('868','551','552') AND ValueDate = @ValueDate";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<int> GetYesterdayTransactionCounts()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = "SELECT COUNT(DISTINCT TransID) FROM TransHist WHERE DateAdded = @DateAdded AND TransCode IN ('868','551','552')";
            using var connection = new SqlConnection(constring);
            return await connection.QuerySingleOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        // ---------- Inward / Outward / Reversed / Messages ----------
        public async Task<int> InwardTransactionPerDay()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(Credit) 
                          FROM VwTransLines 
                          WHERE ValueDate = @ValueDate  
                            AND Credit <> '0.00' 
                            AND TransCode = '868' 
                            AND AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430') 
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
                    AND TransCode = '868' 
                    AND AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430') 
                    AND WorkstationAdded = 'System NIPinwards'";
            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query, new { ValueDate = DateTime.Now.Date.ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); // ✅ Adds comma formatting
        }

        public async Task<string> YesterdayTransactionValue()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT SUM(CAST(Credit AS decimal(18,2))) 
                  FROM VwTransHist 
                  WHERE ValueDate = @ValueDate 
                    AND Credit <> '0.00' 
                    AND TransCode = '868' 
                    AND AccountNo NOT IN (
                        '10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430'
                    )";
            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(query, new { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") }) ?? 0m;
            return result.ToString("N2"); // ✅ Adds comma formatting
        }

        public async Task<int> OutwardTransactionPerDay()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(Debit) 
                          FROM VwTransLines 
                          WHERE ValueDate = @ValueDate  
                            AND Debit <> '0.00' 
                            AND TransCode = '868' 
                            AND AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095')";
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
                            AND TransCode = '868' 
                            AND AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095')";
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
                    AND TransCode = '868' 
                    AND AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095')";

            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(
                query,
                new { ValueDate = DateTime.Now.Date }  // ✅ keep as DateTime, not string
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
                    AND TransCode = '868' 
                    AND AccountNo NOT IN ('10010500130','10020300050','10020350577','10020350576','10040150095')";

            using var connection = new SqlConnection(constring);
            var result = await connection.QuerySingleOrDefaultAsync<decimal?>(
                query,
                new { ValueDate = DateTime.Now.Date.AddDays(-1) }  // ✅ keep as DateTime
            ) ?? 0m;

            return result.ToString("N2");
        }





        public async Task<int> ReversedTransactions()
        {
            var constring = GetConnectionString("DefaultConnection");
            var query = @"SELECT COUNT(*) 
                          FROM TransLines 
                          WHERE ValueDate = @ValueDate 
                            AND UserNarrative LIKE ('%JLMB-RVS%') 
                            AND TransCode = '868' 
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
                            AND EmailAddr <> 'itsupport@jubileelifeng.com'";
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
                            AND EmailAddr <> 'itsupport@jubileelifeng.com'";
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
                            AND TransCode = '868' 
                            AND AccountNo NOT IN (
                                '10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430'
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
                            AND UserNarrative LIKE ('%JLMB-RVS%') 
                            AND TransCode = '868' 
                            AND Credit <> '0.0'";
            using var connection = new SqlConnection(constring);
            return await connection.QueryFirstOrDefaultAsync<int>(query, new { DateAdded = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") });
        }

        //public async Task<decimal> YesterdayTransactionValue()
        //{
        //    var constring = GetConnectionString("DefaultConnection");
        //    var query = @"SELECT SUM(CAST(Credit AS decimal(18,2))) 
        //                  FROM VwTransHist 
        //                  WHERE ValueDate = @ValueDate 
        //                    AND Credit <> '0.00' 
        //                    AND TransCode = '868' 
        //                    AND AccountNo NOT IN (
        //                        '10010500130','10020300050','10020350577','10020350576','10040150095','1001010007000011','10420350430','10020350430'
        //                    )";
        //    using var connection = new SqlConnection(constring);
        //    return await connection.QuerySingleOrDefaultAsync<decimal?>(query, new { ValueDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") }) ?? 0m;
        //}

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

        // ---------- Moneytor transfers (two DBs) ----------
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
