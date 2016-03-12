﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataModel;
using DataModel.GenericRepository;
using DataModel.UnitOfWork;
using Moq;
using NUnit.Framework;
using TestsHelper;

namespace BusinessServices.Tests
{
    public class UserServicesTest
    {
        #region Variables

        private IUserServices _userServices;
        private IUnitOfWork _unitOfWork;
        private List<SysUser> _users;
        private GenericRepository<SysUser> _userRepository;
        private WebApiDbEntities _dbEntities;
        const string CorrectUserName = "arsh";
        const string CorrectPassword = "arsh";
        const string WrongUserName = "arsh1";
        const string WrongPassword = "arsh1";
        #endregion

        #region Test fixture setup

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [TestFixtureSetUp]
        public void Setup()
        {
            _users = SetUpUsers();
        }

        #endregion

        #region Setup

        /// <summary>
        /// Re-initializes test.
        /// </summary>
        [SetUp]
        public void ReInitializeTest()
        {
            _dbEntities = new Mock<WebApiDbEntities>().Object;
            _userRepository = SetUpUserRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.UserRepository).Returns(_userRepository);
            _unitOfWork = unitOfWork.Object;
            _userServices = new UserServices(_unitOfWork);
        }

        #endregion

        #region Private member methods

        /// <summary>
        /// Setup dummy repository
        /// </summary>
        /// <returns></returns>
        private GenericRepository<SysUser> SetUpUserRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<GenericRepository<SysUser>>(MockBehavior.Default, _dbEntities);

            // Setup mocking behavior
            mockRepo.Setup(p => p.GetAll()).Returns(_users);

            //mockRepo.Setup(s => s.Get(It.IsAny<Func<User, bool>>()))
            //    .Returns(
            //        (Func<User, bool> expr) =>
            //        DataInitializer.GetAllUsers().Where(u => u.UserName == CorrectUserName).FirstOrDefault(
            //            u => u.Password == CorrectPassword));

            //mockRepo.Setup(s => s.Get(It.IsAny<Func<User, bool>>()))
            //   .Returns(
            //       (Func<User, bool> expr) =>
            //       DataInitializer.GetAllUsers().Where(u => u.UserName == WrongUserName).FirstOrDefault(
            //           u => u.Password == WrongPassword));

            mockRepo.Setup(p => p.GetByID(It.IsAny<int>()))
                .Returns(new Func<int, SysUser>(
                             id => _users.Find(p => p.Id.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<SysUser>())))
                .Callback(new Action<SysUser>(newToken =>
                {
                    dynamic maxTokenID = _users.Last().Id;
                    dynamic nextTokenID = maxTokenID + 1;
                    newToken.Id = nextTokenID;
                    _users.Add(newToken);
                }));

            mockRepo.Setup(p => p.Update(It.IsAny<SysUser>()))
                .Callback(new Action<SysUser>(token =>
                {
                    var oldUser = _users.Find(a => a.Id == token.Id);
                    oldUser = token;
                }));

            mockRepo.Setup(p => p.Delete(It.IsAny<SysUser>()))
                .Callback(new Action<SysUser>(prod =>
                {
                    var userToRemove =
                        _users.Find(a => a.Id == prod.Id);

                    if (userToRemove != null)
                        _users.Remove(userToRemove);
                }));
            //Create setup for other methods too. note non virtauls methods can not be set up

            // Return mock implementation object
            return mockRepo.Object;
        }

        /// <summary>
        /// Setup dummy tokens data
        /// </summary>
        /// <returns></returns>
        private static List<SysUser> SetUpUsers()
        {
            var userId = new int();
            var users = DataInitializer.GetAllUsers();
            foreach (SysUser user in users)
                user.Id = ++userId;
            return users;
        }

        #endregion

        #region Unit Tests

        ///// <summary>
        ///// Authenticate with correct credentials
        ///// </summary>
        //[Test]
        //public void AuthenticateTest()
        //{

        //    var returnId = _userServices.Authenticate(CorrectUserName, CorrectPassword);
        //    var firstOrDefault = _users.Where(u => u.UserName == CorrectUserName).FirstOrDefault(u => u.Password == CorrectPassword);
        //    if (firstOrDefault != null)
        //        Assert.That(returnId, Is.EqualTo(firstOrDefault.Id));
        //}

        ///// <summary>
        ///// Authenticate with correct credentials
        ///// </summary>
        //[Test]
        //public void AuthenticateWrongCredentialsTest()
        //{
           
        //    var returnId = _userServices.Authenticate(WrongUserName, WrongPassword);
        //    var firstOrDefault = _users.Where(u => u.UserName == WrongUserName).FirstOrDefault(u => u.Password == WrongPassword);
        //    Assert.That(returnId, firstOrDefault != null ? Is.EqualTo(firstOrDefault.Id) : Is.EqualTo(0));
        //}

        #endregion

        #region Tear Down

        /// <summary>
        /// Tears down each test data
        /// </summary>
        [TearDown]
        public void DisposeTest()
        {
            _userServices = null;
            _unitOfWork = null;
            _userRepository = null;
            if (_dbEntities != null)
                _dbEntities.Dispose();
        }

        #endregion

        #region TestFixture TearDown.

        /// <summary>
        /// TestFixture teardown
        /// </summary>
        [TestFixtureTearDown]
        public void DisposeAllObjects()
        {
            _users = null;
        }

        #endregion
    }
}
