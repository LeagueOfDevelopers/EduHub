package ru.lod_misis.user.eduhub.Fragments;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
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
import android.widget.CompoundButton;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import ru.lod_misis.user.eduhub.Adapters.Contacts_adapter_profile;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.JobExpHeaderVIew;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.JobExpItemView;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.ReviewItemsView;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.ReviewsHeaderView;
import ru.lod_misis.user.eduhub.Adapters.TagsAdapter;
import ru.lod_misis.user.eduhub.Dialog.CreateDialog;
import ru.lod_misis.user.eduhub.Fakes.FakeUserProfilePresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.IChangeUsersDataView;
import ru.lod_misis.user.eduhub.Interfaces.View.IFileRepositoryView;
import ru.lod_misis.user.eduhub.Interfaces.View.IRefreshTokenView;
import ru.lod_misis.user.eduhub.Interfaces.View.IUserProfileView;
import ru.lod_misis.user.eduhub.Main2Activity;
import ru.lod_misis.user.eduhub.Models.AddFileResponseModel;
import ru.lod_misis.user.eduhub.Models.ConverDate;
import ru.lod_misis.user.eduhub.Models.DecodeFile;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Models.UserProfile.Review;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserProfileResponse;
import ru.lod_misis.user.eduhub.Presenters.ChangeUserDataPresenter;
import ru.lod_misis.user.eduhub.Presenters.FileRepository;
import ru.lod_misis.user.eduhub.Presenters.RefreshTokenPresenter;
import ru.lod_misis.user.eduhub.Presenters.UserProfilePresenter;
import com.example.user.eduhub.R;
import ru.lod_misis.user.eduhub.RefactorProfile;
import com.jakewharton.retrofit2.adapter.rxjava2.HttpException;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;
import com.squareup.picasso.Picasso;

import java.io.IOException;
import java.net.SocketTimeoutException;
import java.util.ArrayList;

