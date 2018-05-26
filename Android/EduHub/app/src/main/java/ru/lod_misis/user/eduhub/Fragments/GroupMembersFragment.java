package ru.lod_misis.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import ru.lod_misis.user.eduhub.Adapters.GroupMembersAdapter;
import ru.lod_misis.user.eduhub.Fakes.FakeGroupInformationPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.IUpdateList;
import ru.lod_misis.user.eduhub.Interfaces.View.IGroupView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Member;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.GroupInformationPresenter;
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
        if(!fakesButton.getCheckButton()){}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());
        }
        swipeConteiner.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                if(!fakesButton.getCheckButton()){
                    groupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());}
                else {

                    fakeGroupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());
                }
            }
        });

        return v;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(!fakesButton.getCheckButton()){
            Log.d("GroupIdMembersFrag",group.getGroupInfo().getId()+"");
            groupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());}

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
        GroupMembersAdapter adapter;

        Log.d("GroupIdInMemberAdapter",this.group.getGroupInfo().getId());
        ArrayList<Member> members=(ArrayList<Member>) group.getMembers();
        if(user.getUserId()!=null){
         adapter=new GroupMembersAdapter(members,user,getActivity(),this.group,this,getContext());}else{
            adapter=new GroupMembersAdapter(members,getActivity(),this.group,this,getContext());
        }
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
            groupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(user.getToken(),group.getGroupInfo().getId(),getContext());
        }
    }
}