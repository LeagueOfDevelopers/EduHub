package ru.lod_misis.user.eduhub.Interfaces;

import ru.lod_misis.user.eduhub.Models.User;

import java.util.ArrayList;

/**
 * Created by user on 06.12.2017.
 */

public interface IUserRepository {
    ArrayList<User> LoadUsers();
    ArrayList<User> AddUser(User user);
}
