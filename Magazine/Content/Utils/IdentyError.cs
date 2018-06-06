using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazine.Content.Utils
{
    public class IdentyError
    {

        //        {"Message":"La solicitud no es válida.",
        //"ModelState":{"":[
        //	"Las contraseñas deben tener al menos un carácter que no sea una letra ni un dígito. 

        //    Las contraseñas deben tener al menos una letra en minúscula('a' - 'z'). 

        //    Las contraseñas deben tener al menos una letra en mayúscula('A' - 'Z')."
        //]
        //    }
        //}
        [JsonProperty("Message")]
        public int Message { get; set; }
        [JsonProperty("ModelState")]
        public string ModelState { get; set; }
    }
}