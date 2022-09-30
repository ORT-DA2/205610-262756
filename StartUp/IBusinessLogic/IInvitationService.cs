using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IInvitationService
    {
        List<Invitation> GetAllInvitation(InvitationSearchCriteria searchCriteria);
        Invitation GetSpecificInvitation(int invitationId);
        Invitation CreateInvitation(Invitation invitation);
        Invitation UpdateInvitation(int invitationId, Invitation invitation);
        void DeleteInvitation(int invitationId);

    }
}
