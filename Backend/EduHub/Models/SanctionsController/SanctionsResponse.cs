using EduHubLibrary.Domain;
using EduHubLibrary.Facades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.SanctionsController
{
    public class SanctionsResponse
    {
        public SanctionsResponse(IEnumerable<SanctionView> sanctions)
        {
            Sanctions = sanctions;
        }

        public IEnumerable<SanctionView> Sanctions { get; }
    }
}
