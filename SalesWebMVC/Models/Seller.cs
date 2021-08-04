using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        #region ID
       
        //Validadetion
        [Required(ErrorMessage = "{0} required  !! ")]
        public int Id { get; set; }
        [StringLength(60,MinimumLength =3,ErrorMessage ="{0} size should be between {2} and {1}")]
        #endregion

        #region Name
       
        [Required(ErrorMessage = "{0} required  !! ")]
        public string Name { get; set; }
        #endregion


        #region Email
        [Required(ErrorMessage = "{0} required  !! ")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        #endregion



        #region BaseSalary
        //FORMATAÇÕES NO DISPLAY 
       
        [Required(ErrorMessage = "{0} required  !! ")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "$ "+ "{0:F2}")]
        public double BaseSalary { get; set; }

        #endregion



        #region BirthDate

        //Display's custom 
        [Required(ErrorMessage = "{0} required  !! ")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        #endregion


        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr) { Sales.Remove(sr); }
        public double TotalSales (DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr =>sr.Amount);
        }

    }

}
