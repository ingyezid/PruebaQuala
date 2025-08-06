using System;
   using System.ComponentModel.DataAnnotations;
   using System.Globalization;

namespace PruebaQuala.DataAnnotations
{
    public class FechaPosteriorAttribute : RangeAttribute
    {
        public FechaPosteriorAttribute() : base(typeof(DateTime), DateTime.Now.ToString("yyyy-MM-dd"), DateTime.MaxValue.ToString("yyyy-MM-dd"))
        {
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime fecha)
            {
                return fecha > DateTime.Now;
            }

            return false;
        }
    }
}
