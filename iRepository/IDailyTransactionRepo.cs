using DashboardProject.Models;
using System.Threading.Tasks;
using System.Data;
namespace DashboardProject.iRepository
{
    //public interface IDailyTransactionRepo
    //{
    //    public Task<object> GetDailyTransactionCounts();
    //    public Task<object> GetYesterdayTransactionCounts();
    //    public Task<object> GetDailyOpenedAccount();
    //    public Task<object> AccountOpenedYesterdayBankwide();
    //    public Task<object> GetDailyOpenedAccountByAdeolaHopewell();
    //    public Task<object> GetDailyOpenedAccountByOgba();
    //    public Task<object> GetDailyOpenedAccountByCamp();
    //    public Task<object> GetDailyOpenedAccountByAbuja();
    //    public Task<object> GetDailyOpenedAccountByOkooba();
    //    public Task<object> GetDailyOpenedAccountByAdeolaHopewellYesterday();
    //    public Task<object> GetDailyOpenedAccountByOgbaYesterday();
    //    public Task<object> GetDailyOpenedAccountByCampYesterday();
    //    public Task<object> GetDailyOpenedAccountByAbujaYesterday();
    //    public Task<object> GetDailyOpenedAccountByOkoobaYesterday();
    //    public Task<object> InwardTransactionPerDay();
    //    public Task<object> YesterdayInwardTransaction();
    //    public Task<object> InwardTransactionPerDayValue();
    //    public Task<object> YesterdayTransactionValue();
    //    public Task<object> OutwardTransactionPerDay();
    //    public Task<object> OutwardTransactionYesterday();
    //    public Task<object> ReversedTransactions();
    //    public Task<object> YesterdayReversedTransactions();
    //    public Task<object> SentMailPerDay();
    //    public Task<object> SentMailYesterday();
    //    public Task<object> SentSMSPerDay();
    //    public Task<object> SentSMSYesterday();
    //    public Task<int> AtmAndTransferTransaction();
    //    public Task<object> AtmAndTransferTransactionYesterday();
    //    public Task<object> ViewAccountPostStatus();
    //    public Task<object> DownloadSqlDataAsExcel();
    //    //public Task<object> AccountsOnboardedRetail();
    //    public Task<object> RemitaTransactionsYesterday();
    //    public Task<object> RemitaVolumeYesterday();
    //    public Task<object> RemitaTransactions();
    //    public Task<object> RemitaVolume();
    //    public Task<object> SuccessfulTransactionToday();
    //    public Task<object> SuccessfulTransactionYesterday();

    //    public Task<object> FailedTransactionToday();
    //    public Task<object> FailedTransactionYesterday();
    //    //public Task<object> AccountsOnboardedRetail(
    //    public Task<object> Test();
    //    //public Task<bool> TestConnectionAsync();
    //    Task<BarResponse> GetValueByCategories();
    //}



    public interface IDailyTransactionRepo
    {
        // Counts (int)
        Task<int> GetDailyTransactionCounts();
        Task<int> GetYesterdayTransactionCounts();
        Task<int> GetDailyOpenedAccount();
        Task<int> AccountOpenedYesterdayBankwide();
        Task<int> GetDailyOpenedAccountByAdeolaHopewell();
        Task<int> GetDailyOpenedAccountByOgba();
        Task<int> GetDailyOpenedAccountByCamp();
        Task<int> GetDailyOpenedAccountByAbuja();
        Task<int> GetDailyOpenedAccountByOkooba();
        Task<int> GetDailyOpenedAccountByAdeolaHopewellYesterday();
        Task<int> GetDailyOpenedAccountByOgbaYesterday();
        Task<int> GetDailyOpenedAccountByCampYesterday();
        Task<int> GetDailyOpenedAccountByAbujaYesterday();
        Task<int> GetDailyOpenedAccountByOkoobaYesterday();
        Task<int> GetDailyOpenedAccountByEchannelToday();
        Task<int> GetDailyOpenedAccountByEchannelYesterday();
        Task<int> InwardTransactionPerDay();
        Task<int> YesterdayInwardTransaction();
        Task<string> InwardTransactionPerDayValue();
        Task<string> YesterdayTransactionValue();
        Task<int> OutwardTransactionPerDay();
        Task<int> OutwardTransactionYesterday();
        Task<string> OutwardTransactionPerDayValue();
        Task<string> OutwardTransactionPerDayValueYesterday();
        Task<int> ReversedTransactions();
        Task<int> YesterdayReversedTransactions();
        Task<int> SentMailPerDay();
        Task<int> SentMailYesterday();
        Task<int> SentSMSPerDay();
        Task<int> SentSMSYesterday();
        Task<int> AtmAndTransferTransaction();
        Task<int> AtmAndTransferTransactionYesterday();
        Task<int> ViewAccountPostStatus();
        Task<DataTable> RetrieveDataFromSQLAsync();
        Task<object> DownloadSqlDataAsExcel();
        Task<int> RemitaTransactionsYesterday();
        Task<string> RemitaVolumeYesterday();
        Task<int> RemitaTransactions();
        Task<string> RemitaVolume();
        Task<int> SuccessfulTransactionToday();
        Task<int> SuccessfulTransactionYesterday();
        Task<int> FailedTransactionToday();
        Task<int> FailedTransactionYesterday();
        Task<BarResponse> GetValueByCategories();
    }







}
