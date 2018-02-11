package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;

import com.example.user.eduhub.Adapters.InviteUsersAdapter;
import com.example.user.eduhub.Adapters.SpinnerAdapter;
import com.example.user.eduhub.Adapters.SpinnerAdapterForMemberRole;
import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Fakes.FakeSearchUsers;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IInviteUserView;
import com.example.user.eduhub.Interfaces.View.ISearchResponse;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.UserSearchProfile;
import com.example.user.eduhub.Presenters.InviteUserPresenter;
import com.example.user.eduhub.Presenters.SearchUserPresenter;

import java.util.List;

public class InviteUserToGroup extends AppCompatActivity implements ISearchResponse {

    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;
    String groupId;
    RecyclerView recyclerView;
    SearchUserPresenter searchUserPresenter=new SearchUserPresenter(this);
    FakesButton fakesButton=new FakesButton();
    FakeSearchUsers fakeSearchUsers=new FakeSearchUsers(this);
    MemberRole role;
    Boolean flag;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_invite_user_to_group);
        SharedPreferences sPref=getSharedPreferences("User",MODE_PRIVATE);
        Toolbar toolbar=findViewById(R.id.toolbar);
        Intent intent=getIntent();
        groupId=intent.getStringExtra("groupId");
        user= savedDataRepository.loadSavedData(sPref);
        toolbar.setTitle("Приглашение пользователя");
        EditText edit=findViewById(R.id.invite);
        ImageButton backButton=findViewById(R.id.back);
        ImageView searchUser=findViewById(R.id.search_item);
        recyclerView=findViewById(R.id.recycler_view_invitation);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getApplicationContext());
        recyclerView.setLayoutManager(llm);
        Log.d("CheckButton",fakesButton.getCheckButton().toString());
        searchUser.setOnClickListener(click->{
            if(!edit.getText().toString().equals("")){
                if(!fakesButton.getCheckButton()){
                searchUserPresenter.searchUser(edit.getText().toString());}
                else {fakeSearchUsers.searchUser(edit.getText().toString());}}

        });
        backButton.setOnClickListener(click->{
            Intent intent2=new Intent(this,AuthorizedUserActivity.class);
            startActivity(intent2);
        });
        edit.setOnKeyListener(new View.OnKeyListener()
                                     {
                                         public boolean onKey(View v, int keyCode, KeyEvent event)
                                         {
                                             if(
                                                     (keyCode == KeyEvent.KEYCODE_ENTER)) {
                                                 if(!edit.getText().toString().equals("")){
                                                     if(!fakesButton.getCheckButton()){
                                                         searchUserPresenter.searchUser(edit.getText().toString());}
                                                     else {fakeSearchUsers.searchUser(edit.getText().toString());}}
                                                 return true;
                                             }
                                             return false;
                                         }
                                     }
        );
        Spinner spinner=findViewById(R.id.UserRole);
        SpinnerAdapterForMemberRole adapter=new SpinnerAdapterForMemberRole(this,R.layout.spinner_item_for_members_role, MemberRole.values());
        spinner.setAdapter(adapter);
        // заголовок
        spinner.setPrompt("Type of education");
        // выделяем элемент
        spinner.setSelection(0);
        // устанавливаем обработчик нажатия
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                role=MemberRole.values()[position];

            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });
        flag=false;


    }

    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void getResult(List<UserSearchProfile> userProfile) {
        InviteUsersAdapter inviteUsersAdapter=new InviteUsersAdapter(userProfile,this,groupId,0,user.getToken());
        recyclerView.setAdapter(inviteUsersAdapter);
    }

    @Override
    public void getError(Throwable error) {

    }


}