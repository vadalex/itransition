using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{
    public class Drink
    {
        public Drink()
        {
            DrinkAmounts = new HashSet<DrinkAmount>();
        }

        public int DrinkID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(50, ErrorMessage = "Title cannot be longer than 50 characters")] 
        public string Title { get; set; }

        [Required(ErrorMessage = "Volume is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Incorrect volume")]        
        public int Volume { get; set; }

        [Required(ErrorMessage = "ABV is required.")]
        [Range(0.0, 100.0, ErrorMessage = "Incorrect ABV")]
        [DisplayFormat(DataFormatString = "{0:0.#} %", ApplyFormatInEditMode = true)]
        public double AlcoholByVolume { get; set; }

        public int Color { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageLink { get; set; }

        public int BottleShapeID { get; set; }

        public virtual BottleShape BottleShape { get; set; }

        public virtual ICollection<DrinkAmount> DrinkAmounts { get; set; }
    }
}