package ru.lod_misis.user.eduhub;

import android.content.Context;
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
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.user.eduhub.R;

import ru.lod_misis.user.eduhub.Adapters.EmptyAdapterForSearch;
import ru.lod_misis.user.eduhub.Adapters.InviteUsersAdapter;
import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapterForMemberRole;
import ru.lod_misis.user.eduhub.Fakes.FakeInviteUserPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakeSearchUsers;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.IInviteCallback;
import ru.lod_misis.user.eduhub.Interfaces.View.IInviteUserView;
import ru.lod_misis.user.eduhub.Interfaces.View.ISearchResponse;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserSearchProfile;
import ru.lod_misis.user.eduhub.Presenters.InviteUserPresenter;
import ru.lod_misis.user.eduhub.Presenters.SearchUserPresenter;

import java.util.List;

public class InviteUserToGroup extends AppCompatActivity implements ISearchResponse,IInviteCallback,IInviteUserView {

    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;
    String groupId;
    Group group;
    Context context=this;
    RecyclerView recyclerView;
    SearchUserPresenter searchUserPresenter=new SearchUserPresenter(this);
    FakesButton fakesButton=new FakesButton();
    InviteUserPresenter inviteUserPresenter=new InviteUserPresenter(this);
    FakeInviteUserPresenter fakeInviteUserPresenter=new FakeInviteUserPresenter(this);
    FakeSearchUsers fakeSearchUsers=new FakeSearchUsers(this);
    String role;
    Boolean flag;
    Boolean isTeacher;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_invite_user_to_group);
        SharedPreferences sPref=getSharedPreferences("User",MODE_PRIVATE);
        Toolbar toolbar=findViewById(R.id.toolbar);
        Intent intent=getIntent();
        group=(Group) intent.getSerializableExtra("group");
        groupId=group.getGroupInfo().getId();
        user= savedDataRepository.loadSavedData(sPref);
        toolbar.setTitle("Приглашение пользователя");
        EditText edit=findViewById(R.id.invite);
        TextView textView=findViewById(R.id.group_name);
        textView.setText(group.getGroupInfo().getTitle());
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
                    if(role.equals("Ученик")){
                        isTeacher=false;
                    }else{
                        isTeacher=true;
                    }
                searchUserPresenter.searchUserForInvitation(edit.getText().toString(),groupId,this,isTeacher);}
                else {fakeSearchUsers.searchUser(edit.getText().toString(),this);}}

        });
        backButton.setOnClickListener(click->{
            onBackPressed();
        });
        edit.setOnKeyListener(new View.OnKeyListener()
                                     {
                                         public boolean onKey(View v, int keyCode, KeyEvent event)
                                         {
                                             if((keyCode == KeyEvent.KEYCODE_ENTER)) {
                                                 if(!edit.getText().toString().equals("")){
                                                     if(!fakesButton.getCheckButton()){
                                                         if(role.equals("Ученик")){
                                                             isTeacher=false;
                                                         }else{
                                                             isTeacher=true;
                                                         }
                                                         searchUserPresenter.searchUserForInvitation(edit.getText().toString(),groupId,context,isTeacher);}
                                                     else {fakeSearchUsers.searchUser(edit.getText().toString(),context);}}
                                                 return true;
                                             }
                                             return false;
                                         }
                                     }
        );
        Spinner spinner=findViewById(R.id.UserRole);
        String[] roles={"Ученик","Учитель"};
        SpinnerAdapterForMemberRole adapter=new SpinnerAdapterForMemberRole(this,R.layout.spinner_item2, roles);
        spinner.setAdapter(adapter);
        // заголовок
        spinner.setPrompt("Type of education");
        // выделяем элемент

        // устанавливаем обработчик нажатия
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                role=roles[position];
                Log.d("ROLRLRL",role);

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

        Log.d("ROLOLO",role);
        if(userProfile.size()!=0){
        InviteUsersAdapter inviteUsersAdapter=new InviteUsersAdapter(userProfile,this,this);
        recyclerView.setAdapter(inviteUsersAdapter);}else{
            EmptyAdapterForSearch emptyAdapterForSearch=new EmptyAdapterForSearch();
            recyclerView.setAdapter(emptyAdapterForSearch);
        }
    }

    @Override
    public void getError(Throwable error) {

    }


    @Override
    public void InviteCallBack(String invitedId) {
        if(!fakesButton.getCheckButton()){
            inviteUserPresenter.inviteUser(invitedId,role,groupId,user.getToken(),this);}
        else {
            fakeInviteUserPresenter
                    .inviteUser(invitedId, role,groupId,user.getToken(),this);
        }
    }

    @Override
    public void getResponse() {
        Intent intent2=new Intent(this,AuthorizedUserActivity.class);
        startActivity(intent2);
    }
}
