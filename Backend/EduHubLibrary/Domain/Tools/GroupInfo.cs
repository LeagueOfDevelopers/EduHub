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

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Ensure.String.IsNotNullOrWhiteSpace(value);
                _title = value;
            }
        }

        public string Description { get; set; }

        public IEnumerable<string> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
            }
        }

        public GroupType GroupType
        {
            get
            {
                return _groupType;
            }
            set
            {
                Ensure.Any.IsNotNull(value);
                _groupType = value;
            }
        }

        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                Ensure.Any.IsNotNull(value);

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
            set
            {
                Ensure.Any.IsNotNull(value);

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
        
        private string _title;
        private string _description;
        private IEnumerable<string> _tags;
        private GroupType _groupType;
        private int _size;
        private double _moneyPerUser;
    }
}
