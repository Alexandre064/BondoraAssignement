var rangeText = function (start, end) {

    if (start == null || end == null) {
        return "Please select a date";
    }

    var diffDays = GetNumberOfDays(start, end)

    var str = "You will rent this product for " + diffDays;

    if (diffDays == 1) {
        str += " day.";
    } else {
        str += " days.";
    }

    return str;
}

function GetNumberOfDays(start, end) {

    if (start == "" || end == "" || start==null || end==null)
    {
        return;
    }

    var startDate = new Date(start);
    var endDate = new Date(end);

    var numberOfMsPerDay = 24 * 60 * 60 * 1000;
    return 1 + Math.round(Math.abs((endDate - startDate) / (numberOfMsPerDay)));
}

function RentItem() {
    var id = document.URL.substring(document.URL.length - 1);
    var numberOfDay = document.getElementById("numderOfDays").innerHTML;

    if (numberOfDay == null || numberOfDay == "" || numberOfDay =="undefined") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please select a date to rent the product'
        });
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Product/ConfirmRentObject",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ productid: id, daysToRent: numberOfDay, userId: 1 }),
        success: function (result) {
            if (result == "succedded") {
                Swal.fire({
                    timer: 1500,
                    icon: 'success',
                    title: 'Product added to your order',
                    showConfirmButton: true
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'An error occured... Please try again.'
                });
            }
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'An error occured... Please try again.'
            });
        }
    });
}