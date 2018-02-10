package com.example.user.eduhub;

import android.content.Intent;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.FrameLayout;
import android.widget.ImageButton;

import com.example.user.eduhub.Fragments.RefactorTeacherProfile;
import com.example.user.eduhub.Fragments.RefactorUserProfile;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;

public class RefactorProfile extends AppCompatActivity {
UserProfileResponse userProfile;
FragmentTransaction fragmentTransaction;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_refactor_profile);
        ImageButton back=findViewById(R.id.back);

        Intent getIntent=getIntent();
        userProfile=(UserProfileResponse) getIntent.getSerializableExtra("UserProfile");


        fragmentTransaction=getSupportFragmentManager().beginTransaction();
        if(userProfile.getUserProfile().getIsTeacher()){
            RefactorTeacherProfile refactorTeacherProfile=new RefactorTeacherProfile();
            fragmentTransaction.add(R.id.conteiner_for_refactor_fragments,refactorTeacherProfile);
            fragmentTransaction.commit();
        }else {
            RefactorUserProfile refactorUserProfile=new RefactorUserProfile();
            fragmentTransaction.add(R.id.conteiner_for_refactor_fragments,refactorUserProfile);
            fragmentTransaction.commit();
        }







        back.setOnClickListener(click->{
            Intent intent=new Intent(this,AuthorizedUserActivity.class);
            startActivity(intent);
        });
    }
}
