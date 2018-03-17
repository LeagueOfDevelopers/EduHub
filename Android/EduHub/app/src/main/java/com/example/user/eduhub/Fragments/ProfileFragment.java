package com.example.user.eduhub.Fragments;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.media.MediaFormat;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.support.v7.widget.Toolbar;
import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.user.eduhub.Adapters.Contacts_adapter_profile;
import com.example.user.eduhub.Adapters.PlaceHolder.JobExpHeaderVIew;
import com.example.user.eduhub.Adapters.PlaceHolder.JobExpItemView;
import com.example.user.eduhub.Adapters.PlaceHolder.ReviewItemsView;
import com.example.user.eduhub.Adapters.PlaceHolder.ReviewsHeaderView;
import com.example.user.eduhub.Adapters.TagsAdapter;
import com.example.user.eduhub.Dialog.CreateDialog;
import com.example.user.eduhub.Fakes.FakeUserProfilePresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IFileRepositoryView;
import com.example.user.eduhub.Interfaces.View.IRefreshTokenView;
import com.example.user.eduhub.Interfaces.View.IUserProfileView;
import com.example.user.eduhub.Main2Activity;
import com.example.user.eduhub.Models.AddFileResponseModel;
import com.example.user.eduhub.Models.DecodeFile;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.Review;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.Presenters.FileRepository;
import com.example.user.eduhub.Presenters.RefreshTokenPresenter;
import com.example.user.eduhub.Presenters.UserProfilePresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.RefactorProfile;
import com.jakewharton.retrofit2.adapter.rxjava2.HttpException;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;
import com.squareup.picasso.Picasso;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.net.SocketTimeoutException;
import java.util.ArrayList;

import okhttp3.MediaType;
import okhttp3.ResponseBody;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 30.01.2018.
 */

public class ProfileFragment extends Fragment implements IUserProfileView,IRefreshTokenView,IFileRepositoryView {
    FragmentTransaction fragmentTransaction;
    UserProfilePresenter userProfilePresenter=new UserProfilePresenter(this);
    FakesButton fakesButton=new FakesButton();
    SharedPreferences sharedPreferences;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    FakeUserProfilePresenter fakeUserProfilePresenter=new FakeUserProfilePresenter(this);
    FileRepository fileRepository=new FileRepository(this,getActivity());
    View v;
    Boolean flag=false;
    TextView userName;
    TextView userEmail;
    TextView userName2;
    TextView userEmail2;
    TextView sex;
    TextView birthYear;
    TextView aboutMe;
    ImageView refactor;
    ImageView avatar;
    TextView status;
    RecyclerView contacts;
    Button exit;
    ExpandablePlaceHolderView expandablePlaceHolderView;
    ExpandablePlaceHolderView expandablePlaceHolderView2;
    CreateDialog createDialog;
    DialogInterface.OnClickListener myClickListener;
    RefreshTokenPresenter refreshTokenPresenter=new RefreshTokenPresenter(this);
    User user;
    DecodeFile decodeFile=new DecodeFile(getActivity());

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
         v = inflater.inflate(R.layout.teacher_profile, null);
         avatar=v.findViewById(R.id.avatar);
         userName=v.findViewById(R.id.name_user_profile);
         userEmail=v.findViewById(R.id.email_user_profile);
         userName2=v.findViewById(R.id.name_user_profile2);
         userEmail2=v.findViewById(R.id.email_user_profile2);
         status=v.findViewById(R.id.status);
         sex=v.findViewById(R.id.sex);
         birthYear=v.findViewById(R.id.birth_year);
         aboutMe=v.findViewById(R.id.aboutMe);
         refactor=v.findViewById(R.id.refactor);
         contacts=v.findViewById(R.id.contacts);



         exit=v.findViewById(R.id.exit);

        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Мой профиль");

