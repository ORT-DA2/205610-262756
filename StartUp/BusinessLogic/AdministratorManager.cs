using IBusinessLogic;
using StartUp.Domain;
using StartUp.Exceptions;
using StartUp.Domain.SearchCriterias;
using System.Collections.Generic;
using System.Linq;
using System;
using StartUp.IDataAccess;
using System.Linq.Expressions;

namespace BusinessLogic
{
    public class AdministratorManager : IAdministratorManager
    {
        private readonly IRepository<Administrator> _adminRepository;

        public AdministratorManager(IRepository<Administrator> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public List<Administrator> GetAllAdministrator(AdministratorSearchCriteria searchCriteria)
        {
            var emailCriteria = searchCriteria.Email?.ToLower() ?? string.Empty;
            var passwordCriteria = searchCriteria.Password?.ToLower() ?? string.Empty;
            var addressCriteria = searchCriteria.Address?.ToLower() ?? string.Empty;
            var userNameCriteria = searchCriteria.Invitation.UserName?.ToLower() ?? string.Empty;
            var registerDateCriteria = searchCriteria.RegisterDate?.ToString() ?? string.Empty;

            Expression<Func<Administrator, bool>> adminFilter = admin =>
                admin.Email.ToLower().Contains(emailCriteria) &&
                admin.Password.ToLower().Contains(passwordCriteria) &&
                admin.Address.ToLower().Contains(addressCriteria) &&
                admin.Invitation.UserName.Contains(userNameCriteria) &&
                admin.RegisterDate.ToString().Contains(registerDateCriteria);

            return _adminRepository.GetAllExpression(adminFilter).ToList();
        }

        public Administrator GetSpecificAdministrator(string email)
        {
            //Administrator administratorSaved = _admins.FirstOrDefault(m => m.Email == email);
            /*
            if (administratorSaved == null)
            {
                throw new ResourceNotFoundException($"The admin {email} not exist");
            }*/
            return null;
        }

        public Administrator CreateAdministrator(Administrator admin)
        {
            admin.ValidOrFail();
            //_admins.Add(admin);
            return admin;
        }

        public Administrator UpdateAdministrator(int id, Administrator updatedAdministrator)
        {
            /*updatedMovie.ValidOrFail();
            var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

            if (movieSaved == null)
                throw new ResourceNotFoundException($"Could not find specified movie {id}");

            // Workaround - como no puedo editar el elemento directamente en List, lo elimino y lo vuelvo a insertar actualizado
            var newMovie = new Movie()
            {
                Id = movieSaved.Id,
                Description = updatedMovie.Description,
                Title = updatedMovie.Title
            };
            _movies.Remove(movieSaved);
            _movies.Add(newMovie);*/
            var newAdmin = new Administrator();

            return newAdmin;
        }

        public void DeleteAdministrator(int id)
        {
            //var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

            //if (movieSaved == null)
            throw new ResourceNotFoundException($"Could not find specified movie {id}");

            //_movies.Remove(movieSaved);
        }

        public Administrator UpdateAdministrator(string email, Administrator admin)
        {
            throw new System.NotImplementedException();
        }

        public Administrator DeleteAdministrator(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
