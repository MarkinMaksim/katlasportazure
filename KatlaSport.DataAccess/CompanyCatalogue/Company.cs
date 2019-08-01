﻿using System.Collections.Generic;
using KatlaSport.DataAccess.ReportsCatalogue;

namespace KatlaSport.DataAccess.CompanyCatalogue
{
    public class Company
    {
        /// <summary>
        /// Gets or sets a accountant ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a accountant Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a accountant Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets a accountant city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a accountant city.
        /// </summary>
        public string Country { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
