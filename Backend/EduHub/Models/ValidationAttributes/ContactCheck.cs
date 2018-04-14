using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.ValidationAttributes
{
    public class ContactCheck : ValidationAttribute
    { 

        public override bool IsValid(object value)
        {
            var list = value as IList<string>;
            if (list != null)
            {
                list = list.ToList();
                return true;
            }

            return false;
        }
    }
}
