using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                DateTime dueDate = (DateTime)value;
                if (dueDate.Date >= DateTime.Now.Date)
                {
                    return true;
                }
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} cannot be in the past.";
        }
    }
}
