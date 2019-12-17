﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SellersWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        
        [Display(Name="Nome")]
        public string Name { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Display(Name = "Data de aniversario")] //
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:DD/MM/YYYY}")]
        public DateTime BirthDate { get; set; }

        [Display(Name="Salario Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        [Display(Name="Departamento")]
        public Department Department { get; set; } //associação
        public int DepartmentId { get; set; } // com o "Id" o framework vai identificar que o é para ter um id que seja gerado automaticamente.
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();  //associação de varios

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime BirthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            this.BirthDate = BirthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public Double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
