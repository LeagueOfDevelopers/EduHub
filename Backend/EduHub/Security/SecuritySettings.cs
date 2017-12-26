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
        public SecuritySettings(Credentials adminCredentinals, string encryptionKey, string issue, TimeSpan expirationPeriod)
        {
            AdminCredentinals = adminCredentinals;
            EncryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionKey));
            Issue = issue;
            ExpirationPeriod = expirationPeriod;
        }

        public Credentials AdminCredentinals { get; }
        public SymmetricSecurityKey EncryptionKey { get; }
        public string Issue { get; }
        public TimeSpan ExpirationPeriod { get; }
    }
}
