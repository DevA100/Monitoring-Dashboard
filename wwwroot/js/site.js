$(document).ready(function () {
    // Fetch all TODAY's data separately for speed
    function fetchTodayData() {
        const todayEndpoints = [
            { url: "/Home/GetDailyTransactionCount", selector: "#DailyTransact" },
            { url: "/Home/DailyOpenedAccount", selector: "#DailyAccountOpened" },
            { url: "/Home/InwardTransactionPerDay", selector: "#InwardTransactionPerDay" },
            // REMOVED: InwardTransactionPerDayAmount - moved to todayTextEndpoints
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

        // These return formatted strings, not JSON numbers
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
                // Remove quotes if they exist
                const cleanResponse = response.replace(/^"(.*)"$/, '$1');
                console.log(`✅ ${item.url} =>`, cleanResponse);
                $(item.selector).text(cleanResponse);
            }).catch(err => console.warn(`❌ Error: ${item.url}`, err))
        );

        return Promise.all([...todayRequests, ...todayTextRequests]);
    }

    // Fetch all YESTERDAY & static data separately
    function fetchOtherData() {
        const otherEndpoints = [
            { url: "/Home/PreviousInwardTransaction", selector: "#yesterday" },
            // REMOVED: PreviousInwardTransactionAmount - moved to otherTextEndpoints
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

        // These return formatted strings, not JSON numbers
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
                // Remove quotes if they exist
                const cleanResponse = response.replace(/^"(.*)"$/, '$1');
                console.log(`✅ ${item.url} =>`, cleanResponse);
                $(item.selector).text(cleanResponse);
            }).catch(err => console.warn(`❌ Error: ${item.url}`, err))
        );

        // Fetch chart
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

    // Call both together for faster speed
    function refreshDashboard() {
        console.log("🔄 Fetching dashboard data...");
        Promise.all([fetchTodayData(), fetchOtherData()])
            .then(() => console.log("✅ All data updated."))
            .catch(() => console.warn("⚠ Some parts failed to update."));
    }

    // Load immediately + every 60s
    refreshDashboard();
    setInterval(refreshDashboard, 60000);
});























//$(document).ready(function () {


//    // Function to handle AJAX requests and update the UI
//    function fetchData() {
//        // Define an array of promises for the AJAX requests
//        const requests = [
//            $.ajax({
//                type: "GET",
//                url: "/Home/SuccessfulTransactionToday",
//                data: {},
//                contentType: "application/json; charset=utf-8",
//                dataType: "json"
//            }).then(response => $("#SuccessfulTransactionsToday").text(response)),

//            $.ajax({
//                type: "GET",
//                url: "/Home/SuccessfulTransactionYesterday",
//                data: {},
//                contentType: "application/json; charset=utf-8",
//                dataType: "json"
//            }).then(response => $("#SuccessfulTransactionsYesterday").text(response)),

//            $.ajax({
//                type: "GET",
//                url: "/Home/FailedTransactionToday",
//                data: {},
//                contentType: "application/json; charset=utf-8",
//                dataType: "json"
//            }).then(response => $("#FailedTransactionsToday").text(response)),

//            $.ajax({
//                type: "GET",
//                url: "/Home/FailedTransactionYesterday",
//                data: {},
//                contentType: "application/json; charset=utf-8",
//                dataType: "json"
//            }).then(response => $("#FailedTransactionsYesterday").text(response))
//        ];

//        // Use Promise.all to wait for all AJAX requests to complete
//        Promise.all(requests).then(() => {
//            console.log('All data fetched and UI updated.');
//        }).catch(error => {
//            console.error('Error fetching data:', error);
//        });
//    }

//    // Call the function initially
//    fetchData();

//    // Set up an interval to refresh the data every 60 seconds (60000 milliseconds)
//    setInterval(fetchData, 60000); // Adjust the interval as needed




//    //Deva

