using BondoraAssignment.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BondoraAssignment.Models
{
    static public class InvoiceHelper
    {
        private const string SEPARATOR = "------------------------";
        static async public Task<string> MakeInvoice(int userId, int orderId)
        {
            User user = await Task.Run(()=>DBRequest.GetUserById(userId));

            StringBuilder invoiceInString = new StringBuilder();

            invoiceInString.AppendLine(SEPARATOR + " INVOICE FROM " + SEPARATOR);
            invoiceInString.AppendLine("Bondora Construction");
            invoiceInString.AppendLine("A. H. Tammsaare tee 47, 11314 Tallinn, Estonia");
            invoiceInString.AppendLine("+37200000000");
            invoiceInString.AppendLine("bondoraconstruction@bondora.com");

            invoiceInString.AppendLine(Environment.NewLine);

            invoiceInString.AppendLine(SEPARATOR + " TO " + SEPARATOR);

            invoiceInString.AppendLine(user.full_name);
            invoiceInString.AppendLine(user.address);
            invoiceInString.AppendLine(user.phone);
            invoiceInString.AppendLine(user.email);

            invoiceInString.AppendLine(Environment.NewLine);

            invoiceInString.AppendLine(SEPARATOR + " PRODUCTS " + SEPARATOR);

            int cpt = 1;

            foreach(OrderDetail orderDetail in user.OrderDetails.Where(p => p.id == orderId))
            {
                foreach(ProductOrderDetail productOrderDetail in orderDetail.ProductOrderDetails)
                {
                    invoiceInString.AppendLine("#" + cpt);
                    invoiceInString.Append("Product : " + productOrderDetail.Product.name + " || ");
                    invoiceInString.Append("Type : " + productOrderDetail.Product.Category.name + " || ");
                    invoiceInString.Append("Rented for : " + productOrderDetail.number_of_day + " days" + " || ");
                    invoiceInString.Append("Price : " + CalculatorHelper.CalculatePriceOfProductWithDay(productOrderDetail.number_of_day, productOrderDetail.Product) + " || ");
                    invoiceInString.Append("Loyality point gained : " + productOrderDetail.Product.Category.fidelity_point + Environment.NewLine);
                    cpt++;
                }
                invoiceInString.AppendLine(SEPARATOR + " TOTAL " + SEPARATOR);
                invoiceInString.AppendLine("Total price : " + orderDetail.Order.total_price);
                invoiceInString.AppendLine("Total loyality point : " + orderDetail.Order.total_fidelity_point);
                invoiceInString.AppendLine("Thank you for your purchase.");
            }

            return invoiceInString.ToString();

        }
    }
}