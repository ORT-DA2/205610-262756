using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class InvitationManager : IInvitationManager
    {
        public Invitation CreateInvitation()
        {
            throw new NotImplementedException();
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public Invitation DeleteInvitation(string name)
        {
            throw new NotImplementedException();
        }

        public List<Invitation> GetAllInvitation(InvitationSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Invitation GetSpecificInvitation()
        {
            throw new NotImplementedException();
        }

        public Invitation GetSpecificInvitation(string username)
        {
            throw new NotImplementedException();
        }

        public Invitation UpdateInvitation(string name, Invitation invitation)
        {
            throw new NotImplementedException();
        }
    }
}
