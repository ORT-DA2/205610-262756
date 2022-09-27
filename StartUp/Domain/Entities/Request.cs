using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Request
    {
        public int Id { get; set; }
        public List<Petition> Petitions { get; set; }
        public bool State { get; set; }

        public void isValidRequest()
        {
            if (Petitions == null)
                throw new InputException("Petitions empty");
        }
    }
}
