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
    public class InvitationService : IInvitationService
    {

        private readonly IRepository<Invitation> _invitationRepository;
        private readonly IRepository<Pharmacy> _pharmacyRepository;

        public InvitationService(IRepository<Invitation> invitationRepository, IRepository<Pharmacy> pharmacyRepository)
        {
            _invitationRepository = invitationRepository;
            _pharmacyRepository = pharmacyRepository;
        }

        public List<Invitation> GetAllInvitation(InvitationSearchCriteria searchCriteria)
        {
            Expression<Func<Invitation, bool>> invitationFilter = invitation => true;

            List<Invitation> invitations = _invitationRepository.GetAllByExpression(invitationFilter).ToList();

            if(invitations.Count == 0)
            {
                throw new ResourceNotFoundException("No invitations where found");
            }

            return invitations;
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
        
        public Invitation GetSpecificInvitationByUserAndPass(string username, int code)
        {
            var invitationSaved = _invitationRepository.GetOneByExpression(i =>i.UserName == username && i.Code == code);
           
            if (invitationSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified invitation {username} and {code}");
            }

            return invitationSaved;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            invitation.IsValidInvitation();
            NotExistInDataBase(invitation);
            ValidateInvitationRoles(invitation);
            ValidatePharmacyExist(invitation);

            if (invitation.Rol != "administrator")
            {
                invitation.Pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == invitation.Pharmacy.Name);
            }
            CreateAndSave(invitation);
            
            return invitation;
        }

        public Invitation UpdateInvitation(int invitationId, Invitation updatedInvitation)
        {
            updatedInvitation.IsValidInvitation();

            var invitationStored = GetSpecificInvitation(invitationId);

            if (invitationStored.State == "Available")
            {
                invitationStored.Rol = updatedInvitation.Rol;
                invitationStored.UserName = updatedInvitation.UserName;
                if (updatedInvitation.Rol != "administrator")
                {
                    invitationStored.Pharmacy =
                        this._pharmacyRepository.GetOneByExpression(p => p.Name == updatedInvitation.Pharmacy.Name);
                }

                invitationStored.Code = updatedInvitation.Code;
            }
            else
            {
                throw new InputException($"The invitation {invitationStored.UserName} has already been used");
            }

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

        private void NotExistInDataBase(Invitation invitation)
        {
            var invitationSaved = _invitationRepository.GetOneByExpression(i => i.UserName == invitation.UserName);

            if (invitationSaved != null)
            {
                throw new InputException($"An invitation already exists for that user {invitation.UserName}");
            }
        }

        public int GenerateCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            Invitation invitation = _invitationRepository.GetOneByExpression(c => code == c.Code);
            if (invitation != null)
            {
                return GenerateCode();
            }
            return code;
        }

        private void ValidateInvitationRoles(Invitation invitation)
        {
            string roles = "administrator, owner, employee";
            if ((invitation.Rol.ToLower() == "owner" || invitation.Rol.ToLower() == "employee")
                && invitation.Pharmacy == null)
            {
                throw new InputException("The owner and the employee roles need a pharmacy");
            }
            if (!roles.Contains(invitation.Rol))
            {
                throw new InputException("The role is not valid");
            }
        }

        private void CreateAndSave(Invitation invitation)
        {
            if (invitation == null)
            {
                throw new InputException("The invitation is empty");
            }
            else
            {
                invitation.State = "Available";
                invitation.Code = GenerateCode();

                _invitationRepository.InsertOne(invitation);
                _invitationRepository.Save();
            }
        }

        private void ValidatePharmacyExist(Invitation invitation)
        {
            if (!invitation.Rol.Contains("administrator"))
            {
                var pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == invitation.Pharmacy.Name);

                if (pharmacy == null)
                {
                    throw new ResourceNotFoundException($"The pharmacy for which an invitation is being created does not exist");
                }

            }
        }
    }
}
