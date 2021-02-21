using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TeaTime.Api.DTOS
{
    public class NationalParkDtos
    {
        //For this Key & Required We Have to install the package :: System.ComponentModel.DataAnnotations
      
        public int Id { get; set; }
       [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }

    }
}
