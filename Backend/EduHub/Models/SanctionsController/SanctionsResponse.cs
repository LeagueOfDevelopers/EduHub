using System.Collections.Generic;
using EduHubLibrary.Facades.Views;

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