using EduHubLibrary.Domain.Exceptions;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class GroupInfo
    {
        public GroupInfo(Guid id, string title, string description, IEnumerable<string> tags, GroupType groupType,
            bool isPrivate, bool isActive, int size, double moneyPerUser)
        {
            Id = id;
            Title = title;
            Description = description;
            Tags = tags;
            GroupType = groupType;
            IsPrivate = isPrivate;
            IsActive = isActive;
            Size = size;
            MoneyPerUser = moneyPerUser;
        }

        public Guid Id { get; private set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public IEnumerable<string> Tags { get; internal set; }
        public GroupType GroupType { get; private set; }
        public bool IsPrivate { get; private set; }
        public bool IsActive { get; set; }

        public int Size
        {
            get
            {
                return _size;
            }
            internal set
            {
                if (value>0)
                {
                    _size = value;
                }
                else
                {
                    throw new InvalidGroupInfo("size");
                }
            }
        }

        public double MoneyPerUser
        {
            get
            {
                return _moneyPerUser;
            }
            internal set
            {
                if (value >= 0)
                {
                    _moneyPerUser = value;
                }
                else
                {
                    throw new InvalidGroupInfo("money per user");
                }
            }
        }
        
        private int _size;
        private double _moneyPerUser;
    }
}
