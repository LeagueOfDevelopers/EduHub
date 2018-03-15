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

import com.example.user.eduhub.Adapters.GroupMembersAdapter;
import com.example.user.eduhub.Fakes.FakeGroupInformationPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.IUpdateList;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 04.01.2018.
 */

public class GroupMembersFragment extends android.support.v4.app.Fragment implements IGroupView,IUpdateList {
    private Group group;
    RecyclerView recyclerView;
   SwipeRefreshLayout swipeConteiner;
   User user;
   FakesButton fakesButton=new FakesButton();
   GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
   FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View v = inflater.inflate(R.layout.group_members_fragment, null);
        recyclerView=v.findViewById(R.id.recycler);
        swipeConteiner=v.findViewById(R.id.swipeConteinerForMembers);

        swipeConteiner.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
                else {

                    fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
                }
            }
        });

        return v;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(!fakesButton.getCheckButton()){
            Log.d("GroupIdMembersFrag",group.getGroupInfo().getId());
            groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
        }
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
Log.d("UserId2",user.getUserId());
        Log.d("GroupIdInMemberAdapter",this.group.getGroupInfo().getId());
        ArrayList<Member> members=(ArrayList<Member>) group.getMembers();
        GroupMembersAdapter adapter=new GroupMembersAdapter(members,user,getActivity(),this.group,this);
        recyclerView.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        recyclerView.setAdapter(adapter);
        swipeConteiner.setRefreshing(false);
        Log.d("sfsd","sdfsdf");
    }

    public void setGroup(Group group) {
        this.group = group;
    }

    public void setUser(User user) {
        this.user = user;
    }

    @Override
    public void updateList() {
        if(!fakesButton.getCheckButton()){
            groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
        }
    }
}