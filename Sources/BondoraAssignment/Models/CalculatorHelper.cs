using BondoraAssignment.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BondoraAssignment.Models
{
    static public class CalculatorHelper
    {
        /// <summary>
        /// Calculate the total price for the rental of an product with the number of day.
        /// </summary>
        /// <param name="numberOfDay"></param>
        /// <param name="product"></param>
        /// <returns>The price of the product</returns>
        static public int? CalculatePriceOfProductWithDay(int? numberOfDay, Product product)
        {
            int? totalPrice = 0;
            ICollection<FromToCalculation> fromToCalculations = product.Category.FromToCalculations;

            int? tmp = numberOfDay;

            foreach (FromToCalculation rateInfo in fromToCalculations)
            {
                if (tmp == 0)
                {
                    return totalPrice;
                }
                else if (rateInfo.from == rateInfo.to)
                {
                    totalPrice += rateInfo.Rate.price;
                }
                else if (numberOfDay >= rateInfo.from && numberOfDay <= rateInfo.to)
                {
                    totalPrice += rateInfo.Rate.price * tmp;
                    tmp -= tmp;
                }
                else if (numberOfDay > rateInfo.to)
                {
                    totalPrice += rateInfo.to * rateInfo.Rate.price;
                    tmp -= rateInfo.to;

                }
                else if (rateInfo.to == null)
                {
                    totalPrice += rateInfo.Rate.price * tmp;
                    tmp = 0;
                }
            }

            return totalPrice;
        }

        /// <summary>
        /// Return the total price of an user's cart.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        static public int? CalculateTotalPrice(User user)
        {
            int? total = 0;

            foreach (CartDetail cartDetail in user.CartDetails)
            {
                total += CalculatePriceOfProductWithDay(cartDetail.number_of_day, cartDetail.Product);
            }
            return total;
        }

        /// <summary>
        /// Return the total loyality point of an user's cart.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        static public int? CalculateTotalFidelityPoint(User user)
        {
            int? total = 0;

            foreach (CartDetail cartDetail in user.CartDetails)
            {
                total += cartDetail.Product.Category.fidelity_point;
            }
            return total;
        }
    }
}