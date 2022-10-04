using StartUp.Domain.SearchCriterias;

namespace StartUp.Models.Models.In
{
    public class RequestSearchCriteriaModel
    {
        public string? State { get; set; }

        public RequestSearchCriteria ToEntity()
        {
            return new RequestSearchCriteria()
            {
                State = this.State
            };
        }
    }
}
