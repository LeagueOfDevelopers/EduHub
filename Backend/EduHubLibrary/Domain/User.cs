using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Guid Id { get; private set; }

        public User(string name, string email, bool isTeacher)
        {
            Name = name;
            Email = email;
            _isTeacher = isTeacher;
            _isActive = true;
            Id = Guid.NewGuid();
        }

        public void EditProfile(string newName, bool isTeacher)
        {
            Name = newName;
            _isTeacher = isTeacher;
        }

        public void RestoreProfile()
        {
            _isActive = true;
        }

        public void DeleteProfile()
        {
            _isActive = false;
        }

        public void InviteToGroup(Group group, RoleInChat role)
        {
            if (_isActive)
            {
                if (_memberShips.Any(s => s.Group.Equals(group)))
                {
                    throw new Exception("Пользователь уже приглашен или состоит в этой группе");
                }
                else
                {
                    _memberShips.Add(new MemberShip(group, role, StatusInChat.Invited));
                }
            }
            else throw new Exception("Пользователь неактивен");
        }

        public void AcceptInvitation(Group group)
        {
            if (_memberShips.Any(s => s.Group.Equals(group)))
            {
                throw new Exception("Пользователь не приглашен в эту группу");
            }
            else
            {
                MemberShip Membership = _memberShips.First(s => s.Group.Equals(group));
                Membership.Status = StatusInChat.Waiting;
            }
        }

        public void RejectInvitation(Group group)
        {
            if (_memberShips.Any(s => s.Group.Equals(group)))
            {
                throw new Exception("Пользователь не приглашен в эту группу");
            }
            else
            {
                MemberShip Membership = _memberShips.First(s => s.Group.Equals(group));
                _memberShips.Remove(Membership);
            }
        }

        public void DeleteFromGroup(Group group)
        {
            if (_memberShips.Any(s => s.Group.Equals(group)))
            {
                throw new Exception("Пользователь не состоит в этой группе");
            }
            else
            {
                MemberShip Membership = _memberShips.First(s => s.Group.Equals(group));

                if (Membership.Role == RoleInChat.Teacher)
                {
                    throw new Exception("Преподаватель не может покинуть группу, не закрыв курс");
                }
                else
                {
                    _memberShips.Remove(Membership);
                }
            }
        }

        bool _isTeacher;
        bool _isActive;
        List<MemberShip> _memberShips;
    }

    class MemberShip
    {
        public Group Group
        {
            get { return _group; }
            private set { _group = value; }
        }
        Group _group;

        public RoleInChat Role { get; private set; }
        public StatusInChat Status { get; set; }

        public MemberShip(Group group, RoleInChat role, StatusInChat status)
        {
            _group = group;
            Role = role;
            Status = status;
            Restrictions = new List<Punishment>();
        }

        public void AddPunishment()
        {
            Restrictions.Add(new Punishment());
        }

        List<Punishment> Restrictions;
    }

    struct Punishment
    {
        //Параметры санкций
    }

    public enum RoleInChat { Creator, Teacher, Student }
    public enum StatusInChat { Invited, Waiting, Educating }
}
