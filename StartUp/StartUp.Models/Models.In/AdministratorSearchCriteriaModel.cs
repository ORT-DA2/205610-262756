﻿using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class AdministratorSearchCriteriaModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public DateTime? RegisterDate { get; set; }
        public Invitation? Invitation { get; set; }

        public AdministratorSearchCriteria ToEntity()
        {
            return new AdministratorSearchCriteria()
            {
                Email = this.Email,
                Password = this.Password,
                Address = this.Address,
                RegisterDate = this.RegisterDate,
                Invitation = this.Invitation
            };
        }
    }
}
