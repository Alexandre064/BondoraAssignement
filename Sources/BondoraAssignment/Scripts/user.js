function DownloadInvoice(order, user) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/Users/DownloadInvoiceFile',
        data: JSON.stringify({ orderid: order, userId: user }),
        success: function (data) {
            var response = data;
            window.location = '/Users/Download?fileGuid=' + response.FileGuid
                + '&filename=' + response.FileName;
        }
    })
}