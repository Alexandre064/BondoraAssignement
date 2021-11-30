function RemoveItemFromCart(productId,userId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Are you sure you want to remove this item ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, remove it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Confirmed(iproductId, userId);
        }
    })
}

function Confirmed(productId, user) {
    $.ajax({
        type: "POST",
        url: "/CartDetail/RemoveObjectFromCart",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ productidincart: productId, userId: user }),
        success: function (result) {
            if (result == "succedded") {
                Swal.fire({
                    icon: 'success',
                    title: 'Product removed from your order',
                    showConfirmButton: true
                }).then((result) => {
                    location.reload();
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

function ConfirmBuyOrder(userId)
{
    $.ajax({
        type: "POST",
        url: "/CartDetail/ConfirmOrder",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ user: userId }),
        success: function (result) {
            if (result == "succedded") {
                location.replace("/Users/Details/1")
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'An error occured... Please try again.'
                });
            }
        }
    })
}