package com.example.user.eduhub.Fragments;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.example.user.eduhub.Adapters.EmptyGroupAdapter;
import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.CreateGroupActivity;
import com.example.user.eduhub.Fakes.FakeGroupRepository;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 15.01.2018.
 */

public class UsersGroupsFragment extends Fragment implements IGroupListView {
    RecyclerView recyclerView;
    private User user;
    SwipeRefreshLayout swipeContainer;
    GroupsPresenter groupsPresenter=new GroupsPresenter(this);
    FakeGroupRepository fakeGroupRepository=new FakeGroupRepository(this);
    FakesButton fakesButton=new FakesButton();
    EmptyGroupAdapter emptyGroupAdapter=new EmptyGroupAdapter();

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.users_groups, null);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Мои группы");
        Button button=v.findViewById(R.id.create_group_btn);
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);
        if(!fakesButton.getCheckButton()){

            groupsPresenter.loadUsersGroup(user.getToken(),user.getUserId());}else{
            fakeGroupRepository.loadUsersGroup(user.getToken(),user.getUserId());
        }
        button.setOnClickListener(click->{
            Intent intent=new Intent(getActivity(), CreateGroupActivity.class);
            startActivity(intent);
        });
        swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupsPresenter.loadAllGroupsForUsers();}else{
                    fakeGroupRepository.loadAllGroupsForUsers();
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
}
