using EduHubLibrary.Common;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHub.Security
{
    public class SecuritySettings
    {
        public SecuritySettings(string encryptionKey, string issue, TimeSpan expirationPeriod)
        {
            EncryptionKey = encryptionKey;
            Issue = issue;
            ExpirationPeriod = expirationPeriod;
        }
        
        public string EncryptionKey { get; }
        public string Issue { get; }
        public TimeSpan ExpirationPeriod { get; }
    }
}
