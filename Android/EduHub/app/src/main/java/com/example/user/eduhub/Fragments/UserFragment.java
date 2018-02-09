package com.example.user.eduhub.Fragments;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.example.user.eduhub.Adapters.EmptyGroupAdapter;
import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Fakes.FakeGroupRepository;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.MainActivity;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by user on 14.12.2017.
 */

public class UserFragment extends android.support.v4.app.Fragment implements IGroupListView {
    ArrayList<Group> groups;
    SwipeRefreshLayout swipeContainer;
    GroupsPresenter groupsPresenter=new GroupsPresenter(this);
    FakeGroupRepository fakeGroupRepository=new FakeGroupRepository(this);
    FakesButton fakesButton=new FakesButton();
    EmptyGroupAdapter emptyGroupAdapter=new EmptyGroupAdapter();


    RecyclerView recyclerView;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_for_users_and_teachers, null);
        Button btn=getActivity().findViewById(R.id.btn);

        btn.setText("ЗАРЕГИСТРИРОВАТЬСЯ И НАЧАТЬ РАБОТУ");
        btn.setTextSize(10);
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);
        if(!fakesButton.getCheckButton()){

            groupsPresenter.loadAllGroupsForUsers();}else{
            fakeGroupRepository.loadAllGroupsForUsers();
        }

        swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupsPresenter.loadAllGroupsForUsers();}else{
                    fakeGroupRepository.loadAllGroupsForUsers();
                }
            }
        });


        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(),MainActivity.class);
                startActivity(intent);
            }
        });
        return v;

    }

    public void setGroups(ArrayList<Group> groups){
        this.groups=groups;
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
}