        Log.d("TOKEN",user.getToken());
        Log.d("CHECK BUTTON",fakesButton.getCheckButton().toString());
        expandablePlaceHolderView=v.findViewById(R.id.expandableView);
        expandablePlaceHolderView2=v.findViewById(R.id.expandableView2);
        if(!fakesButton.getCheckButton()){
            Log.d("TOKEN",user.getToken());
            userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());}
        else{
            fakeUserProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());
        }

    return v;}

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        sharedPreferences =getActivity().getSharedPreferences("User",MODE_PRIVATE);
        user=savedDataRepository.loadSavedData(sharedPreferences);
        if(!fakesButton.getCheckButton()){
            Log.d("TOKEN",user.getToken());
            userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());}
        else{
            fakeUserProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());
        }
        flag=true;

    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
    }

    @Override
    public void onResume() {
        super.onResume();
        if(!flag){
            onRefresh();
        }

    }

    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void getError(Throwable error) {

        if(error instanceof HttpException){
            switch (((HttpException) error).code()){
                case 401:{refreshTokenPresenter.refreshToken(user.getToken());}

            }
        }
        if(error instanceof SocketTimeoutException){
            MakeToast("Возможно у Вас пропалосоединение с интернетом");
        }
    }

    @Override
    public void onPause() {
        super.onPause();
        flag=false;
        expandablePlaceHolderView.removeAllViews();
        expandablePlaceHolderView2.removeAllViews();
    }

    @Override
    public void getUserProfile(UserProfileResponse userProfile) {
        Log.d("Role",userProfile.getUserProfile().getIsTeacher().toString());
        userEmail.setText(userProfile.getUserProfile().getEmail());
        userEmail2.setText(userProfile.getUserProfile().getEmail());
        userName.setText(userProfile.getUserProfile().getName());
        userName2.setText(userProfile.getUserProfile().getName());
        if(userProfile.getUserProfile().getIsTeacher()){
            status.setText("Преподаватель");
        }else{
            status.setText("Ученик");
        }
        if(userProfile.getUserProfile().getAvatarLink()!=null){
            fileRepository.loadFileFromServer(user.getToken(),userProfile.getUserProfile().getAvatarLink());
        }
        if(!userProfile.getUserProfile().getGender().equals("0")){
            v.findViewById(R.id.card_of_sex).setVisibility(View.VISIBLE);
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
            v.findViewById(R.id.card_of_birth).setVisibility(View.VISIBLE);
            birthYear.setText(userProfile.getUserProfile().getBirthYear()+"");
        }
        if(userProfile.getUserProfile().getContacts().size()==0){
            v.findViewById(R.id.links).setVisibility(View.GONE);
        }else{
            v.findViewById(R.id.links).setVisibility(View.VISIBLE);
            contacts.setHasFixedSize(true);
            Contacts_adapter_profile adapter1=new Contacts_adapter_profile((ArrayList<String>) userProfile.getUserProfile().getContacts(),getActivity(),getContext());
            StaggeredGridLayoutManager llm = new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
            contacts.setLayoutManager(llm);
            contacts.setAdapter(adapter1);
        }

        if(userProfile.getUserProfile().getAboutUser()==null){
            v.findViewById(R.id.card_of_aboutMe).setVisibility(View.GONE);
        }else{
            v.findViewById(R.id.card_of_aboutMe).setVisibility(View.VISIBLE);
            aboutMe.setText(userProfile.getUserProfile().getAboutUser());}
            if(userProfile.getUserProfile().getIsTeacher()){
        if(userProfile.getTeacherProfile().getSkills().size()==0){
            v.findViewById(R.id.card_of_skils).setVisibility(View.VISIBLE);
            v.findViewById(R.id.card_of_skils).setVisibility(View.GONE);
        }else{
            v.findViewById(R.id.card_of_skils).setVisibility(View.VISIBLE);
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
            expandablePlaceHolderView.setVisibility(View.VISIBLE);
            expandablePlaceHolderView2.setVisibility(View.VISIBLE);
            Log.d("ROLE2",userProfile.getUserProfile().getIsTeacher().toString());
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
    private void MakeToast(String s) {
        Toast toast = Toast.makeText(getActivity().getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }

    @Override
    public void getResponse(User user) {
        savedDataRepository.SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail(),sharedPreferences);
        userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());
    }

    @Override
    public void getThrowable() {
        Intent intent=new Intent(getActivity(), Main2Activity.class);
        SharedPreferences.Editor editor=sharedPreferences.edit();
        editor.clear();
        editor.commit();
        getActivity().startActivity(intent);
    }

    @Override
    public void getResponse(AddFileResponseModel addFileResponseModel) {

    }

    @SuppressLint("NewApi")
    @Override
    public void getFile(ResponseBody file) {




        byte[] rawBitmap;
        try {rawBitmap=file.bytes();
             Log.d("Проверяемчтозахрень тут",rawBitmap[5]+"");
             Bitmap bitmap = BitmapFactory.decodeByteArray(rawBitmap,0,rawBitmap.length);


            avatar.setImageBitmap(bitmap);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    void onRefresh(){

            sharedPreferences =getActivity().getSharedPreferences("User",MODE_PRIVATE);
            user=savedDataRepository.loadSavedData(sharedPreferences);
            if(!fakesButton.getCheckButton()){
                Log.d("TOKEN",user.getToken());
                userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());}
            else{
                fakeUserProfilePresenter.loadUserProfile(user.getToken(),user.getUserId());
            }

    }
}
