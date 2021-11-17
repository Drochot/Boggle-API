using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BoggleApi.Models
{
    public class BoggleBox
    {
        public Guid BoggleBoxId { get; set; }
        public List<List<Die>> Dice { get; set; }

    }
    public class Die
    {
        public char Value { get; set; }

        public Die(char value)
        {
            this.Value = value;
        }
    }
}
