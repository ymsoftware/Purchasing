using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YM.Purchasing.Requisitions
{
    public class Requisition
    {
        public string Id { get; private set; }
        public string DepartmentId { get; private set; }
        public int Year { get; private set; }
        public RequisitionStatus Status { get; private set; }
        public RequisitionType Type { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }

        public Requisition(string deptId, int year)
        {
            if (string.IsNullOrWhiteSpace(deptId))
            {
                throw new ArgumentException("deptId is missing");
            }

            if (year < DateTime.Now.Year - 1)
            {
                throw new ArgumentException("year is invalid");
            }

            DepartmentId = deptId;
            Year = year;
        }

        public Requisition(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is missing");
            }

            var tokens = name.Split('-'); //010020-F-2015
            if (tokens.Length != 3)
            {
                throw new ArgumentException("name is invalid");
            }

            string deptId = tokens[0];
            if (string.IsNullOrWhiteSpace(deptId))
            {
                throw new ArgumentException("deptId is missing");
            }

            string type = tokens[1];
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("type is missing");
            }

            type = type.ToLower();

            RequisitionType rt = type == "f" ? RequisitionType.Formal : type == "i" ? RequisitionType.Informal : RequisitionType.None;
            if (rt == RequisitionType.None)
            {
                throw new ArgumentException("type is invalid");
            }

            int year;
            if (!int.TryParse(tokens[2], out year))
            {
                throw new ArgumentException("year is invalid");
            }

            DepartmentId = deptId;
            Type = rt;
            Year = year;
        }

        public Requisition SetType(RequisitionType type)
        {
            Type = type;
            return this;
        }

        public Requisition SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("title is missing");
            }
            Title = title;
            return this;
        }

        public ExecutionResult Draft(string userId)
        {
            Status = RequisitionStatus.Draft;

            return ExecutionResult.Success();
        }
    }
}
