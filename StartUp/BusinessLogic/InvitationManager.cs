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
    public class InvitationManager : IInvitationManager
    {

        private readonly IRepository<Invitation> _invitationRepository;

        public InvitationManager(IRepository<Invitation> invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public List<Invitation> GetAllInvitation(InvitationSearchCriteria searchCriteria)
        {
            var rolCriteria = searchCriteria.Rol?.ToLower() ?? string.Empty;
            var userNameCriteria = searchCriteria.UserName?.ToLower() ?? string.Empty;
            var codeCriteria = searchCriteria.Code.ToString()?.ToLower() ?? string.Empty;
            var isActiveCriteria = searchCriteria.State.ToLower() ?? string.Empty;
            var pharmacyCriteria = searchCriteria.Pharmacy ?? null;

            Expression<Func<Invitation, bool>> invitationFilter = invitation =>
                invitation.Rol.ToLower().Contains(rolCriteria) &&
                invitation.UserName.ToLower().Contains(userNameCriteria) &&
                invitation.Code.ToString().Contains(codeCriteria) &&
                invitation.State.Contains(isActiveCriteria) &&
                invitation.Pharmacy == pharmacyCriteria;

            return _invitationRepository.GetAllExpression(invitationFilter).ToList();
        }

        public Invitation GetSpecificInvitation(int invitationId)
        {
            var invitationSaved = _invitationRepository.GetOneByExpression(i => i.Id == invitationId);

            if (invitationSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified invitation {invitationId}");
            }

            return invitationSaved;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            invitation.IsValidInvitation();
            NotExistInDataBase(invitation);
            invitation.SetCodeAndState();

            _invitationRepository.InsertOne(invitation);
            _invitationRepository.Save();

            return invitation;
        }

        public Invitation UpdateInvitation(int invitationId, Invitation updatedInvitation)
        {
            updatedInvitation.IsValidInvitation();

            var invitationStored = GetSpecificInvitation(invitationId);

            invitationStored.Rol = updatedInvitation.Rol;
            invitationStored.UserName = updatedInvitation.UserName;
            invitationStored.Pharmacy = updatedInvitation.Pharmacy;

            _invitationRepository.UpdateOne(invitationStored);
            _invitationRepository.Save();

            return invitationStored;
        }

        public void DeleteInvitation(int invitationId)
        {
            var invitationStored = GetSpecificInvitation(invitationId);

            _invitationRepository.DeleteOne(invitationStored);
            _invitationRepository.Save();
        }

        ///////////////////////////
        public void NotExistInDataBase(Invitation invitation)
        {
            var invitationSaved = _invitationRepository.GetOneByExpression(i => i == invitation);

            if (invitationSaved != null)
            {
                throw new InputException($"An invitation already exists for that user {invitation.UserName}");
            }
        }
    }
}
