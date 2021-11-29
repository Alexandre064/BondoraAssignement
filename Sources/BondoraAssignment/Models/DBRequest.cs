using BondoraAssignment.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BondoraAssignment.Models
{
    public class DBRequest
    {
        static private BondoraAssignementDBEntities db = new BondoraAssignementDBEntities();

        /// <summary>
        /// Return all product from database
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<Product> GetAllProducts()
        {
            return from p in db.Products select p;
        }

        /// <summary>
        /// Return all users from database
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<User> GetUsers()
        {
            return from p in db.Users select p;
        }

        /// <summary>
        /// Search the database with a specific product id
        /// </summary>
        /// <param name="selectedid"></param>
        /// <returns>Return the product if it exist, otherwise return null</returns>
        internal async static Task<Product> GetProductById(int selectedid)
        {
            if (selectedid <= 0)
                return null;
            return await db.Products.FindAsync(selectedid);
        }

        /// <summary>
        /// Search the database with a specific user id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>Return the user if it exist, otherwise return null</returns>
        internal async static Task<User> GetUserById(int? userid)
        {
            if (userid <= 0)
                return null;
            return await db.Users.FindAsync(userid);
        }
        
        /// <summary>
        /// Add a product and the number of day to rent to an user's cart
        /// </summary>
        /// <param name="selectedItemId"></param>
        /// <param name="numberOfDaysToRent"></param>
        /// <param name="userid"></param>
        /// <returns>True if no problem, False otherwise</returns>
        public static async Task<bool> AddItemToUserCart(int selectedItemId, int numberOfDaysToRent, int userid)
        {
            try
            {
                User user = await Task.Run(() => GetUserById(userid));
                Product product = await Task.Run(() => GetProductById(selectedItemId));

                user.CartDetails.Add(new CartDetail()
                {
                    Product = product,
                    number_of_day = numberOfDaysToRent
                });

                db.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove a product from an user's cart
        /// </summary>
        /// <param name="productidincart"></param>
        /// <param name="userid"></param>
        /// <returns>True if no problem, False otherwise</returns>
        public async static Task<bool> RemoveItemFromUserCart(int productidincart, int userid)
        {
            try
            {
                User user = await GetUserById(userid);
                RemoveItem(productidincart, user);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove the product from the user's cart
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="user"></param>
        private static void RemoveItem(int productid, User user)
        {
            CartDetail p = user.CartDetails.Single(u => u.id == productid);

            user.CartDetails.Remove(p);

            db.SaveChanges();
        }

        /// <summary>
        /// Copy all data from user cart into the order details of the user to confirm the command
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="totalPrice"></param>
        /// <param name="totalFidelityPoint"></param>
        /// <returns>True if everything went well, otherwise return false</returns>
        public static async Task<bool> ConfirmCommandForUser(int userid, int? totalPrice, int? totalFidelityPoint)
        {

            try
            {
                User user = await GetUserById(userid);
                OrderDetail orderDetail = new OrderDetail()
                {
                    date = DateTime.Now,
                };
                foreach(CartDetail cartDetail in user.CartDetails)
                {
                    ProductOrderDetail productOrderDetail = new ProductOrderDetail()
                    {
                        Product = cartDetail.Product,
                        number_of_day = cartDetail.number_of_day,
                    };

                    orderDetail.ProductOrderDetails.Add(productOrderDetail);
                }
                Order order = new Order()
                {
                    total_fidelity_point = totalFidelityPoint,
                    total_price = totalPrice,
                };

                orderDetail.Order = order;

                user.OrderDetails.Add(orderDetail);

                
                order.OrderDetails.Add(orderDetail);

                db.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Add fidelity point to the users
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fidelityPoint"></param>
        public static void AddFidelityPoint(User user, int? fidelityPoint)
        {
            user.fidelity_point += fidelityPoint;
            db.SaveChanges();
        }

        /// <summary>
        /// Remove all item from the user's cart
        /// </summary>
        /// <param name="user"></param>
        public static void RemoveAllItemFromUserCart(User user)
        {
            user.CartDetails.Clear();
        }
    }
}