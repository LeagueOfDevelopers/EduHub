using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace EduHub.Models.ValidationAttributes
{
    public class ListLength : ValidationAttribute
    {
        private readonly int _maxLength;
        private readonly int _minLength;

        public ListLength(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;

            if (list != null) return list.Count <= _maxLength && list.Count >= _minLength;

            return false;
        }
    }
}