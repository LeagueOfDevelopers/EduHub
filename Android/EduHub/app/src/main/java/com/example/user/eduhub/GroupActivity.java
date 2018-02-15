package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.widget.ImageButton;

import com.example.user.eduhub.Fakes.FakeGroupInformationPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Fragments.MainGroupFragment;
import com.example.user.eduhub.Fragments.UnsignedMainGroupFragment;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;

public class GroupActivity extends AppCompatActivity
        implements  IFragmentsActivities,IGroupView {
    Group group;
    FragmentTransaction transaction;
    MainGroupFragment mainGroupFragment;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;
    GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
    FakesButton fakesButton=new FakesButton();
    FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_group);
        Intent intent=getIntent();
        group=(Group) intent.getSerializableExtra("group") ;
        ImageButton back=findViewById(R.id.back);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("");
        setSupportActionBar(toolbar);
        SharedPreferences sPref=getSharedPreferences("User",MODE_PRIVATE);
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
            Intent intent1 = new Intent(this,AuthorizedUserActivity.class);

            startActivity(intent1);
        });

    }






    @Override
    public void switchingFragmets(Fragment fragment) {
        transaction=getSupportFragmentManager().beginTransaction();
        if(fragment instanceof MainGroupFragment){

            ((MainGroupFragment) fragment).setGroup(group);
        }
        transaction.replace(R.id.group_fragments_conteiner,fragment);
        transaction.addToBackStack(null);
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
        transaction.add(R.id.group_fragments_conteiner,mainGroupFragment);
        transaction.commit();}else{
            UnsignedMainGroupFragment unsignedMainGroupFragment=new UnsignedMainGroupFragment();
            unsignedMainGroupFragment.setGroup(group);

            transaction=getSupportFragmentManager().beginTransaction();
            transaction.add(R.id.group_fragments_conteiner,unsignedMainGroupFragment);
            transaction.commit();
        }



    }
}
