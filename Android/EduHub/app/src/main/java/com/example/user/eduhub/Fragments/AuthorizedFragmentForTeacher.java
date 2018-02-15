package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.EmptyGroupAdapter;
import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Fakes.FakeGroupRepository;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;

import java.util.ArrayList;

/**
 * Created by User on 09.02.2018.
 */

public class AuthorizedFragmentForTeacher extends android.support.v4.app.Fragment implements IGroupListView {
    RecyclerView recyclerView;
    ArrayList<Group> groups=new ArrayList<>();
    SwipeRefreshLayout swipeContainer;
    GroupsPresenter groupsPresenter=new GroupsPresenter(this);
    FakeGroupRepository fakeGroupRepository=new FakeGroupRepository(this);
    FakesButton fakesButton=new FakesButton();
    EduHubApi eduHubApi;
    TextView textView;
    EmptyGroupAdapter emptyGroupAdapter=new EmptyGroupAdapter();

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_for_users_and_teachers, null);
        v.findViewById(R.id.btn).setVisibility(View.GONE);
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);
        recyclerView.setLayoutManager(llm);
        if(!fakesButton.getCheckButton()){

            groupsPresenter.loadAllGroupsForTeachers();}else{
            fakeGroupRepository.loadAllGroupsForTeachers();
        }

        swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupsPresenter.loadAllGroupsForTeachers();}else{
                    fakeGroupRepository.loadAllGroupsForTeachers();
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
        Log.d("checkButton",fakesButton.getCheckButton().toString());
        if(groups.size()==0){
            recyclerView.setAdapter(emptyGroupAdapter);
        }else{
            GroupAdapter adapter=new GroupAdapter(groups,getActivity(),getContext());

            recyclerView.setAdapter(adapter);
        }
        swipeContainer.setRefreshing(false);
    }
}

