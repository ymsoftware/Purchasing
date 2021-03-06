﻿using System;
using System.Linq;
using YM.Purchasing.Requisitions.Actions;

namespace YM.Purchasing.Requisitions
{
    public class Requisition : IRequisition
    {
        #region Properties

        public string Id { get; private set; }
        public string DepartmentId { get; private set; }
        public int Year { get; private set; }
        public int Sequence { get; private set; }
        public RequisitionStatus Status { get; private set; }
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

        public IEntityAction<IRequisition>[] AuthorizedActions(string userId)
        {
            var actions = new IEntityAction<IRequisition>[]
            {
                new DraftAction(),
                new CreateAction()
            };

            var authorized = actions.Where(e => e.IsAuthorized(this, userId).IsAuthorized).ToArray();

            return authorized;
        }

        #region Actions

        public ExecutionResult Draft()
        {
            Status = RequisitionStatus.Draft;
            return ExecutionResult.Success();
        }

        public ExecutionResult Create()
        {
            Status = RequisitionStatus.Created;
            return ExecutionResult.Success();
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", DepartmentId, Sequence, Year);
        }
    }
}
