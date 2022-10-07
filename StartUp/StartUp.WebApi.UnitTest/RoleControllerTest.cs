using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using StartUp.DataAccess.Repositories;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class RoleControllerTest
    {
        private Mock<IRoleService> _serviceMock;
        private RoleController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IRoleService>(MockBehavior.Strict);
            _controller = new RoleController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingRoleTest()
        {
            var role = CreateRole();
            var expectedRole = new RoleBasicModel(role);
            _serviceMock.Setup(manager => manager.GetSpecificRole(It.IsAny<int>())).Returns(role);

            var response = _controller.GetRole(role.Id);
            var okResponseObject = response as OkObjectResult;

            Assert.AreEqual(expectedRole, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingRoleTest()
        {
            Role rol = CreateRole();
            _serviceMock.Setup(manager => manager.GetSpecificRole(It.IsAny<int>())).Returns((Role)null);

            _controller.GetRole(rol.Id);
        }
        
        [TestMethod]
        public void GetExistingRoleWithModelTest()
        {
            RoleSearchCriteriaModel roleModel = new RoleSearchCriteriaModel();
            roleModel.Permission = "Employee";
            List<Role> rolList = new List<Role>();
            Role rol = CreateRole();
            rolList.Add(rol);
            
            _serviceMock.Setup(manager => manager.GetAllRole(It.IsAny<RoleSearchCriteria>())).Returns(rolList);

            _controller.GetRole(roleModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingRoleWithModelTest()
        {
            RoleSearchCriteriaModel roleModel = new RoleSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No roles where found");

            _serviceMock.Setup(manager => manager.GetAllRole(It.IsAny<RoleSearchCriteria>())).Throws(exception);

            var response = _controller.GetRole(roleModel);
        }
        
        [TestMethod]
        public void CreateRoleTest()
        {
            RoleModel roleModel = CreateRoleModel();
            Role role = CreateRole();
            _serviceMock.Setup(server => server.CreateRole(It.IsAny<Role>())).Returns(role);

            var response = _controller.CreateRole(roleModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }

        [TestMethod]
        public void DeleteRoleTest()
        {
            Role role = CreateRole();
            _serviceMock.Setup(server => server.DeleteRole(role.Id));

            var response = _controller.Delete(role.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private Role CreateRole()
        {
            return new Role()
            {
                Id = 1,
                Permission = "Administrator"
            };
        }
        
        private RoleModel CreateRoleModel()
        {
            return new RoleModel()
            {
                Permission = "Administrator"
            };
        }
    }
}