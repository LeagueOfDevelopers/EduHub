package com.example.user.eduhub.Interfaces;

import android.app.Fragment;
import android.content.Intent;

import com.example.user.eduhub.Classes.User;

/**
 * Created by user on 06.12.2017.
 */

public interface IFragmentsActivities {
   void switchingFragmets(Fragment fragment);
   void signIn(User user);


}
