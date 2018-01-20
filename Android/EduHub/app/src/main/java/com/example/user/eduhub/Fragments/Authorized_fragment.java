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
import android.widget.Toast;

import com.example.user.eduhub.Adapters.EmptyGroupAdapter;
import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Fakes.FakeGroupRepository;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.Presenters.IGroupRepository;
import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;
import com.example.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 30.12.2017.
 */

public class Authorized_fragment extends android.support.v4.app.Fragment implements IGroupListView {
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
        recyclerView=v.findViewById(R.id.recycler);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        swipeContainer = (SwipeRefreshLayout) v.findViewById(R.id.swipeContainer);
        recyclerView.setLayoutManager(llm);
        if(!fakesButton.getCheckButton()){

            groupsPresenter.loadGroups();}else{
            fakeGroupRepository.loadGroups();
        }

        swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupsPresenter.loadGroups();}else{
                    fakeGroupRepository.loadGroups();
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
    public void getError() {

    }

    @Override
    public void getGroups(ArrayList<Group> groups) {
        if(groups.size()==0){
            recyclerView.setAdapter(emptyGroupAdapter);
        }else{
            GroupAdapter adapter=new GroupAdapter(groups,getActivity());

            recyclerView.setAdapter(adapter);
        }
        swipeContainer.setRefreshing(false);
    }
}
