using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EduHubLibrary.Common
{
    public class Credentials
    {
        public string Email { get; }
        public string PasswordHash { get; }

        public Credentials(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
        
        public static Credentials FromRawData(string email, string rawPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
                var passwordHash = Encoding.UTF8.GetString(hashBytes);
                return new Credentials(email, passwordHash);
            }
        }

        public override bool Equals(object obj)
        {
            var credentials = obj as Credentials;
            return credentials != null &&
                   Email == credentials.Email &&
                   PasswordHash == credentials.PasswordHash;
        }

        public override int GetHashCode()
        {
            var hashCode = 93515719;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PasswordHash);
            return hashCode;
        }
        
        public static bool operator ==(Credentials credentials1, Credentials credentials2)
        {
            return EqualityComparer<Credentials>.Default.Equals(credentials1, credentials2);
        }

        public static bool operator !=(Credentials credentials1, Credentials credentials2)
        {
            return !(credentials1 == credentials2);
        }
    }
}
