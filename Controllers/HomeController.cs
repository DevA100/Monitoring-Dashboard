using DashboardProject.iRepository;
using DashboardProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDailyTransactionRepo _dailyTransactionRepo;

        public HomeController(ILogger<HomeController> logger, IDailyTransactionRepo dailyTransactionRepo)
        {
            _logger = logger;
            _dailyTransactionRepo = dailyTransactionRepo;
        }

        public IActionResult Index()
        {           
            return View();
        }

        //Daily Transaction
        [HttpGet]
        public async Task<object> GetDailyTransactionCount()
        {
            var response = await _dailyTransactionRepo.GetDailyTransactionCounts();
            return response;
        }

        //Daily Transaction yesterday
        [HttpGet]
        public async Task<object> GetYesterdayTransactionCount()
        {
            var response = await _dailyTransactionRepo.GetYesterdayTransactionCounts();
            return response;
        }

        //AccountOpened
        [HttpGet]
        public async Task<object> DailyOpenedAccount()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccount();
            return response;
        }

        //AccountOpenedyesterdaybankwide
        [HttpGet]
        public async Task<object> AccountOpenedYesterday()
        {
            var response = await _dailyTransactionRepo.AccountOpenedYesterdayBankwide();
            return response;
        }

        //AccountOpened by AH
        [HttpGet]
        public async Task<object> DailyOpenedAccountByAdeolaHopewell()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByAdeolaHopewell();
            return response;
        }

        // AccountOpened by AH yesterday
        [HttpGet]
        public async Task<object> DailyOpenedAccountByAdeolaHopewellYesterday()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByAdeolaHopewellYesterday();
            return response;
        }

        //AccountOpened by Ogba
        [HttpGet]
        public async Task<object> DailyOpenedAccountByOgba()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByOgba();
            return response;
        }

        //AccountOpened by Ogba yesterday
        [HttpGet]
        public async Task<object> DailyOpenedAccountByOgbaYesterday()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByOgbaYesterday();
            return response;
        }

        //AccountOpened by Camp
        [HttpGet]
        public async Task<object> DailyOpenedAccountByCamp()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByCamp();
            return response;
        }

        //AccountOpened by Camp yesterday
        [HttpGet]
        public async Task<object> DailyOpenedAccountByCampYesterday()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByCampYesterday();
            return response;
        }

        //AccountOpened by Abuja
        [HttpGet]
        public async Task<object> DailyOpenedAccountByAbuja()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByAbuja();
            return response;
        }

        //AccountOpened by Abuja yesterday
        [HttpGet]
        public async Task<object> DailyOpenedAccountByAbujaYesterday()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByAbujaYesterday();
            return response;
        }

        //AccountOpened by Okooba
        [HttpGet]
        public async Task<object> DailyOpenedAccountByOkooba()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByOkooba();
            return response;
        }

        //AccountOpened by Okooba yesterday
        [HttpGet]
        public async Task<object> DailyOpenedAccountByOkoobaYesterday()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByOkoobaYesterday();
            return response;
        }



        [HttpGet]
        public async Task<object> GetDailyOpenedAccountByEchannelYesterday()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByEchannelYesterday();
            return response;
        }

        [HttpGet]
        public async Task<object> GetDailyOpenedAccountByEchannelToday()
        {
            var response = await _dailyTransactionRepo.GetDailyOpenedAccountByEchannelToday();
            return response;
        }

        //inward
        [HttpGet]
        public async Task<object> InwardTransactionPerDay()
        {
            var response = await _dailyTransactionRepo.InwardTransactionPerDay();
            return response;
        }

        //inward
        [HttpGet]
        public async Task<object> PreviousInwardTransaction()
        {
            var response = await _dailyTransactionRepo.YesterdayInwardTransaction();
            return response;
        }


        //inward value
        [HttpGet]
        public async Task<object> InwardTransactionPerDayAmount()
        {
            var response = await _dailyTransactionRepo.InwardTransactionPerDayValue();
            return response;
        }

        // yesterday inward amount 
        [HttpGet]
        public async Task<object> PreviousInwardTransactionAmount()
        {
            var response = await _dailyTransactionRepo.YesterdayTransactionValue();
            return response;
        }

        //outward
        [HttpGet]
        public async Task<object> OutwardTransactionPerDay()
        {
            var response = await _dailyTransactionRepo.OutwardTransactionPerDay();
            return response;
        }

        //outward yesterday
        [HttpGet]
        public async Task<object> OutwardTransactionYesterday()
        {
            var response = await _dailyTransactionRepo.OutwardTransactionYesterday();
            return response;
        }


        public async Task<object> OutwardTransactionPerDayValue()
        {
            var response = await _dailyTransactionRepo.OutwardTransactionPerDayValue();
            return response;
        }

        public async Task<object> OutwardTransactionPerDayValueYesterday()
        {
            var response = await _dailyTransactionRepo.OutwardTransactionPerDayValueYesterday();
            return response;
        }
        //outward
        [HttpGet]
        public async Task<object> ReversedTransactions()
        {
            var response = await _dailyTransactionRepo.ReversedTransactions();
            return response;
        }

        //yesterday reversed
        [HttpGet]
        public async Task<object> ReversedYesterday()
        {
            var response = await _dailyTransactionRepo.YesterdayReversedTransactions();
            return response;
        }

        //mail per day
        [HttpGet]
        public async Task<object> SentMailPerDay()
        {
            var response = await _dailyTransactionRepo.SentMailPerDay();
            return response;
        }

        //mail per day
        [HttpGet]
        public async Task<object> SentMailYesterday()
        {
            var response = await _dailyTransactionRepo.SentMailYesterday();
            return response;
        }

        //mail per day
        [HttpGet]
        public async Task<object> SentSMSPerDay()
        {
            var response = await _dailyTransactionRepo.SentSMSPerDay();
            return response;
        }

        //mail per day
        [HttpGet]
        public async Task<object> SmsMailYesterday()
        {
            var response = await _dailyTransactionRepo.SentSMSYesterday();
            return response;
        }


        //Bar Chat
        [HttpGet]
        public async Task<object> MonitorBarChat()
        {
            var response = await _dailyTransactionRepo.GetValueByCategories();
            return response;
        }

        // atm, withdrawal and transfer
        [HttpGet]
        public async Task<object> AtmWithdrawalTransfer()
        {
            var response = await _dailyTransactionRepo.AtmAndTransferTransaction();
            return response;
        }

        // atm, withdrawal and transfer yesterday
        [HttpGet]
        public async Task<object> AtmwithdrawalTransferYesterday()
        {
            var response = await _dailyTransactionRepo.AtmAndTransferTransactionYesterday();
            return response;
        }

        // post status
        [HttpGet]
        public async Task<object> ViewPostStatusReport()
        {
            var response = await _dailyTransactionRepo.ViewAccountPostStatus();
            return response;
        }

        // download poststatus
        [HttpGet]
        public async Task<object> DownloadPostStatusFile()
        {
            var response = await _dailyTransactionRepo.DownloadSqlDataAsExcel();
            return response;
        }

      

        // Remita Transactions
        [HttpGet]
        public async Task<object> RemitaTransactionCountYesterday()
        {
            var response = await _dailyTransactionRepo.RemitaTransactionsYesterday();
            return response;
        }
        [HttpGet]
        public async Task<object> RemitaTransactionCount()
        {
            var response = await _dailyTransactionRepo.RemitaTransactions();
            return response;
        }

        // Remita Volume
        [HttpGet]
        public async Task<object> RemitaVolumeCountYesterday()
        {
            var response = await _dailyTransactionRepo.RemitaVolumeYesterday();
            return response;
        }

        [HttpGet]
        public async Task<object> RemitaVolumeCount()
        {
            var response = await _dailyTransactionRepo.RemitaVolume();
            return response;
        }

        [HttpGet]
        public async Task<object> SuccessfulTransactionToday()
        {
            var response = await _dailyTransactionRepo.SuccessfulTransactionToday();
            return response;
        }

        [HttpGet]
        public async Task<object> SuccessfulTransactionYesterday()
        {
            var response = await _dailyTransactionRepo.SuccessfulTransactionYesterday();
            return response;
        }

        [HttpGet]
        public async Task<object> FailedTransactionToday()
        {
            var response = await _dailyTransactionRepo.FailedTransactionToday();
            return response;
        }

        [HttpGet]
        public async Task<object> FailedTransactionYesterday()
        {
            var response = await _dailyTransactionRepo.FailedTransactionYesterday();
            return response;
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
