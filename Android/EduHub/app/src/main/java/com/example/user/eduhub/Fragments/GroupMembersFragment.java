package com.example.user.eduhub.Fragments;

import android.os.Bundle;
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
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 04.01.2018.
 */

public class GroupMembersFragment extends android.support.v4.app.Fragment implements IGroupView {
    private Group group;
    RecyclerView recyclerView;
   SwipeRefreshLayout swipeConteiner;
   FakesButton fakesButton=new FakesButton();
   GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
   FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.group_members_fragment, null);
        recyclerView=v.findViewById(R.id.recycler);
        swipeConteiner=v.findViewById(R.id.swipeConteinerForMembers);
        if(!fakesButton.getCheckButton()){
            Log.d("GroupIdMembersFrag",group.getGroupInfo().getId());
            groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
        }
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

        ArrayList<Member> members=(ArrayList<Member>) group.getMembers();
        GroupMembersAdapter adapter=new GroupMembersAdapter(members);
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
}
