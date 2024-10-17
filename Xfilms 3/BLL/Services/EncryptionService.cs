using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EncryptionService
    {
        private byte[]? _salt;
        private int _iteration;
        public string? Pass { get; set; }
        public string? SaltDb { get; private set; }
        public string? PassDb { get; private set; }

        public string IncomeSalt { private get; set; }

        public void HashPass()
        {
            EncryptionPass();
        }

        private void Salt()
        {
            _salt = RandomNumberGenerator.GetBytes(128 / 8);
        }
        private void EncryptionPass()
        {
            Salt();
            var hash = new Rfc2898DeriveBytes(Pass, _salt, _iteration);
            PassDb = Convert.ToBase64String(hash.GetBytes(_iteration));
            SaltDb = Convert.ToBase64String(_salt);
        }

        public string DecryptionPass()
        {
            _salt = Convert.FromBase64String(IncomeSalt);
            var hash = new Rfc2898DeriveBytes(Pass, _salt, _iteration);
            var passCheck = Convert.ToBase64String(hash.GetBytes(_iteration));
            return passCheck;
        }
        public EncryptionService() => _iteration = 15;
    }
}
