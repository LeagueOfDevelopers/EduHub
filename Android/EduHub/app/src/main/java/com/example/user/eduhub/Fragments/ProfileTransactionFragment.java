package com.example.user.eduhub.Fragments;

import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.Contacts_adapter_profile;
import com.example.user.eduhub.Adapters.PlaceHolder.JobExpHeaderVIew;
import com.example.user.eduhub.Adapters.PlaceHolder.JobExpItemView;
import com.example.user.eduhub.Adapters.PlaceHolder.ReviewItemsView;
import com.example.user.eduhub.Adapters.PlaceHolder.ReviewsHeaderView;
import com.example.user.eduhub.Adapters.TagsAdapter;
import com.example.user.eduhub.Dialog.CreateDialog;
import com.example.user.eduhub.Fakes.FakeUserProfilePresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IUserProfileView;
import com.example.user.eduhub.Main2Activity;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.Review;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.Presenters.UserProfilePresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.RefactorProfile;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;

import java.util.ArrayList;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 30.01.2018.
 */

public class ProfileTransactionFragment extends Fragment implements IUserProfileView {
    FragmentTransaction fragmentTransaction;
    UserProfilePresenter userProfilePresenter=new UserProfilePresenter(this);
    FakesButton fakesButton=new FakesButton();
    SharedPreferences sharedPreferences;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    FakeUserProfilePresenter fakeUserProfilePresenter=new FakeUserProfilePresenter(this);
    View v;
    TextView userName;
    TextView userEmail;
    TextView userName2;
    TextView userEmail2;
    TextView sex;
    TextView birthYear;
    TextView aboutMe;
    ImageView refactor;
    RecyclerView contacts;
    Button exit;
    ExpandablePlaceHolderView expandablePlaceHolderView;
    ExpandablePlaceHolderView expandablePlaceHolderView2;
    CreateDialog createDialog;
    DialogInterface.OnClickListener myClickListener;

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
         v = inflater.inflate(R.layout.teacher_profile, null);
         userName=v.findViewById(R.id.name_user_profile);
         userEmail=v.findViewById(R.id.email_user_profile);
         userName2=v.findViewById(R.id.name_user_profile2);
         userEmail2=v.findViewById(R.id.email_user_profile2);
         sex=v.findViewById(R.id.sex);
         birthYear=v.findViewById(R.id.birth_year);
         aboutMe=v.findViewById(R.id.aboutMe);
         refactor=v.findViewById(R.id.refactor);
         contacts=v.findViewById(R.id.contacts);



         exit=v.findViewById(R.id.exit);
         expandablePlaceHolderView=v.findViewById(R.id.expandableView);
         expandablePlaceHolderView2=v.findViewById(R.id.expandableView2);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Мой профиль");
        sharedPreferences =getActivity().getSharedPreferences("User",MODE_PRIVATE);
        User user=savedDataRepository.loadSavedData(sharedPreferences);
        Log.d("TOKEN",user.getToken());
        Log.d("CHECK BUTTON",fakesButton.getCheckButton().toString());
        if(!fakesButton.getCheckButton()){

            userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());}
        else{
            fakeUserProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());
        }
    return v;}



    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void getError(Throwable error) {

    }

    @Override
    public void getUserProfile(UserProfileResponse userProfile) {
        userEmail.setText(userProfile.getUserProfile().getEmail());
        userEmail2.setText(userProfile.getUserProfile().getEmail());
        userName.setText(userProfile.getUserProfile().getName());
        userName2.setText(userProfile.getUserProfile().getName());

        if(!userProfile.getUserProfile().getGender().equals("0")){
            if (userProfile.getUserProfile().getGender().equals("1")){
                sex.setText("Мужской");
            }else{
                sex.setText("Женский");
            }
        }else {
            v.findViewById(R.id.card_of_sex).setVisibility(View.GONE);
        }
        if(userProfile.getUserProfile().getBirthYear().toString().equals("0")){
            v.findViewById(R.id.card_of_birth).setVisibility(View.GONE);
        }else {
            birthYear.setText(userProfile.getUserProfile().getBirthYear()+"");
        }
        if(userProfile.getUserProfile().getContacts()==null){
            v.findViewById(R.id.links).setVisibility(View.GONE);
        }else{
            contacts.setHasFixedSize(true);
            Contacts_adapter_profile adapter1=new Contacts_adapter_profile((ArrayList<String>) userProfile.getUserProfile().getContacts(),getActivity(),getContext());
            StaggeredGridLayoutManager llm = new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
            contacts.setLayoutManager(llm);
            contacts.setAdapter(adapter1);
        }

        if(userProfile.getUserProfile().getAboutUser()==null){
            v.findViewById(R.id.card_of_aboutMe).setVisibility(View.GONE);
        }else{
            aboutMe.setText(userProfile.getUserProfile().getAboutUser());}
            if(userProfile.getUserProfile().getIsTeacher()){
        if(userProfile.getTeacherProfile().getSkills().size()==0){
            v.findViewById(R.id.card_of_skils).setVisibility(View.GONE);
        }else{
            RecyclerView recyclerView=v.findViewById(R.id.skils);
            recyclerView.setHasFixedSize(true);
            StaggeredGridLayoutManager staggeredGridLayoutManager=new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
            recyclerView.setLayoutManager(staggeredGridLayoutManager);
            TagsAdapter adapter=new TagsAdapter((ArrayList<String>) userProfile.getTeacherProfile().getSkills());
            recyclerView.setAdapter(adapter);
        }}else{
                v.findViewById(R.id.card_of_skils).setVisibility(View.GONE);
            }
        if(userProfile.getUserProfile().getIsTeacher()){
        expandablePlaceHolderView.addView(new ReviewsHeaderView(getContext(),userProfile.getTeacherProfile().getReviews().size()+" отзывов"));
        for(Review review:userProfile.getTeacherProfile().getReviews()){
            expandablePlaceHolderView.addView(new ReviewItemsView(getContext(),review));
        }
        if(userProfile.getTeacherProfile().getJobExp()!=null){
            expandablePlaceHolderView.addView(new JobExpHeaderVIew(getContext(),userProfile.getTeacherProfile().getJobExp().size()+" групп"));
            for(Group group:userProfile.getTeacherProfile().getJobExp()){
                expandablePlaceHolderView.addView(new JobExpItemView(getContext(),group));
            }
        }}else{
            expandablePlaceHolderView.setVisibility(View.GONE);
            expandablePlaceHolderView2.setVisibility(View.GONE);
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
        refactor.setOnClickListener(click->{
            Intent intent=new Intent(getActivity(),RefactorProfile.class);
            intent.putExtra("UserProfile",userProfile);
            getActivity().startActivity(intent);
        });
    }
}
