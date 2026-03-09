using DashboardProject.Models;
using System.Threading.Tasks;
using System.Data;
namespace DashboardProject.iRepository
{




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