import okhttp3.ResponseBody;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
    FileRepository fileRepository;
    View v;
    View line1;
    View line2;
    View line3;
    View line4;
    View line5;
    View line6;

    Boolean flag=false;
    TextView userName;
    TextView userEmail;
    TextView userName2;
    TextView userEmail2;
    TextView sex;
    TextView birthYear;
    TextView aboutMe;
    TextView headerSex;
    TextView headerName;
    TextView headerEmail;
    TextView headerSkils;
    TextView headerBirthyear;
    TextView headerContacts;
    TextView headerAboutMe;

    FloatingActionButton refactor;
    ImageView avatar;
    Switch status;
    RecyclerView contacts;
    FrameLayout mainLayout;
    ExpandablePlaceHolderView expandablePlaceHolderView;
    ExpandablePlaceHolderView expandablePlaceHolderView2;
    CreateDialog createDialog;
    DialogInterface.OnClickListener myClickListener;
    RefreshTokenPresenter refreshTokenPresenter=new RefreshTokenPresenter(this);
    User user;
    RelativeLayout relativeLayout;
    DecodeFile decodeFile=new DecodeFile(getActivity());
    ConverDate converDate=new ConverDate();

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

         v = inflater.inflate(R.layout.teacher_profile, null);


        fileRepository=new FileRepository(this,getContext());
         avatar=v.findViewById(R.id.avatar);
         userName=v.findViewById(R.id.name_user_profile);
         mainLayout=v.findViewById(R.id.main_profile_layout);
         userName2=v.findViewById(R.id.name_user_profile2);
         userEmail2=v.findViewById(R.id.email_user_profile2);
         status=v.findViewById(R.id.teacher_or_not);
         sex=v.findViewById(R.id.sex);
         birthYear=v.findViewById(R.id.birth_year);
         aboutMe=v.findViewById(R.id.aboutMe);
         refactor=v.findViewById(R.id.refactor);
         contacts=v.findViewById(R.id.contacts);
        line1=v.findViewById(R.id.line1);
        line2=v.findViewById(R.id.line2);
        line3=v.findViewById(R.id.line3);
        line4=v.findViewById(R.id.line4);
        line5=v.findViewById(R.id.line5);
        line6=v.findViewById(R.id.line6);

        headerAboutMe=v.findViewById(R.id.aboutMe_header);
        headerBirthyear=v.findViewById(R.id.birth_year_header);
        headerContacts=v.findViewById(R.id.contacts_header);
        headerEmail=v.findViewById(R.id.email_user_profile2_header);
        headerName=v.findViewById(R.id.name_user_profile_header);
        headerSex=v.findViewById(R.id.sex_header);
        headerSkils=v.findViewById(R.id.skils_header);





        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Мой профиль");

        Log.d("TOKEN",user.getToken());
        Log.d("CHECK BUTTON",fakesButton.getCheckButton().toString());
        expandablePlaceHolderView=v.findViewById(R.id.expandableView);
        expandablePlaceHolderView2=v.findViewById(R.id.expandableView2);
        if(!fakesButton.getCheckButton()){}
        else{
            fakeUserProfilePresenter.loadUserProfile(user.getToken(),user.getUserId(),getContext());
        }
        status.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                EduHubApi eduHubApi= RetrofitBuilder.getApi(getContext());
                if(isChecked){

                    if(!fakesButton.getCheckButton()){

                        eduHubApi.becomeTeacher("Bearer "+user.getToken())
                                .subscribeOn(Schedulers.io())
                                .observeOn(AndroidSchedulers.mainThread())
                                .subscribe();
                    }

                }else{
                    if(!fakesButton.getCheckButton()){
                    eduHubApi.becomeSimpleUser("Bearer "+user.getToken())
                            .subscribeOn(Schedulers.io())
                            .observeOn(AndroidSchedulers.mainThread())
                            .subscribe();}
                }
            }
        });
    return v;}

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        sharedPreferences =getActivity().getSharedPreferences("User",MODE_PRIVATE);
        user=savedDataRepository.loadSavedData(sharedPreferences);
        if(!fakesButton.getCheckButton()){
            Log.d("TOKEN",user.getToken());
            userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId(),getContext());}

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
                case 401:{refreshTokenPresenter.refreshToken(user.getToken(),getContext());}

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
        expandablePlaceHolderView.setVisibility(View.GONE);
        expandablePlaceHolderView2.setVisibility(View.GONE);

        Log.d("Role",userProfile.getUserProfile().getIsTeacher().toString());

        userEmail2.setText(userProfile.getUserProfile().getEmail());
        userName.setText(userProfile.getUserProfile().getName());
        userName2.setText(userProfile.getUserProfile().getName());
        status.setChecked(userProfile.getUserProfile().getIsTeacher());

        if(userProfile.getUserProfile().getAvatarLink()!=null){
            Picasso.get().load("http://85.143.104.47:2411/api/file/img/"+userProfile.getUserProfile().getAvatarLink()).resize(150,150).into(avatar);
        }
        if(!userProfile.getUserProfile().getGender().equals("0")){
            sex.setVisibility(View.VISIBLE);
            line3.setVisibility(View.VISIBLE);
            headerSex.setVisibility(View.VISIBLE);
            if (userProfile.getUserProfile().getGender().equals("1")){
                sex.setText("Мужской");
            }else{
                sex.setText("Женский");
            }
        }else {
            sex.setVisibility(View.GONE);
            line3.setVisibility(View.GONE);
            headerSex.setVisibility(View.GONE);
        }
        if(userProfile.getUserProfile().getBirthYear().toString().equals("0001-01-01T00:00:00+00:00")){
            line4.setVisibility(View.GONE);
            headerBirthyear.setVisibility(View.GONE);
            birthYear.setVisibility(View.GONE);

        }else {
            line4.setVisibility(View.VISIBLE);
            headerBirthyear.setVisibility(View.VISIBLE);
            birthYear.setVisibility(View.VISIBLE);
            birthYear.setText(converDate.convertDate(userProfile.getUserProfile().getBirthYear(),false));
        }
        if(userProfile.getUserProfile().getContacts().size()==0){
            headerContacts.setVisibility(View.GONE);
            contacts.setVisibility(View.GONE);

        }else{


            contacts.setHasFixedSize(true);
            Contacts_adapter_profile adapter1=new Contacts_adapter_profile((ArrayList<String>) userProfile.getUserProfile().getContacts(),getActivity(),getContext());
            StaggeredGridLayoutManager llm = new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
            contacts.setLayoutManager(llm);
            contacts.setAdapter(adapter1);
            headerContacts.setVisibility(View.VISIBLE);
            contacts.setVisibility(View.VISIBLE);
        }

        if(userProfile.getUserProfile().getAboutUser()==null){
            headerAboutMe.setVisibility(View.GONE);
            aboutMe.setVisibility(View.GONE);
            line5.setVisibility(View.GONE);
        }else{
            headerAboutMe.setVisibility(View.VISIBLE);
            aboutMe.setVisibility(View.VISIBLE);
            line5.setVisibility(View.VISIBLE);
            aboutMe.setText(userProfile.getUserProfile().getAboutUser());}
            if(userProfile.getUserProfile().getIsTeacher()){
                headerSkils.setVisibility(View.VISIBLE);
                v.findViewById(R.id.skils).setVisibility(View.VISIBLE);
                line6.setVisibility(View.VISIBLE);
        if(userProfile.getTeacherProfile().getSkills().size()==0){
            headerSkils.setVisibility(View.GONE);
            v.findViewById(R.id.skils).setVisibility(View.GONE);
            line6.setVisibility(View.GONE);
        }else{
            headerSkils.setVisibility(View.VISIBLE);
            v.findViewById(R.id.skils).setVisibility(View.VISIBLE);
            line6.setVisibility(View.VISIBLE);
            RecyclerView recyclerView=v.findViewById(R.id.skils);
            recyclerView.setHasFixedSize(true);
            StaggeredGridLayoutManager staggeredGridLayoutManager=new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
            recyclerView.setLayoutManager(staggeredGridLayoutManager);
            TagsAdapter adapter=new TagsAdapter((ArrayList<String>) userProfile.getTeacherProfile().getSkills());
            recyclerView.setAdapter(adapter);
        }}else{
                headerSkils.setVisibility(View.GONE);
                v.findViewById(R.id.skils).setVisibility(View.GONE);
                line6.setVisibility(View.GONE);
            }

        if(userProfile.getUserProfile().getIsTeacher()){
            expandablePlaceHolderView.setVisibility(View.VISIBLE);

            Log.d("ROLE2",userProfile.getUserProfile().getIsTeacher().toString());
        expandablePlaceHolderView.addView(new ReviewsHeaderView(getContext(),userProfile.getTeacherProfile().getReviews().size()+" отзывов"));
        for(Review review:userProfile.getTeacherProfile().getReviews()){
            expandablePlaceHolderView.addView(new ReviewItemsView(getContext(),review));
        }
        if(userProfile.getTeacherProfile().getJobExp()!=null){
            expandablePlaceHolderView2.setVisibility(View.VISIBLE);
            expandablePlaceHolderView2.addView(new JobExpHeaderVIew(getContext(),userProfile.getTeacherProfile().getJobExp().size()+" групп"));
            for(Group group:userProfile.getTeacherProfile().getJobExp()){
                expandablePlaceHolderView2.addView(new JobExpItemView(getContext(),group));
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

        refactor.setOnClickListener(click->{
            Intent intent=new Intent(getActivity(),RefactorProfile.class);
            intent.putExtra("UserProfile",userProfile);
            getActivity().startActivity(intent);
        });
        mainLayout.setVisibility(View.VISIBLE);
    }
    private void MakeToast(String s) {
        Toast toast = Toast.makeText(getActivity().getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }

    @Override
    public void getResponse(User user) {
        savedDataRepository.SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail(),user.getTeacher(),sharedPreferences);
        userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId(),getContext());
    }

    @Override
    public void getThrowable(Throwable error) {
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
                userProfilePresenter.loadUserProfile(user.getToken(),user.getUserId(),getContext());}
            else{
                fakeUserProfilePresenter.loadUserProfile(user.getToken(),user.getUserId(),getContext());
            }

    }

}
