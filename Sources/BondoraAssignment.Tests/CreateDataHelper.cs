using BondoraAssignment.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondoraAssignment.Tests
{
    class CreateDataHelper
    {
        /// <summary>
        /// Method to creat a false user with an existing cart of one product.
        /// </summary>
        /// <param name="numberOfDayForProduct"></param>
        /// <returns>return the user</returns>
        public User CreateUser(int numberOfDayForProduct)
        {
            User user = new User()
            {
                full_name = "test",
                id = 1,

            };
            user.CartDetails.Add(CreateCartDetail(numberOfDayForProduct));
            return user;
        }

        public CartDetail CreateCartDetail(int numberOfDayForProduct)
        {
            Category catTest = new Category()
            {
                name = "Test",
                description = "Test equipment",
                fidelity_point = 2,
                id = 1,
                rate_id = 1
            };
            Rate uniqueRate = new Rate()
            {
                name = "unique",
                price = 100,
                id = 1
            };
            Rate premiumRate = new Rate()
            {
                name = "premium",
                price = 60,
                id = 2
            };
            Rate specializedRate = new Rate()
            {
                name = "specialized",
                price = 40,
                id = 3
            };
            FromToCalculation testUnique = new FromToCalculation()
            {
                Category = catTest,
                from = 0,
                to = 0,
                Rate = uniqueRate,
            };
            FromToCalculation testDailyOne = new FromToCalculation()
            {
                Category = catTest,
                from = 1,
                to = 3,
                Rate = premiumRate,
            };
            FromToCalculation testDailyTwo = new FromToCalculation()
            {
                Category = catTest,
                from = 4,
                to = null,
                Rate = specializedRate,
            };
            catTest.FromToCalculations.Add(testUnique);
            catTest.FromToCalculations.Add(testDailyOne);
            catTest.FromToCalculations.Add(testDailyTwo);
            Product productTest = new Product()
            {
                name = "Test",
                Category = catTest,
                product_description = "Test",
            };
            CartDetail cartDetailTest = new CartDetail()
            {
                Product = productTest,
                number_of_day = numberOfDayForProduct,
            };
            productTest.CartDetails.Add(cartDetailTest);

            return cartDetailTest;
        }
    }
}
