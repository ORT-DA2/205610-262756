using StartUp.Domain.SearchCriterias;

namespace StartUp.Models.Models.In
{
    public class RequestSearchCriteriaModels
    {
        public bool? State { get; set; }

        public RequestSearchCriteria ToEntity()
        {
            return new RequestSearchCriteria()
            {
                State = this.State
            };
        }
    }
}
