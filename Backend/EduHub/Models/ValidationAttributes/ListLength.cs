using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.ValidationAttributes
{
    public class ListLength : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public ListLength(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;

            if(list != null)
            { 
                return list.Count <= _maxLength && list.Count >= _minLength;
            }

            return false;
        }
    }
}
