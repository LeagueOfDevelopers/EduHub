﻿using System;
using System.ComponentModel.DataAnnotations;

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
            var birthDate = (DateTimeOffset) value;
            var current = DateTime.Now.Year;
            return (birthDate.Year >= _startYear && birthDate.Year <= current)
                   || birthDate.CompareTo(DateTimeOffset.MinValue) == 0;
        }
    }
}