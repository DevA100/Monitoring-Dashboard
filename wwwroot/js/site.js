$(document).ready(function () {
    function fetchTodayData() {
        const todayEndpoints = [
            { url: "/Home/GetDailyTransactionCount", selector: "#DailyTransact" },
            { url: "/Home/DailyOpenedAccount", selector: "#DailyAccountOpened" },
            { url: "/Home/InwardTransactionPerDay", selector: "#InwardTransactionPerDay" },
            { url: "/Home/OutwardTransactionPerDay", selector: "#OutwardTransactionPerDay" },
           
            { url: "/Home/ReversedTransactions", selector: "#ReversedTransactions" },
            { url: "/Home/SentMailPerDay", selector: "#SentMailPerDay" },
            { url: "/Home/SentSMSPerDay", selector: "#SentSMSPerDay" },
            { url: "/Home/AtmWithdrawalTransfer", selector: "#AtmAndTransferTransaction" },
            { url: "/Home/DailyOpenedAccountByAdeolaHopewell", selector: "#hopewell" },
            { url: "/Home/DailyOpenedAccountByOgba", selector: "#ogba" },
            { url: "/Home/DailyOpenedAccountByCamp", selector: "#camp" },
            { url: "/Home/DailyOpenedAccountByAbuja", selector: "#abuja" },
            { url: "/Home/DailyOpenedAccountByOkooba", selector: "#okooba" },
            { url: "/Home/GetDailyOpenedAccountByEchannelToday", selector: "#echannel" },
            { url: "/Home/RemitaTransactionCount", selector: "#remitatranscount" },
            { url: "/Home/SuccessfulTransactionToday", selector: "#SuccessfulTransactionsToday" },
            { url: "/Home/FailedTransactionToday", selector: "#FailedTransactionsToday" }
        ];

        const todayTextEndpoints = [
            { url: "/Home/RemitaVolumeCount", selector: "#remitavolumecount" },
            { url: "/Home/InwardTransactionPerDayAmount", selector: "#InwardTransactionPerDayValue" },
             { url: "/Home/OutwardTransactionPerDayValue", selector: "#OutwardTransactionPerDayValue" } 
        ];

        const todayRequests = todayEndpoints.map(item =>
            $.ajax({
                type: "GET",
                url: item.url,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then(response => {
                $(item.selector).text(response);
            }).catch(err => console.warn(`❌ Error: ${item.url}`, err))
        );

        const todayTextRequests = todayTextEndpoints.map(item =>
            $.ajax({
                type: "GET",
                url: item.url,
                dataType: "text"
            }).then(response => {
                const cleanResponse = response.replace(/^"(.*)"$/, '$1');
                console.log(`✅ ${item.url} =>`, cleanResponse);
                $(item.selector).text(cleanResponse);
            }).catch(err => console.warn(`❌ Error: ${item.url}`, err))
        );

        return Promise.all([...todayRequests, ...todayTextRequests]);
    }

    function fetchOtherData() {
        const otherEndpoints = [
            { url: "/Home/PreviousInwardTransaction", selector: "#yesterday" },
            { url: "/Home/SentMailYesterday", selector: "#mailsentyesterday" },
            { url: "/Home/SmsMailYesterday", selector: "#smssentyesterday" },
            { url: "/Home/AccountOpenedYesterday", selector: "#yesterdayaccountbankwide" },
            { url: "/Home/ReversedYesterday", selector: "#yesterdayreverse" },
            { url: "/Home/AtmwithdrawalTransferYesterday", selector: "#atmtranferyestreday" },
            { url: "/Home/OutwardTransactionYesterday", selector: "#outwardtranyesterday" },
            { url: "/Home/GetYesterdayTransactionCount", selector: "#yesterdayTransaction" },
            { url: "/Home/DailyOpenedAccountByAdeolaHopewellYesterday", selector: "#hopewellyesterday" },
            { url: "/Home/DailyOpenedAccountByOgbaYesterday", selector: "#ogbayesterday" },
            { url: "/Home/DailyOpenedAccountByCampYesterday", selector: "#campyesterday" },
            { url: "/Home/DailyOpenedAccountByAbujaYesterday", selector: "#abujayesterday" },
            { url: "/Home/DailyOpenedAccountByOkoobaYesterday", selector: "#okoobayesterday" },
            { url: "/Home/GetDailyOpenedAccountByEchannelYesterday", selector: "#echannelyesterday" },
            { url: "/Home/RemitaTransactionCountYesterday", selector: "#remitatranscountyesterday" },
            { url: "/Home/SuccessfulTransactionYesterday", selector: "#SuccessfulTransactionsYesterday" },
            { url: "/Home/FailedTransactionYesterday", selector: "#FailedTransactionsYesterday" }
        ];

        const otherTextEndpoints = [
            { url: "/Home/RemitaVolumeCountYesterday", selector: "#remitavolumecountyesterday" },
            { url: "/Home/PreviousInwardTransactionAmount", selector: "#yesterdayamount" },
            { url: "/Home/OutwardTransactionPerDayValueYesterday", selector: "#OutwardTransactionPerDayValueYesterday" } 
        ];

        const otherRequests = otherEndpoints.map(item =>
            $.ajax({
                type: "GET",
                url: item.url,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then(response => {
                $(item.selector).text(response);
            }).catch(err => console.warn(`❌ Error: ${item.url}`, err))
        );

        const otherTextRequests = otherTextEndpoints.map(item =>
            $.ajax({
                type: "GET",
                url: item.url,
                dataType: "text"
            }).then(response => {
                const cleanResponse = response.replace(/^"(.*)"$/, '$1');
                console.log(`✅ ${item.url} =>`, cleanResponse);
                $(item.selector).text(cleanResponse);
            }).catch(err => console.warn(`❌ Error: ${item.url}`, err))
        );

        const chartRequest = $.ajax({
            type: "GET",
            url: "/Home/MonitorBarChat",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).then(response => {
            const xValues = response.categories;
            const yValues = response.values;
            const barColors = ["red", "green", "blue", "orange", "brown", "black", "moccasin", "lavender"];
            new Chart("myBarChart", {
                type: "bar",
                data: {
                    labels: xValues,
                    datasets: [{ backgroundColor: barColors, data: yValues }]
                },
                options: {
                    legend: { display: false },
                    title: { display: true }
                }
            });
        }).catch(err => console.error("❌ Bar chart error", err));

        return Promise.all([...otherRequests, ...otherTextRequests, chartRequest]);
    }

    function refreshDashboard() {
        console.log("🔄 Fetching dashboard data...");
        Promise.all([fetchTodayData(), fetchOtherData()])
            .then(() => console.log("✅ All data updated."))
            .catch(() => console.warn("⚠ Some parts failed to update."));
    }

    refreshDashboard();
    setInterval(refreshDashboard, 60000);
});





















