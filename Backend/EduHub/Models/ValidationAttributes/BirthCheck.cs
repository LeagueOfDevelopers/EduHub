using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.ValidationAttributes
{
    public class BirthCheck : ValidationAttribute
    {
        private readonly int _startYear;

        public BirthCheck(int startYear)
        {
            _startYear = startYear;
        }

        public override bool IsValid(object value)
        {
            var year = Convert.ToInt32(value);
            var current = DateTime.Now.Year;
            return year >= _startYear && year <= current;
        }
    }
}
