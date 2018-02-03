package com.example.user.eduhub.Fragments;

import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.PlaceHolder.JobExpHeaderVIew;
import com.example.user.eduhub.Adapters.PlaceHolder.JobExpItemView;
import com.example.user.eduhub.Adapters.PlaceHolder.ReviewItemsView;
import com.example.user.eduhub.Adapters.PlaceHolder.ReviewsHeaderView;
import com.example.user.eduhub.Dialog.CreateDialog;
import com.example.user.eduhub.Main2Activity;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.UserProfile.Review;
import com.example.user.eduhub.Models.UserProfile.UserProfile;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 30.01.2018.
 */

public class TeacherProfile extends Fragment {
    private UserProfile userProfile;
    CreateDialog createDialog;
    DialogInterface.OnClickListener myClickListener;
    SharedPreferences sharedPreferences;


    public void setUserProfile(UserProfile userProfile) {
        this.userProfile = userProfile;
    }
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.teacher_profile, null);
        TextView userName=v.findViewById(R.id.name_user_profile);
        TextView userEmail=v.findViewById(R.id.email_user_profile);
        TextView userName2=v.findViewById(R.id.name_user_profile2);
        TextView userEmail2=v.findViewById(R.id.email_user_profile2);
        TextView sex=v.findViewById(R.id.sex);
        TextView birthYear=v.findViewById(R.id.birth_year);
        TextView skils=v.findViewById(R.id.skils);
        TextView numberOfReview=v.findViewById(R.id.number_of_reviews);
        Button exit=v.findViewById(R.id.exit);
        sharedPreferences=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        ExpandablePlaceHolderView expandablePlaceHolderView=v.findViewById(R.id.expandableView);
        ExpandablePlaceHolderView expandablePlaceHolderView2=v.findViewById(R.id.expandableView2);

        userEmail.setText(userProfile.getEmail());
        userEmail2.setText(userProfile.getEmail());
        userName.setText(userProfile.getName());
        userName2.setText(userProfile.getName());
        skils.setText("");
        for (String skil:userProfile.getTeacherProfile().getSkills()
             ) {
            skils.setText(skils.getText().toString()+" "+skil);

        }
        expandablePlaceHolderView.addView(new ReviewsHeaderView(getContext(),userProfile.getTeacherProfile().getReviews().size()+" отзывов"));
        for(Review review:userProfile.getTeacherProfile().getReviews()){
            expandablePlaceHolderView.addView(new ReviewItemsView(getContext(),review));
        }
        expandablePlaceHolderView.addView(new JobExpHeaderVIew(getContext(),userProfile.getTeacherProfile().getJobExp().size()+" групп"));
        for(Group group:userProfile.getTeacherProfile().getJobExp()){
            expandablePlaceHolderView.addView(new JobExpItemView(getContext(),group));
        }
        myClickListener = new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                switch (which) {
                    // положительная кнопка
                    case Dialog.BUTTON_POSITIVE:
                        Intent intent=new Intent(getActivity(), Main2Activity.class);
                        SharedPreferences.Editor editor=sharedPreferences.edit();
                        editor.clear();
                        editor.commit();
                        getActivity().startActivity(intent);

                        break;
                    // негативная кнопка
                    case Dialog.BUTTON_NEGATIVE:

                        break;

                }
            }
        };
        exit.setOnClickListener(click->{
            createDialog=new CreateDialog(getContext(),myClickListener);
            createDialog.onCreateDialog(2).show();

        });
        return v;
    }

}

