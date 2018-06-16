using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models
{
    // Modelos usados como parámetros para las acciones de AccountController.

    public class AddExternalLoginBindingModel
    {
        /// <summary>
        /// Token de acceso externo
        /// </summary>
        [Required]
        [Display(Name = "Token de acceso externo")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        /// <summary>
        /// Contraseña actual
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }
        /// <summary>
        /// Nueva contraseña
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }
        /// <summary>
        /// Confirmar la nueva contraseña
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        /// <summary>
        /// Correo electrónico
        /// </summary>
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
        /// <summary>
        /// Contraseña
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        /// <summary>
        /// Confirmar contraseña
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        /// <summary>
        /// Correo electrónico
        /// </summary>
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        /// <summary>
        /// Proveedor de inicio de sesión
        /// </summary>
        [Required]
        [Display(Name = "Proveedor de inicio de sesión")]
        public string LoginProvider { get; set; }
        /// <summary>
        /// Clave de proveedor
        /// </summary>
        [Required]
        [Display(Name = "Clave de proveedor")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        /// <summary>
        /// Nueva contraseña
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }
        /// <summary>
        /// Confirmar la nueva contraseña
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
