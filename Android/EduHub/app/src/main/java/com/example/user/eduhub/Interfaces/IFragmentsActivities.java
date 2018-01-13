package com.example.user.eduhub.Interfaces;



import android.support.v4.app.Fragment;

import com.example.user.eduhub.Models.User;

/**
 * Created by user on 06.12.2017.
 */

public interface IFragmentsActivities {
   void switchingFragmets(Fragment fragment);
   void signIn(User user);


}
