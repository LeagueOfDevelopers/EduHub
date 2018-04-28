package ru.lod_misis.user.eduhub;

import android.app.Activity;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Adapters.Contacts_adapter_profile;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.JobExpHeaderVIew;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.JobExpItemView;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.ReviewItemsView;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.ReviewsHeaderView;
import ru.lod_misis.user.eduhub.Adapters.TagsAdapter;
import ru.lod_misis.user.eduhub.Dialog.CreateDialog;
import ru.lod_misis.user.eduhub.Fakes.FakeUserProfilePresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.IFileRepositoryView;
import ru.lod_misis.user.eduhub.Interfaces.View.IRefreshTokenView;
import ru.lod_misis.user.eduhub.Interfaces.View.IUserProfileView;
import ru.lod_misis.user.eduhub.Models.AddFileResponseModel;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Models.UserProfile.Review;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserProfileResponse;
import ru.lod_misis.user.eduhub.Presenters.FileRepository;
import ru.lod_misis.user.eduhub.Presenters.RefreshTokenPresenter;
import ru.lod_misis.user.eduhub.Presenters.UserProfilePresenter;


import com.example.user.eduhub.R;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;

import java.io.IOException;
import java.util.ArrayList;

import okhttp3.ResponseBody;

public class AnotherProfileActivity extends AppCompatActivity implements IUserProfileView,IRefreshTokenView,IFileRepositoryView {
    UserProfilePresenter userProfilePresenter=new UserProfilePresenter(this);
    FakesButton fakesButton=new FakesButton();
    SharedPreferences sharedPreferences;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    FakeUserProfilePresenter fakeUserProfilePresenter=new FakeUserProfilePresenter(this);
    FileRepository fileRepository;

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
    RelativeLayout relativeLayout;
    FrameLayout load;
    Activity v=this;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        fileRepository=new FileRepository(this,this);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_another_profile);
        Intent intent=getIntent();
        String id=intent.getStringExtra("id");
        sharedPreferences =getSharedPreferences("User",MODE_PRIVATE);
        user=savedDataRepository.loadSavedData(sharedPreferences);
        ImageButton backButton=findViewById(R.id.back);
        relativeLayout=findViewById(R.id.window);

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

        Toolbar toolbar=findViewById(R.id.toolbar);


        Log.d("TOKEN",user.getToken());
        Log.d("CHECK BUTTON",fakesButton.getCheckButton().toString());
        expandablePlaceHolderView=v.findViewById(R.id.expandableView);
        expandablePlaceHolderView2=v.findViewById(R.id.expandableView2);

        if(!fakesButton.getCheckButton()){
            Log.d("TOKEN",user.getToken());
            userProfilePresenter.loadUserProfile(user.getToken(),id,this);}
        else{
            fakeUserProfilePresenter.loadUserProfile(user.getToken(),id,this);
        }
        backButton.setOnClickListener(click->{
            onBackPressed();
        });
    }

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
    public void getResponse(User user) {

    }

    @Override
    public void getThrowable() {

    }

    @Override
    public void getResponse(AddFileResponseModel addFileResponseModel) {

    }

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

    @Override
    public void getUserProfile(UserProfileResponse userProfile) {
        {
            Log.d("window",relativeLayout.toString());

            relativeLayout.setVisibility(View.VISIBLE);
            Log.d("IsVisible",(relativeLayout.getVisibility())+"");
            Log.d("Role12",userProfile.getUserProfile().getIsTeacher().toString());
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
                fileRepository.loadImageFromServer(user.getToken(),userProfile.getUserProfile().getAvatarLink());
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
                Contacts_adapter_profile adapter1=new Contacts_adapter_profile((ArrayList<String>) userProfile.getUserProfile().getContacts(),this,this);
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
                expandablePlaceHolderView.addView(new ReviewsHeaderView(this,userProfile.getTeacherProfile().getReviews().size()+" отзывов"));
                for(Review review:userProfile.getTeacherProfile().getReviews()){
                    expandablePlaceHolderView.addView(new ReviewItemsView(this,review));
                }
                if(userProfile.getTeacherProfile().getJobExp()!=null){
                    expandablePlaceHolderView.addView(new JobExpHeaderVIew(this,userProfile.getTeacherProfile().getJobExp().size()+" групп"));
                    for(Group group:userProfile.getTeacherProfile().getJobExp()){
                        expandablePlaceHolderView.addView(new JobExpItemView(this,group));
                    }
                }}else{
                expandablePlaceHolderView.setVisibility(View.GONE);
                expandablePlaceHolderView2.setVisibility(View.GONE);
            }



        }
    }
}
