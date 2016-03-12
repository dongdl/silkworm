using System;
using System.Collections.Generic;
using ATEC.Core.DataModel;
using DataModel;

namespace TestsHelper
{
    /// <summary>
    /// Data initializer for unit tests
    /// </summary>
    public class DataInitializer
    {
        /// <summary>
        /// Dummy products
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetAllProducts()
        {
            var products = new List<Product>
                               {
                                   new Product() {Name = "Laptop"},
                                   new Product() {Name = "Mobile"},
                                   new Product() {Name = "HardDrive"},
                                   new Product() {Name = "IPhone"},
                                   new Product() {Name = "IPad"}
                               };
            return products;
        }

        /// <summary>
        /// Dummy tokens
        /// </summary>
        /// <returns></returns>
        public static List<Token> GetAllTokens()
        {
            var tokens = new List<Token>
                               {
                                   new Token()
                                       {
                                           AuthToken = "9f907bdf-f6de-425d-be5b-b4852eb77761",
                                           ExpiresOn = DateTime.Now.AddHours(2),
                                           IssuedOn = DateTime.Now,
                                           UserId = 1
                                       },
                                   new Token()
                                       {
                                           AuthToken = "9f907bdf-f6de-425d-be5b-b4852eb77762",
                                           ExpiresOn = DateTime.Now.AddHours(1),
                                           IssuedOn = DateTime.Now,
                                           UserId = 2
                                       }
                               };
                               
            return tokens;
        }

        /// <summary>
        /// Dummy users
        /// </summary>
        /// <returns></returns>
        public static List<SysUser> GetAllUsers()
        {
            var users = new List<SysUser>
                               {
                                   new SysUser()
                                       {
                                           UserName = "akhil",
                                           Password = "akhil",
                                           FullName = "Akhil Mittal",
                                       },
                                   new SysUser()
                                       {
                                           UserName = "arsh",
                                           Password = "arsh",
                                           FullName = "Arsh Mittal",
                                       },
                                   new SysUser()
                                       {
                                           UserName = "divit",
                                           Password = "divit",
                                           FullName = "Divit Agarwal",
                                       }
                               };

            return users;
        }

    }
}
