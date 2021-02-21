using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TeaTime.Api.Dtos
{
    public class NationalParkDtos
    {
        //For this Key & Required We Have to install the package :: System.ComponentModel.DataAnnotations
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }

    }
}
