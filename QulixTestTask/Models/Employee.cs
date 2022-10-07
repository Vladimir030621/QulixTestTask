using QulixTestTask.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QulixTestTask.Models
{
    public class Employee
    {
        private Dictionary<int, string> positions;
        public Employee()
        {
            positions = Positions.AllPositions();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime StartDate { get; set; }
        public int PositionTypeId { get; set; }
        public string PositionType
        {
            get
            {
                return positions.ContainsKey(PositionTypeId)  ? positions[PositionTypeId] : "No position";
            }
        }

        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
