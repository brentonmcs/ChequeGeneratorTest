using System;
using System.ComponentModel.DataAnnotations;

namespace SPHealthChequeConverter.Models
{
    public class ChequeSubmitDetails
    {
        [Required]
        public string Name { get; set; }

        [Range(0.01, Double.MaxValue, ErrorMessage = "Amount must be greater than $0")]        
        public double Amount { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime Date { get; set; }
    }
}