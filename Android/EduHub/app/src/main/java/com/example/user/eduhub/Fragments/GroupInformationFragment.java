package com.example.user.eduhub.Fragments;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;


import com.example.user.eduhub.Adapters.TagsAdapter;
import com.example.user.eduhub.AuthorizedUserActivity;
import com.example.user.eduhub.Fakes.FakeExitFromGroupPresenter;
import com.example.user.eduhub.Fakes.FakeGroupInformationPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IExitFromGroupView;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.ExitFromGroupPresenter;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;
import com.example.user.eduhub.R;

import java.util.ArrayList;

import static android.content.Context.MODE_PRIVATE;


/**
 * Created by User on 05.01.2018.
 */

public class GroupInformationFragment extends Fragment implements IGroupView,IExitFromGroupView {
    private Group group;


    public void setGroup(Group group) {
        this.group = group;
    }
    Boolean isTeacher=false;
    TextView members;
    TextView cost;
    RecyclerView recyclerView;
    TextView discription;
    SwipeRefreshLayout swipeConteiner;
    FakesButton fakesButton=new FakesButton();
    GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
    FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    ExitFromGroupPresenter exitFromGroupPresenter=new ExitFromGroupPresenter(this);
    FakeExitFromGroupPresenter fakeExitFromGroupPresenter=new FakeExitFromGroupPresenter(this);
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;
    SharedPreferences sharedPreferences;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.group_information_fragment, null);
        sharedPreferences=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        user=savedDataRepository.loadSavedData(sharedPreferences);
        members=v.findViewById(R.id.members);
        cost=v.findViewById(R.id.cost);
        recyclerView=v.findViewById(R.id.tags);
        discription=v.findViewById(R.id.discription);
        if(!fakesButton.getCheckButton()){
            groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
        }
        Button exit=v.findViewById(R.id.exit);

        exit.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
                for (Member member :group.getMembers()
                     ) {
                    if(member.getUserId().equals(user.getUserId())){
                        if(member.getMemberRole()==2){
                            isTeacher=true;
                        }

                    }
                }
                if(isTeacher){
                    exitFromGroupPresenter.exitFromGroupForTeacher(user.getToken(),group.getGroupInfo().getId(),user.getUserId());
                }
                else {
                exitFromGroupPresenter.exitFromGroupForUser(user.getToken(),group.getGroupInfo().getId(),user.getUserId());}
            }else{
                fakeExitFromGroupPresenter.exitFromGroupForTeacher(user.getToken(),group.getGroupInfo().getId(),user.getUserId());
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
        members.setText(group.getGroupInfo().getCurrentAmount()+"/"+group.getGroupInfo().getSize());
        cost.setText("$"+group.getGroupInfo().getCost());

        recyclerView.setHasFixedSize(true);
        StaggeredGridLayoutManager staggeredGridLayoutManager=new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
        recyclerView.setLayoutManager(staggeredGridLayoutManager);
        TagsAdapter adapter=new TagsAdapter((ArrayList<String>) group.getGroupInfo().getTags());
        recyclerView.setAdapter(adapter);

       // discription.setText(group.getGroupInfo().getDescription());
    }

    @Override
    public void getResponse() {
        Intent intent1 = new Intent(getActivity(),AuthorizedUserActivity.class);

        startActivity(intent1);
    }
}
