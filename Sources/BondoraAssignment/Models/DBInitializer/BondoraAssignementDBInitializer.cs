using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BondoraAssignment.Models.EntityFramework;

namespace BondoraAssignment.Models.DBInitializer
{
    public class BondoraAssignementDBInitializer
    {
        /// <summary>
        /// This method add datas into the database. For test purpose only.
        /// </summary>
        /// <param name="context">Database contexte</param>
        static public void InitDataBaseWithTestData()
        {
            using (BondoraAssignementDBEntities context = new BondoraAssignementDBEntities())
            {

                #region Populate Inventory
                #region Populate Category
                Category catHeavy = new Category()
                {
                    name = "Heavy",
                    description = "Heavy equipment",
                    fidelity_point = 2,
                    id = 1,
                    rate_id = 1
                };

                Category catRegular = new Category()
                {
                    name = "Regular",
                    description = "Regular equipment",
                    fidelity_point = 1,
                    id = 2,
                    rate_id = 2,
                };

                Category catSpecialized = new Category()
                {
                    name = "Specialized",
                    description = "Specialized equipment",
                    fidelity_point = 1,
                    id = 3,
                    rate_id = 3
                };
                #endregion

                #region Populate Rate
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
                #endregion

                #region Populate FromToCalculation
                FromToCalculation calculationHeavyUnique = new FromToCalculation()
                {
                    Category = catHeavy,
                    from = 0,
                    to = 0,
                    Rate = uniqueRate,
                };
                FromToCalculation calculationHeavyDaily = new FromToCalculation()
                {
                    Category = catHeavy,
                    from = 1,
                    to = null,
                    Rate = premiumRate,
                };

                FromToCalculation calculationRegularUnique = new FromToCalculation()
                {
                    Category = catRegular,
                    from = 0,
                    to = 0,
                    Rate = uniqueRate,
                };
                FromToCalculation calculationRegularDaily1 = new FromToCalculation()
                {
                    Category = catRegular,
                    from = 1,
                    to = 2,
                    Rate = premiumRate,
                };
                FromToCalculation calculationRegularDaily2 = new FromToCalculation()
                {
                    Category = catRegular,
                    from = 3,
                    to = null,
                    Rate = specializedRate,
                };

                FromToCalculation calculationSpecialisedDaily1 = new FromToCalculation()
                {
                    Category = catSpecialized,
                    from = 1,
                    to = 3,
                    Rate = premiumRate,
                };
                FromToCalculation calculationSpecialisedDaily2 = new FromToCalculation()
                {
                    Category = catSpecialized,
                    from = 4,
                    to = null,
                    Rate = specializedRate,
                };

                #endregion

                #region Populate Products
                Product caterBull = new Product()
                {
                    name = "Caterpillar bulldozer",
                    Category = catHeavy,
                    product_description = "Lorem Ipsum Caterpillar Bulldozer"
                };

                Product kamTruck = new Product()
                {
                    name = "KamAZ truck",
                    Category = catRegular,
                    product_description = "Lorem Ipsum KamAZ truck"
                };

                Product komatsuCrane = new Product()
                {
                    name = "Komatsu crane",
                    Category = catHeavy,
                    product_description = "Lorem Ipsum Komatsu crane"
                };

                Product volvoSteamroller = new Product()
                {
                    name = "Volvo steamroller",
                    Category = catRegular,
                    product_description = "Lorem Ipsum Volvo steamroller"
                };

                Product boschJackhammer = new Product()
                {
                    name = "Bosch jackhammer",
                    Category = catSpecialized,
                    product_description = "Lorem Ipsum Bosch jackhammer"
                };
                #endregion

                context.Categories.Add(catHeavy);
                context.Categories.Add(catRegular);
                context.Categories.Add(catSpecialized);
                context.Rates.Add(uniqueRate);
                context.Rates.Add(premiumRate);
                context.Rates.Add(specializedRate);
                context.FromToCalculations.Add(calculationHeavyDaily);
                context.FromToCalculations.Add(calculationHeavyUnique);
                context.FromToCalculations.Add(calculationRegularDaily1);
                context.FromToCalculations.Add(calculationRegularDaily2);
                context.FromToCalculations.Add(calculationRegularUnique);
                context.FromToCalculations.Add(calculationSpecialisedDaily1);
                context.FromToCalculations.Add(calculationSpecialisedDaily2);
                context.Products.Add(caterBull);
                context.Products.Add(kamTruck);
                context.Products.Add(komatsuCrane);
                context.Products.Add(volvoSteamroller);
                context.Products.Add(boschJackhammer);
                #endregion

                #region Populate Users
                User user = new User()
                {
                    email = "",
                    fidelity_point = 0,
                    full_name = "Albert Einstein",
                    phone = "+6969696060",
                    address = "Princeton University, Princeton, NJ 08544, United States of America"
                };
                context.Users.Add(user);
                #endregion

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Return true if there is at least one product in the database. Otherwise return false.
        /// </summary>
        /// <returns></returns>
        static public bool CheckifDataExist()
        {
            return DBRequest.GetAllProducts().Count() > 0;
        }
    }
}