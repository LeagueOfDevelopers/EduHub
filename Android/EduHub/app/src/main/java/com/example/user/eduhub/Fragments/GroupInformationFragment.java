package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;


import com.example.user.eduhub.Fakes.FakeGroupInformationPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;
import com.example.user.eduhub.R;



/**
 * Created by User on 05.01.2018.
 */

public class GroupInformationFragment extends Fragment implements IGroupView {
    private Group group;

    public void setGroup(Group group) {
        this.group = group;
    }

    TextView members;
    TextView cost;
    TextView tags;
    TextView discription;
    SwipeRefreshLayout swipeConteiner;
    FakesButton fakesButton=new FakesButton();
    GroupInformationPresenter groupInformationPresenter=new GroupInformationPresenter(this);
    FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.group_information_fragment, null);
        members=v.findViewById(R.id.members);
        cost=v.findViewById(R.id.cost);
        tags=v.findViewById(R.id.tags);
        discription=v.findViewById(R.id.discription);
        if(!fakesButton.getCheckButton()){
            groupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());}
        else {

            fakeGroupInformationPresenter.loadGroupInformation(group.getGroupInfo().getId());
        }

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
        members.setText(group.getGroupInfo().getMemberAmount()+"/"+group.getGroupInfo().getSize());
        cost.setText("$"+group.getGroupInfo().getCost());
        for (String tag:group.getGroupInfo().getTags()
             ) {        tags.setText(tags.getText().toString()+tag+" ");


        }

       // discription.setText(group.getGroupInfo().getDescription());
    }
}
