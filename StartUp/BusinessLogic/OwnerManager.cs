using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class OwnerManager : IOwnerManager
    {
        private readonly IRepository<Owner> _ownerRepository;

        public OwnerManager(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public List<Owner> GetAllOwner(OwnerSearchCriteria searchCriteria)
        {
            var emailCriteria = searchCriteria.Email?.ToLower() ?? string.Empty;
            var addressCriteria = searchCriteria.Address?.ToLower() ?? string.Empty;
            //   HAY QUE AGREGAR farmacia, invitacion y registerdate`?????????
            var regiterDateCriteria = searchCriteria.RegisterDate?.ToString() ?? string.Empty;
            var pharmacyCriteria = searchCriteria.Pharmacy.Name?.ToString() ?? string.Empty;
            var invitationCriteria = searchCriteria.Invitation.UserName?.ToString() ?? string.Empty;

            Expression<Func<Owner, bool>> ownerFilter = owner =>
                owner.Email.ToLower().Contains(emailCriteria) &&
                owner.Address.ToLower().Contains(addressCriteria) &&
                owner.RegisterDate.ToString().Contains(regiterDateCriteria) &&
                owner.Pharmacy.Name.ToLower().Contains(pharmacyCriteria) &&
                owner.Invitation.UserName.ToLower().Contains(invitationCriteria);

            return _ownerRepository.GetAllExpression(ownerFilter).ToList();
        }

        public Owner GetSpecificOwner(int ownerId)
        {
            var ownerSaved = _ownerRepository.GetOneByExpression(o => o.Id == ownerId);

            if (ownerSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified owner {ownerId}");
            }

            return ownerSaved;
        }

        public Owner CreateOwner(Owner owner)
        {
            owner.isValidOwner();

            _ownerRepository.InsertOne(owner);
            _ownerRepository.Save();

            return owner;
        }

        public Owner UpdateOwner(int ownerId, Owner updatedOwner)
        {
            updatedOwner.isValidOwner();

            var ownerStored = GetSpecificOwner(ownerId);

            ownerStored.Pharmacy = updatedOwner.Pharmacy;
            ownerStored.Email = updatedOwner.Email;
            ownerStored.Password = updatedOwner.Password;
            ownerStored.Address = updatedOwner.Address;
            ownerStored.RegisterDate = updatedOwner.RegisterDate;
            ownerStored.Password = updatedOwner.Password;
            ownerStored.Invitation = updatedOwner.Invitation;

            _ownerRepository.UpdateOne(ownerStored);
            _ownerRepository.Save();

            return ownerStored;
        }

        public void DeleteOwner(int ownerId)
        {
            var ownerStored = GetSpecificOwner(ownerId);

            _ownerRepository.DeleteOne(ownerStored);
            _ownerRepository.Save();
        }

    }
}
