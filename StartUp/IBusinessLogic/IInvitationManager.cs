using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IInvitationManager
    {
        List<Invitation> GetAllInvitation(InvitationSearchCriteria searchCriteria);
        Invitation GetSpecificInvitation();
        Invitation CreateInvitation();

    }
}
