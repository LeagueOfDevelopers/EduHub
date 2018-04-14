package ru.lod_misis.user.eduhub.Fragments;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.Toast;

import ru.lod_misis.user.eduhub.Adapters.EmptyGroupAdapter;
import ru.lod_misis.user.eduhub.Adapters.GroupAdapter;
import ru.lod_misis.user.eduhub.CreateGroupActivity;
import ru.lod_misis.user.eduhub.Fakes.FakeGroupRepository;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.IGroupListView;
import ru.lod_misis.user.eduhub.Interfaces.View.IRefreshTokenView;
import ru.lod_misis.user.eduhub.Main2Activity;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.GroupsPresenter;
import ru.lod_misis.user.eduhub.Presenters.RefreshTokenPresenter;
import com.example.user.eduhub.R;
import com.jakewharton.retrofit2.adapter.rxjava2.HttpException;

import java.net.SocketTimeoutException;
import java.util.ArrayList;

/**
 * Created by User on 15.01.2018.
 */

public class UsersGroupsFragment extends Fragment implements IGroupListView,IRefreshTokenView {
    RecyclerView recyclerView;
    private User user;
    SwipeRefreshLayout swipeContainer;
    GroupsPresenter groupsPresenter=new GroupsPresenter(this);
    FakeGroupRepository fakeGroupRepository=new FakeGroupRepository(this);
    FakesButton fakesButton=new FakesButton();
    EmptyGroupAdapter emptyGroupAdapter=new EmptyGroupAdapter();
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    SharedPreferences sharedPreferences;
    RefreshTokenPresenter refreshTokenPresenter=new RefreshTokenPresenter(this);
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.users_groups, null);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Мои группы");
        sharedPreferences=getActivity().getSharedPreferences("User", Context.MODE_PRIVATE);
        Button button=v.findViewById(R.id.create_group_btn);
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);
        Log.d("UserId",user.getUserId());
        if(!fakesButton.getCheckButton()){

            groupsPresenter.loadUsersGroup(user.getToken(),user.getUserId(),getContext());}else{
            fakeGroupRepository.loadUsersGroup(user.getToken(),user.getUserId(),getContext());
        }
        button.setOnClickListener(click->{
            Intent intent=new Intent(getActivity(), CreateGroupActivity.class);
            startActivity(intent);
        });
        swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){

                    groupsPresenter.loadUsersGroup(user.getToken(),user.getUserId(),getContext());}else{
                    fakeGroupRepository.loadUsersGroup(user.getToken(),user.getUserId(),getContext());
                }
            }
        });

        return v;
    }

    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void getError(Throwable error) {
        switch (((HttpException) error).code()){
            case 401:{refreshTokenPresenter.refreshToken(user.getToken(),getContext());}

        }

        if(error instanceof SocketTimeoutException){
        MakeToast("Возможно у Вас пропалосоединение с интернетом");
    }
    }

    @Override
    public void getGroups(ArrayList<Group> groups) {
        if(groups.size()==0){
            recyclerView.setAdapter(emptyGroupAdapter);
        }else{
            GroupAdapter adapter=new GroupAdapter(groups,getActivity(),getContext());

            recyclerView.setAdapter(adapter);
        }
        swipeContainer.setRefreshing(false);
    }

    public void setToken(User user) {
        this.user = user;
    }
    @Override
    public void getResponse(User user) {
        savedDataRepository.SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail(),user.getTeacher(),sharedPreferences);
        groupsPresenter.loadUsersGroup(user.getToken(),user.getUserId(),getContext());
    }

    @Override
    public void getThrowable() {
        Intent intent=new Intent(getActivity(), Main2Activity.class);
        SharedPreferences.Editor editor=sharedPreferences.edit();
        editor.clear();
        editor.commit();
        getActivity().startActivity(intent);
    }
    private void MakeToast(String s) {
        Toast toast = Toast.makeText(getActivity().getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }
}
