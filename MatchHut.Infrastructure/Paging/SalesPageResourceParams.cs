﻿using System;

namespace MatchHut.Infrastructure.Paging
{
    public class SalesFilter : Filter
    {
        public DateTime? SalesDate { get; set; }
    }

    public class SalesPageResourceParams : PageResourceParams<SalesFilter>
    {
        public DateTime? SalesDate => _filterObj?.SalesDate;
    }
}