//    function fetchData() {
//        const requests = [
//            { url: "/Home/GetDailyTransactionCount", selector: "#DailyTransact" },
//            { url: "/Home/DailyOpenedAccount", selector: "#DailyAccountOpened" },
//            { url: "/Home/InwardTransactionPerDay", selector: "#InwardTransactionPerDay" },
//            { url: "/Home/InwardTransactionPerDayAmount", selector: "#InwardTransactionPerDayValue" },
//            { url: "/Home/PreviousInwardTransaction", selector: "#yesterday" },
//            { url: "/Home/PreviousInwardTransactionAmount", selector: "#yesterdayamount" },
//            { url: "/Home/SentMailYesterday", selector: "#mailsentyesterday" },
//            { url: "/Home/SmsMailYesterday", selector: "#smssentyesterday" },
//            { url: "/Home/AccountOpenedYesterday", selector: "#yesterdayaccountbankwide" },
//            { url: "/Home/ReversedYesterday", selector: "#yesterdayreverse" },
//            { url: "/Home/AtmwithdrawalTransferYesterday", selector: "#atmtranferyestreday" },
//            { url: "/Home/OutwardTransactionYesterday", selector: "#outwardtranyesterday" },
//            { url: "/Home/GetYesterdayTransactionCount", selector: "#yesterdayTransaction" },
//            { url: "/Home/DailyOpenedAccountByAdeolaHopewellYesterday", selector: "#hopewellyesterday" },
//            { url: "/Home/DailyOpenedAccountByOgbaYesterday", selector: "#ogbayesterday" },
//            { url: "/Home/DailyOpenedAccountByCampYesterday", selector: "#campyesterday" },
//            { url: "/Home/DailyOpenedAccountByAbujaYesterday", selector: "#abujayesterday" },
//            { url: "/Home/DailyOpenedAccountByOkoobaYesterday", selector: "#okoobayesterday" },
//            { url: "/Home/RemitaTransactionCountYesterday", selector: "#remitatranscountyesterday" },
//            { url: "/Home/RemitaTransactionCount", selector: "#remitatranscount" },
//            { url: "/Home/RemitaVolumeCount", selector: "#remitavolumecount" },
//            { url: "/Home/RemitaVolumeCountYesterday", selector: "#remitavolumecountyesterday" },
//            { url: "/Home/OutwardTransactionPerDay", selector: "#OutwardTransactionPerDay" },
//            { url: "/Home/ReversedTransactions", selector: "#ReversedTransactions" },
//            { url: "/Home/SentMailPerDay", selector: "#SentMailPerDay" },
//            { url: "/Home/SentSMSPerDay", selector: "#SentSMSPerDay" },
//            { url: "/Home/AtmWithdrawalTransfer", selector: "#AtmAndTransferTransaction" },
//            { url: "/Home/DailyOpenedAccountByAdeolaHopewell", selector: "#hopewell" },
//            { url: "/Home/DailyOpenedAccountByOgba", selector: "#ogba" },
//            { url: "/Home/DailyOpenedAccountByCamp", selector: "#camp" },
//            { url: "/Home/DailyOpenedAccountByAbuja", selector: "#abuja" },
//            { url: "/Home/DailyOpenedAccountByOkooba", selector: "#okooba" },
//            { url: "/Home/SuccessfulTransactionToday", selector: "#SuccessfulTransactionsToday" },
//            { url: "/Home/SuccessfulTransactionYesterday", selector: "#SuccessfulTransactionsYesterday" },
//            { url: "/Home/FailedTransactionToday", selector: "#FailedTransactionsToday" },
//            { url: "/Home/FailedTransactionYesterday", selector: "#FailedTransactionsYesterday" }
//        ];

//        const ajaxCalls = requests.map(item =>
//            $.ajax({
//                type: "GET",
//                url: item.url,
//                contentType: "application/json; charset=utf-8",
//                dataType: "json"
//            }).then(response => {
//                $(item.selector).text(response);
//            }).catch(err => {
//                console.error(`Error fetching ${item.url}:`, err);
//            })
//        );

