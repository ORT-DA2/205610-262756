using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.DataAccess;
using StartUp.DataAccess.Repositories;
using StartUp.Domain;
using StartUp.IDataAccess;
using System.Collections.Generic;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class AdministratorManagerTest
    {
        private Mock<StartUpContext> _startUpContextMock;
        private IRepository<Administrator> _administratorRepositoryMock;

        [TestInitialize]
        public void SetUp()
        {
            _startUpContextMock = new Mock<StartUpContext>(MockBehavior.Strict);
            _administratorRepositoryMock = new AdministratorRepository(_startUpContextMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _startUpContextMock.VerifyAll();
        }

        // Arrange: Creamos los mocks y se lo pasamos a los objetos que queremos testear
        // Act: Probamos/ejecutamos el metodo
        // Assert: Comprobamos que el resultado es el que esperamos

        // Dummy: No tiene comportamiento, ej: Pasar por parametro algo que no vamos a usar
        // Fake: Objetos que funcionan de verdad pero con algun shortcut que los hace no viables en prod. Ej BD en memoria 
        // Stubs: Devuelven algo por default, ya estan pre programados y no se verifica comportamiento
        // Spies: Como el stub pero tambien registra informacion extra
        // Mocks: Objetos que programamos nosotros para el test y es el unico que verifica comportamiento
        [TestMethod]
        public void GetAllAdministrator()
        {
            //private Mock<StartUpContext> _startUpContextMock;
        //private IRepository<Administrator> _administratorRepositoryMock;
        /*
        var listAdmin = GenerateDummyAdmin();
            var listAdminModels = listAdmin.Select(m => new AdministratorBasicModel(m)).ToList();
            var listSearchCriteria = new AdministratorSearchCriteriaModel();
            _startUpContextMock.Setup(a => a.GetAllAdministrator(It.IsAny<AdministratorSearchCriteria>())).Returns(listAdmin);

            var response = _adminController.GetAdministrator(listSearchCriteria);

            var okResponse = response as OkObjectResult;
            var retrievedAdmin = okResponse.Value as IEnumerable<AdministratorBasicModel>;
        
            CollectionAssert.AreEqual(listAdminModels, retrievedAdmin.ToList());*/

        }

        private List<Administrator> GenerateDummyAdmin() => new List<Administrator>()
        {
        new Administrator() { Address = "Yaguaron1560", Email = "correo1@gmail.com"  },
        new Administrator() { Address = "Presidenteberro2562", Email = "correo2@gmail.com"}
        };

    }
}
