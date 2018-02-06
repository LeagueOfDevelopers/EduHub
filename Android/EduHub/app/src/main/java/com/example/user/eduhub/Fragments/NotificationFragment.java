package com.example.user.eduhub.Fragments;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.user.eduhub.Adapters.PlaceHolder.EmptyInvitationsView;
import com.example.user.eduhub.Adapters.PlaceHolder.InvetationItemView;
import com.example.user.eduhub.Adapters.PlaceHolder.InvitationHeaderView;
import com.example.user.eduhub.Fakes.FakeGroupInformationPresenter;
import com.example.user.eduhub.Fakes.FakeInvitationsPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Interfaces.View.IInvitationsView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Invitation;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.GroupInformationPresenter;
import com.example.user.eduhub.Presenters.InvitationsPresenter;
import com.example.user.eduhub.R;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;

import java.util.ArrayList;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 01.02.2018.
 */

public class NotificationFragment extends Fragment implements IInvitationsView,IGroupView {
    ExpandablePlaceHolderView expandablePlaceHolderView;
    InvitationsPresenter invitationsPresenter=new InvitationsPresenter(this);
    User user;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    FakeInvitationsPresenter fakeInvitationsPresenter=new FakeInvitationsPresenter(this);
    SwipeRefreshLayout swipeRefreshLayout;
    GroupInformationPresenter groupInfirmationPresenter=new GroupInformationPresenter(this);
    FakeGroupInformationPresenter fakeGroupInformationPresenter=new FakeGroupInformationPresenter(this);
    ArrayList<Group> groups=new ArrayList<>();
    FakesButton fakesButton=new FakesButton();
    ArrayList<Invitation> invitations=new ArrayList<>();
    int i=0;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.notification_fragment, null);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Уведомления");
        swipeRefreshLayout=v.findViewById(R.id.swipeConteinerForNotifications);
        expandablePlaceHolderView=v.findViewById(R.id.expandableView);
        SharedPreferences sPref=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        user= savedDataRepository.loadSavedData(sPref);
        if(!fakesButton.getCheckButton()){
            invitationsPresenter.loadInvitations(user.getToken());}else{
            fakeInvitationsPresenter.loadInvitations(user.getToken());
        }
        swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                expandablePlaceHolderView.removeAllViews();
                if(!fakesButton.getCheckButton()){
                    invitationsPresenter.loadInvitations(user.getToken());}else{
                    fakeInvitationsPresenter.loadInvitations(user.getToken());
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
        swipeRefreshLayout.setRefreshing(false);
    }

    @Override
    public void getError(Throwable error) {

    }

    @Override
    public void getInvitations(ArrayList<Invitation> invitations) {
        if(invitations.size()!=0) {
            this.invitations=invitations;
            Log.d("NOTIFICATIONS", invitations.size() + "");
            for (Invitation invitation : invitations
                    ) {
                if(fakesButton.getCheckButton()){
                    Log.e("Check",fakesButton.getCheckButton().toString());
                    fakeGroupInformationPresenter.loadGroupInformation(invitation.getGroupId());
                }else{
                    Log.d("GroupIdInvitation",invitation.getGroupId());
                    groupInfirmationPresenter.loadGroupInformation(invitation.getGroupId());
                }
            }
        }else{
            expandablePlaceHolderView.addView(new EmptyInvitationsView());
        }
    }

    @Override
    public void getInformationAboutGroup(Group group) {
        groups.add(group);
        Log.d("groups",groups.size()+"");

            expandablePlaceHolderView.addView(new InvitationHeaderView(getContext(),invitations.get(i),getActivity(),user,groups.get(i)));
            expandablePlaceHolderView.addView(new InvetationItemView(getContext(),groups.get(i),getActivity()));
        i++;


    }
}

