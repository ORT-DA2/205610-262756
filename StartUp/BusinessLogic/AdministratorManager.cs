using IBusinessLogic;
using StartUp.Domain;
using StartUp.Exceptions;
using StartUp.Domain.SearchCriterias;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class AdministratorManager : IAdministratorManager
    {
        private static List<Administrator> _admins = new List<Administrator>()
    {
        new Administrator() { Email = "admin1@gmail.com"},
        new Administrator() { Email = "admin2@gmail.com"}
    };

        public List<Administrator> GetAllAdministrator(AdministratorSearchCriteria searchCriteria)
        {
            return _admins;
        }

        public Administrator GetSpecificAdministrator(string email)
        {
            var administratorSaved = _admins.FirstOrDefault(m => m.Email == email);

            if (administratorSaved == null)
                throw new ResourceNotFoundException($"Could not find specified admin {email}");

            return administratorSaved;
        }

        public Administrator CreateAdministrator(Administrator admin)
        {
            //movie.ValidOrFail();
            //movie.Id = _movies.Count() + 1;
            //_movies.Add(movie);
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

    }
}
