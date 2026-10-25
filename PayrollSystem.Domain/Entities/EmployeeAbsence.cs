namespace PayrollSystem.Domain.Entities
{
    public class EmployeeAbsence
    {
        public int Id { get; private set; }
        public int Year{ get; private set; }
        public int Month{ get; private set; }
        public int AbsenceDaysCount { get; private set; }
        public int EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;
        private EmployeeAbsence()
        {
            
        }

        public EmployeeAbsence(int year, int month, int absenceDaysCount, int employeeId)
        {

            if(month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");
            }
            if (employeeId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(employeeId), "Valid employee ID is required");
            }

            EmployeeId = employeeId;
            Year = year;
            Month = month;

            UpdateAbsenceDays(absenceDaysCount);
    }

        public void UpdateAbsenceDays(int absenceDaysCount)
        {
            if(absenceDaysCount <= 0 || absenceDaysCount > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(absenceDaysCount), "Absence days must be between 0 and 31.");
            }

            AbsenceDaysCount = absenceDaysCount;
        }
    }
}