using System;
using System.Collections.Generic;
using System.Linq;
using YM.Purchasing.Requisitions.Actions;

namespace YM.Purchasing.Requisitions
{
    public class Requisition
    {
        #region Properties

        public string Id { get; private set; }
        public string DepartmentId { get; private set; }
        public int Year { get; private set; }
        public RequisitionStatus Status { get; private set; }
        public RequisitionType Type { get; private set; }
        public string Title { get; private set; }

        #endregion

        #region Property Setters

        public Requisition SetDepartmentId(string deptId)
        {
            if (string.IsNullOrWhiteSpace(deptId))
            {
                throw new ArgumentException("deptId is missing");
            }
            DepartmentId = deptId;
            return this;
        }

        public Requisition SetYear(int year)
        {
            if (year < DateTime.Now.Year - 1)
            {
                throw new ArgumentException("year is invalid");
            }
            Year = year;
            return this;
        }

        public Requisition SetStatus(RequisitionStatus status)
        {
            if (status == RequisitionStatus.None)
            {
                throw new ArgumentException("invalid status");
            }
            Status = status;
            return this;
        }

        public Requisition SetType(RequisitionType type)
        {
            if (type == RequisitionType.None)
            {
                throw new ArgumentException("invalid type");
            }
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

        #endregion

        EntityAction<Requisition>[] AuthorizedActions(string userId)
        {
            var actions = new List<EntityAction<Requisition>>();

            EntityAction<Requisition> action;

            action = new DraftAction();
            if (action.IsAuthorized(this, userId).IsAuthorized)
            {
                actions.Add(action);
            }

            return actions.ToArray();
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", DepartmentId, Type.ToString().First(), Year);
        }
    }
}
