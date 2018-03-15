package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.content.CursorLoader;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.widget.ImageButton;

import com.example.user.eduhub.Fakes.FakeGroupInformationPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Fragments.MainGroupFragment;
import com.example.user.eduhub.Fragments.UnsignedMainGroupFragment;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Interfaces.View.ICourseMethodsView;
import com.example.user.eduhub.Interfaces.View.IFileRepositoryView;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.AddFileResponseModel;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.CourseMethodsPresenter;
import com.example.user.eduhub.Presenters.FileRepository;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;

import java.io.File;

import okhttp3.ResponseBody;

public class GroupActivity extends AppCompatActivity
        implements  IFragmentsActivities,IGroupView,IFileRepositoryView,ICourseMethodsView {
    Group group;
    FragmentTransaction transaction;
    MainGroupFragment mainGroupFragment;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;
    GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
    FakesButton fakesButton=new FakesButton();
    FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";
    SharedPreferences sPref;
    CourseMethodsPresenter addCourseMethodsPresenter=new CourseMethodsPresenter(this);
    FileRepository fileRepository=new FileRepository(this,this);
    ImageButton back;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_group);
        Intent intent=getIntent();
        group=(Group) intent.getSerializableExtra("group") ;
         back=findViewById(R.id.back);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("");
        setSupportActionBar(toolbar);
        sPref=getSharedPreferences("User",MODE_PRIVATE);
        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){
            user= savedDataRepository.loadSavedData(sPref);
            if(!fakesButton.getCheckButton()){
                Log.d("GroupId",group.getGroupInfo().getId());
                groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
            }else{
                fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
            }
        }else{
            UnsignedMainGroupFragment unsignedMainGroupFragment=new UnsignedMainGroupFragment();
            unsignedMainGroupFragment.setGroup(group);
            transaction=getSupportFragmentManager().beginTransaction();
            transaction.add(R.id.group_fragments_conteiner,unsignedMainGroupFragment);
            transaction.commit();
        }


        back.setOnClickListener(click->{
            onBackPressed();
        });

    }

    @Override
    protected void onResume() {
        super.onResume();

    }

    @Override
    public void switchingFragmets(Fragment fragment) {
        transaction=getSupportFragmentManager().beginTransaction();
        if(fragment instanceof MainGroupFragment){

            ((MainGroupFragment) fragment).setGroup(group);
        }
        transaction.replace(R.id.group_fragments_conteiner,fragment);
        transaction.commit();
    }

    @Override
    public void signIn(User user) {

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
    public void getInformationAboutGroup(Group group) {
        Log.d("MyId",user.getUserId());
        group.getGroupInfo().setId(this.group.getGroupInfo().getId());
        Boolean flag=false;
        for (Member member:group.getMembers()) {
            if(user.getUserId().equals(member.getUserId())){
                Log.d("memberId",member.getUserId());
                flag=true;
            }

        }

        if(flag){
        mainGroupFragment=new MainGroupFragment();
        mainGroupFragment.setGroup(group);
        transaction=getSupportFragmentManager().beginTransaction();
        Log.d("TRANSACTION",transaction.isEmpty()+"");
        if(transaction.isEmpty()){
        transaction.replace(R.id.group_fragments_conteiner,mainGroupFragment);}
        else{

        }
        transaction.commit();}else{
            UnsignedMainGroupFragment unsignedMainGroupFragment=new UnsignedMainGroupFragment();
            unsignedMainGroupFragment.setGroup(group);

            transaction=getSupportFragmentManager().beginTransaction();
            transaction.add(R.id.group_fragments_conteiner,unsignedMainGroupFragment);
            transaction.commit();
        }
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){
        Intent intent = new Intent(this, AuthorizedUserActivity.class);
        intent.putExtra("group",group);
        startActivity(intent);}else{
            Intent intent1=new Intent(this,Main2Activity.class);
            startActivity(intent1);
        }
    }
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(resultCode==RESULT_OK)
            if(requestCode==1){

               Uri uri=data.getData();
               Log.d("URI",uri.toString());
                fileRepository.loadFiletoServer(user.getToken(),uri);

            }
    }

    @Override
    public void getResponse(AddFileResponseModel addFileResponseModel) {
        Log.d("Test",addFileResponseModel.getFileName());
        addCourseMethodsPresenter.addPlan(user.getToken(),group.getGroupInfo().getId(),addFileResponseModel.getFileName());

    }

    @Override
    public void getFile(ResponseBody file) {

    }

    @Override
    public void getResponseAfterAddCourse() {
        Log.d("ResponseAfterAddCourse","2");
        onResume();
    }

    @Override
    public void getResponseAfterYourResponse() {

    }
}
