using System;
using System.Collections.Generic;

namespace API.Models
{
    // Modelos devueltos por las acciones de AccountController.

    public class ExternalLoginViewModel
    {
        /// <summary>
        /// Nombre (cadena)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Url (cadena)
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// State (cadena)
        /// </summary>
        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        /// <summary>
        /// LocalLoginProvider (cadena)
        /// </summary>
        public string LocalLoginProvider { get; set; }
        /// <summary>
        /// Email (cadena)
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Logins (List UserLoginInfoViewModel)
        /// </summary>
        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }
        /// <summary>
        /// ExternalLoginProviders (List ExternalLoginViewModel)
        /// </summary>
        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        /// <summary>
        /// Email (Cadena)
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// HasRegistered (Cadena)
        /// </summary>
        public bool HasRegistered { get; set; }
        /// <summary>
        /// LoginProvider (Cadena)
        /// </summary>
        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        /// <summary>
        /// LoginProvider (Cadena)
        /// </summary>
        public string LoginProvider { get; set; }
        /// <summary>
        /// ProviderKey (Cadena)
        /// </summary>
        public string ProviderKey { get; set; }
    }
}