//        // Monitor Chart separately
//        const chartRequest = $.ajax({
//            type: "GET",
//            url: "/Home/MonitorBarChat",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json"
//        }).then(response => {
//            const xValues = response.categories;
//            const yValues = response.values;
//            const barColors = ["red", "green", "blue", "orange", "brown", "black", "moccasin", "lavender"];

//            new Chart("myBarChart", {
//                type: "bar",
//                data: {
//                    labels: xValues,
//                    datasets: [{
//                        backgroundColor: barColors,
//                        data: yValues
//                    }]
//                },
//                options: {
//                    legend: { display: false },
//                    title: { display: true }
//                }
//            });
//        }).catch(err => {
//            console.error("Error loading chart:", err);
//        });

//        Promise.all([...ajaxCalls, chartRequest])
//            .then(() => {
//                console.log("✅ All data updated");
//            })
//            .catch(err => {
//                console.error("⚠️ Some data failed to load", err);
//            });
//    }

//    // Initial load
//    fetchData();

//    // Auto-refresh every 60 seconds
//    setInterval(fetchData, 60000);


   ///buchi
//    setInterval(function () {

//        $.ajax({
//            type: "GET",
//            url: "/Home/GetDailyTransactionCount",
//            data: {},
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (response) {
//                $("#DailyTransact").text(response);

//                // Daily opened account
//                $.ajax({
//                    type: "GET",
//                    url: "/Home/DailyOpenedAccount",
//                    data: {},
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    success: function (response) {
//                        $("#DailyAccountOpened").text(response);

//                        //inward flow
//                        $.ajax({
//                            type: "GET",
//                            url: "/Home/InwardTransactionPerDay",
//                            data: {},
//                            contentType: "application/json; charset=utf-8",
//                            dataType: "json",
//                            success: function (response) {
//                                $("#InwardTransactionPerDay").text(response);



//                                $.ajax({
//                                    type: "GET",
//                                    url: "/Home/InwardTransactionPerDayAmount",
//                                    data: {},
//                                    contentType: "application/json; charset=utf-8",
//                                    dataType: "json",
//                                    success: function (response) {
//                                        $("#InwardTransactionPerDayValue").text(response);

//                                        // yesterday inward
//                                        $.ajax({
//                                            type: "GET",
//                                            url: "/Home/PreviousInwardTransaction",
//                                            data: {},
//                                            contentType: "application/json; charset=utf-8",
//                                            dataType: "json",
//                                            success: function (response) {
//                                                $("#yesterday").text(response);

//                                                // yesterdayvalue
//                                                $.ajax({
//                                                    type: "GET",
//                                                    url: "/Home/PreviousInwardTransactionAmount",
//                                                    data: {},
//                                                    contentType: "application/json; charset=utf-8",
//                                                    dataType: "json",
//                                                    success: function (response) {
//                                                        $("#yesterdayamount").text(response);

//                                                        //mail sent yesterday
//                                                        $.ajax({
//                                                            type: "GET",
//                                                            url: "/Home/SentMailYesterday",
//                                                            data: {},
//                                                            contentType: "application/json; charset=utf-8",
//                                                            dataType: "json",
//                                                            success: function (response) {
//                                                                $("#mailsentyesterday").text(response);
//                                                                // sms yesterday
//                                                                $.ajax({
//                                                                    type: "GET",
//                                                                    url: "/Home/SmsMailYesterday",
//                                                                    data: {},
//                                                                    contentType: "application/json; charset=utf-8",
//                                                                    dataType: "json",
//                                                                    success: function (response) {
//                                                                        $("#smssentyesterday").text(response);

//                                                                        // account opened yesterday bankwide
//                                                                        $.ajax({
//                                                                            type: "GET",
//                                                                            url: "/Home/AccountOpenedYesterday",
//                                                                            data: {},
//                                                                            contentType: "application/json; charset=utf-8",
//                                                                            dataType: "json",
//                                                                            success: function (response) {
//                                                                                $("#yesterdayaccountbankwide").text(response);

//                                                                                // reversed yesterday
//                                                                                $.ajax({
//                                                                                    type: "GET",
//                                                                                    url: "/Home/ReversedYesterday",
//                                                                                    data: {},
//                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                    dataType: "json",
//                                                                                    success: function (response) {
//                                                                                        $("#yesterdayreverse").text(response);

//                                                                                        // REVERSED ATM, WITHDRWAL AND TRANSFER
//                                                                                        $.ajax({
//                                                                                            type: "GET",
//                                                                                            url: "/Home/AtmwithdrawalTransferYesterday",
//                                                                                            data: {},
//                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                            dataType: "json",
//                                                                                            success: function (response) {
//                                                                                                $("#atmtranferyestreday").text(response);

//                                                                                                //outward transaction
//                                                                                                $.ajax({
//                                                                                                    type: "GET",
//                                                                                                    url: "/Home/OutwardTransactionYesterday",
//                                                                                                    data: {},
//                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                    dataType: "json",
//                                                                                                    success: function (response) {
//                                                                                                        $("#outwardtranyesterday").text(response);

//                                                                                                        // yesterday daily transaction

//                                                                                                        $.ajax({
//                                                                                                            type: "GET",
//                                                                                                            url: "/Home/GetYesterdayTransactionCount",
//                                                                                                            data: {},
//                                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                                            dataType: "json",
//                                                                                                            success: function (response) {
//                                                                                                                $("#yesterdayTransaction").text(response);

//                                                                                                                $.ajax({
//                                                                                                                    type: "GET",
//                                                                                                                    url: "/Home/DailyOpenedAccountByAdeolaHopewellYesterday",
//                                                                                                                    data: {},
//                                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                                    dataType: "json",
//                                                                                                                    success: function (response) {
//                                                                                                                        $("#hopewellyesterday").text(response);

//                                                                                                                        $.ajax({
//                                                                                                                            type: "GET",
//                                                                                                                            url: "/Home/DailyOpenedAccountByOgbaYesterday",
//                                                                                                                            data: {},
//                                                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                                                            dataType: "json",
//                                                                                                                            success: function (response) {
//                                                                                                                                $("#ogbayesterday").text(response);

//                                                                                                                                $.ajax({
//                                                                                                                                    type: "GET",
//                                                                                                                                    url: "/Home/DailyOpenedAccountByCampYesterday",
//                                                                                                                                    data: {},
//                                                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                                                    dataType: "json",
//                                                                                                                                    success: function (response) {
//                                                                                                                                        $("#campyesterday").text(response);

//                                                                                                                                        $.ajax({
//                                                                                                                                            type: "GET",
//                                                                                                                                            url: "/Home/DailyOpenedAccountByAbujaYesterday",
//                                                                                                                                            data: {},
//                                                                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                                                                            dataType: "json",
//                                                                                                                                            success: function (response) {
//                                                                                                                                                $("#abujayesterday").text(response);

//                                                                                                                                                $.ajax({
//                                                                                                                                                    type: "GET",
//                                                                                                                                                    url: "/Home/DailyOpenedAccountByOkoobaYesterday",
//                                                                                                                                                    data: {},
//                                                                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                                                                    dataType: "json",
//                                                                                                                                                    success: function (response) {
//                                                                                                                                                        $("#okoobayesterday").text(response);

//                                                                                                                                                        $.ajax({
//                                                                                                                                                            type: "GET",
//                                                                                                                                                            url: "/Home/RemitaTransactionCountYesterday",
//                                                                                                                                                            data: {},
//                                                                                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                                                                                            dataType: "json",
//                                                                                                                                                            success: function (response) {
//                                                                                                                                                                $("#remitatranscountyesterday").text(response);

//                                                                                                                                                                $.ajax({
//                                                                                                                                                                    type: "GET",
//                                                                                                                                                                    url: "/Home/RemitaTransactionCount",
//                                                                                                                                                                    data: {},
//                                                                                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                                                                                    dataType: "json",
//                                                                                                                                                                    success: function (response) {
//                                                                                                                                                                        $("#remitatranscount").text(response);

//                                                                                                                                                                        $.ajax({
//                                                                                                                                                                            type: "GET",
//                                                                                                                                                                            url: "/Home/RemitaVolumeCount",
//                                                                                                                                                                            data: {},
//                                                                                                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                                                                                                            dataType: "json",
//                                                                                                                                                                            success: function (response) {
//                                                                                                                                                                                $("#remitavolumecount").text(response);

//                                                                                                                                                                                $.ajax({
//                                                                                                                                                                                    type: "GET",
//                                                                                                                                                                                    url: "/Home/RemitaVolumeCountYesterday",
//                                                                                                                                                                                    data: {},
//                                                                                                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                                                                                                    dataType: "json",
//                                                                                                                                                                                    success: function (response) {
//                                                                                                                                                                                        $("#remitavolumecountyesterday").text(response);

                                                                                                                                                                                    
                                                                                                                                                                             
//                                                                                                                                                                                    }
//                                                                                                                                                                                });
//                                                                                                                                                                            }
//                                                                                                                                                                        });

//                                                                                                                                                                    }
//                                                                                                                                                                });
//                                                                                                                                                            }
//                                                                                                                                                        });
//                                                                                                                                                    }
//                                                                                                                                                });
//                                                                                                                                            }
//                                                                                                                                        });
//                                                                                                                                    }
//                                                                                                                                });
//                                                                                                                            }
//                                                                                                                        });
//                                                                                                                    }
//                                                                                                                });
//                                                                                                            }
//                                                                                                        });

//                                                                                                    }
//                                                                                                });
//                                                                                            }
//                                                                                        });

//                                                                                    }
//                                                                                });
//                                                                            }
//                                                                        });

//                                                                    }
//                                                                });
//                                                            }
//                                                        });
//                                                    }
//                                                });



//                                            }
//                                        });
//                                    }

//                                });
                            

//                                // outward flow
//                                $.ajax({
//                                    type: "GET",
//                                    url: "/Home/OutwardTransactionPerDay",
//                                    data: {},
//                                    contentType: "application/json; charset=utf-8",
//                                    dataType: "json",
//                                    success: function (response) {
//                                        $("#OutwardTransactionPerDay").text(response);

//                                        //Reversed Transactions per day
//                                        $.ajax({
//                                            type: "GET",
//                                            url: "/Home/ReversedTransactions",
//                                            data: {},
//                                            contentType: "application/json; charset=utf-8",
//                                            dataType: "json",
//                                            success: function (response) {
//                                                $("#ReversedTransactions").text(response);

//                                                // sent mail per day
//                                                $.ajax({
//                                                    type: "GET",
//                                                    url: "/Home/SentMailPerDay",
//                                                    data: {},
//                                                    contentType: "application/json; charset=utf-8",
//                                                    dataType: "json",
//                                                    success: function (response) {
//                                                        $("#SentMailPerDay").text(response);

//                                                        // sent sms per day

//                                                        $.ajax({
//                                                            type: "GET",
//                                                            url: "/Home/SentSMSPerDay",
//                                                            data: {},
//                                                            contentType: "application/json; charset=utf-8",
//                                                            dataType: "json",
//                                                            success: function (response) {
//                                                                $("#SentSMSPerDay").text(response);

//                                                                //atm/withdrawal/transfer
//                                                                $.ajax({
//                                                                    type: "GET",
//                                                                    url: "/Home/AtmWithdrawalTransfer",
//                                                                    data: {},
//                                                                    contentType: "application/json; charset=utf-8",
//                                                                    dataType: "json",
//                                                                    success: function (response) {
//                                                                        $("#AtmAndTransferTransaction").text(response);

//                                                                        // first outward from 192.168.224.41 db
//                                                                        $.ajax({
//                                                                            type: "GET",
//                                                                            url: "/Home/DailyOpenedAccountByAdeolaHopewell",
//                                                                            data: {},
//                                                                            contentType: "application/json; charset=utf-8",
//                                                                            dataType: "json",
//                                                                            success: function (response) {
//                                                                                $("#hopewell").text(response);

//                                                                                // open by AH
//                                                                                $.ajax({
//                                                                                    type: "GET",
//                                                                                    url: "/Home/DailyOpenedAccountByOgba",
//                                                                                    data: {},
//                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                    dataType: "json",
//                                                                                    success: function (response) {
//                                                                                        $("#ogba").text(response);

//                                                                                        //camp
//                                                                                        $.ajax({
//                                                                                            type: "GET",
//                                                                                            url: "/Home/DailyOpenedAccountByCamp",
//                                                                                            data: {},
//                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                            dataType: "json",
//                                                                                            success: function (response) {
//                                                                                                $("#camp").text(response);

//                                                                                                //abuja
//                                                                                                $.ajax({
//                                                                                                    type: "GET",
//                                                                                                    url: "/Home/DailyOpenedAccountByAbuja",
//                                                                                                    data: {},
//                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                    dataType: "json",
//                                                                                                    success: function (response) {
//                                                                                                        $("#abuja").text(response);
//                                                                                                        //oko-oba
//                                                                                                        $.ajax({
//                                                                                                            type: "GET",
//                                                                                                            url: "/Home/DailyOpenedAccountByOkooba",
//                                                                                                            data: {},
//                                                                                                            contentType: "application/json; charset=utf-8",
//                                                                                                            dataType: "json",
//                                                                                                            success: function (response) {
//                                                                                                                $("#okooba").text(response);

//                                                                                                                // chat
//                                                                                                                $.ajax({
//                                                                                                                    type: "GET",
//                                                                                                                    url: "/Home/MonitorBarChat",
//                                                                                                                    data: {},
//                                                                                                                    contentType: "application/json; charset=utf-8",
//                                                                                                                    dataType: "json",
//                                                                                                                    success: function (response) {

//                                                                                                                        console.log("checking bar chart ", response);

//                                                                                                                        var xValues = response.categories;//["Italy", "France", "Spain", "USA", "Argentina"];
//                                                                                                                        var yValues = response.values;//[55, 49, 44, 24, 15];
//                                                                                                                        var barColors = ["red", "green", "blue", "orange", "brown", "black", "moccasin", "lavender"];

//                                                                                                                        new Chart("myBarChart", {
//                                                                                                                            type: "bar",
//                                                                                                                            data: {
//                                                                                                                                labels: xValues,
//                                                                                                                                datasets: [{
//                                                                                                                                    backgroundColor: barColors,
//                                                                                                                                    data: yValues
//                                                                                                                                }]
//                                                                                                                            },
//                                                                                                                            options: {
//                                                                                                                                legend: { display: false },
//                                                                                                                                title: {
//                                                                                                                                    display: true,
//                                                                                                                                    //text: "Values By Categories"
//                                                                                                                                }
//                                                                                                                            }
//                                                                                                                        });

//                                                                                                                    },
//                                                                                                                });
//                                                                                                            }
//                                                                                                        });
//                                                                                                    }
//                                                                                                });
//                                                                                            }
//                                                                                        });

//                                                                                    }
//                                                                                });

//                                                                            }
//                                                                        });

//                                                                    }
//                                                                });

//                                                            }
//                                                        });
//                                                    }
//                                                });
//                                            }
//                                        });
//                                    }
//                                });
//                            }
//                        });
//                    }
//                });
//            },
//            failure: function (res) {
//                /* toastr.error("Service error. please retry ", "Error")*/
//            },
//            error: function (res) {
//                /* toastr.error("Service error. please retry ", "Error")*/
//            }
//        });
    
//    }, 5000);
//});
