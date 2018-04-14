package ru.lod_misis.user.eduhub.Fakes;

import android.content.Context;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ILoginPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ILoginView;
import ru.lod_misis.user.eduhub.Models.User;

/**
 * Created by User on 18.01.2018.
 */

public class FakeLoginPresenter implements ILoginPresenter{
    private ILoginView loginView;

    public FakeLoginPresenter(ILoginView loginView) {
        this.loginView = loginView;
    }

    @Override
    public void Login(String email, String password, Context context) {
        User user=new User();
        user.setEmail(email);
        user.setName("Александр");
        user.setUserId("93d08fd5-c101-42d4-8811-8e48f2434304");
        user.setToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSb2xlIjoiVXNlciIsIlVzZXJJZCI6IjM2ODAwYTM5LTNkYmUtNDc1MS04OTM1LWQwMzc5YmViZjI5NCIsImV4cCI6MTUxNzA0NjMxMCwiaXNzIjoibG9kLW1pc2lzLnJ1In0.t20JkeCU-cVnQYugkhIa1S99-bfOdTC_2GQbA8xnH0E");
        user.setTeacher(true);
        user.setAvatarLink("");
        loginView.login(user);
    }
}
