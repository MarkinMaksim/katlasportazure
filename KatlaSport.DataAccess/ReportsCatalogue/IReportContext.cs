﻿namespace KatlaSport.DataAccess.ReportsCatalogue
{
    public interface IReportContext
    {
        /// <summary>
        /// Gets a set of <see cref="Company"/> entities.
        /// </summary>
        IEntitySet<Report> Reports { get; }
    }
}
