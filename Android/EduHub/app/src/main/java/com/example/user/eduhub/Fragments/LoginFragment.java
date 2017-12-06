package com.example.user.eduhub.Fragments;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.R;

/**
 * Created by user on 05.12.2017.
 */

public class LoginFragment extends Fragment {
IFragmentsActivities fragmentsActivities;
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            fragmentsActivities = (IFragmentsActivities) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement onSomeEventListener");
        }
    }
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.login_fragment, null);
        EditText emailText=(EditText)v.findViewById(R.id.edit_email);
        EditText password=(EditText)v.findViewById(R.id.edit_password);
        Button signIn= v.findViewById(R.id.sign_in);
        Button signUp= v.findViewById(R.id.sign_up);
        signUp.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                RegistrationFragment registrationFragment=new RegistrationFragment();
                fragmentsActivities.switchingFragmets(registrationFragment);

            }
        });
        return v;

    }

}
