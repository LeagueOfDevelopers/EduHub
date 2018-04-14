package ru.lod_misis.user.eduhub.Fragments;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.ExpandablePlaceHolderView;

import java.util.ArrayList;

import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.Empty_notifications;
import ru.lod_misis.user.eduhub.Adapters.PlaceHolder.NotificationHeaderView;
import ru.lod_misis.user.eduhub.Fakes.FakeInvitationsPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.INotificationsView;
import ru.lod_misis.user.eduhub.Models.Notivications.Invitation;
import ru.lod_misis.user.eduhub.Models.Notivications.Notification;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.NotificationsPresenter;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 06.04.2018.
 */

public class NotificationsFragment extends Fragment implements INotificationsView {
    ExpandablePlaceHolderView expandablePlaceHolderView;
    NotificationsPresenter invitationsPresenter=new NotificationsPresenter(this);
    User user;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    FakeInvitationsPresenter fakeInvitationsPresenter=new FakeInvitationsPresenter(this);
    SwipeRefreshLayout swipeRefreshLayout;
    FakesButton fakesButton=new FakesButton();
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
            invitationsPresenter.getAllNotifications(user.getToken(),getContext());}else{
            fakeInvitationsPresenter.loadInvitations(user.getToken(),getContext());
        }
        swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                Log.d("123","123");
                i=0;
                expandablePlaceHolderView.removeAllViews();
                if(!fakesButton.getCheckButton()){
                    invitationsPresenter.getAllNotifications(user.getToken(),getContext());}else{
                    fakeInvitationsPresenter.loadInvitations(user.getToken(),getContext());
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
    public void getInvitations(ArrayList<Invitation> invitations) {

    }

    @Override
    public void getAllNotifications(ArrayList<Notification> notifications) {
        if(notifications.size()!=0){
        swipeRefreshLayout.setRefreshing(false);
        Log.d("Notifications",notifications.size()+"");
        for (Notification notification:notifications) {
            expandablePlaceHolderView.addView(new NotificationHeaderView(notification));


        }
    }else{
            expandablePlaceHolderView.addView(new Empty_notifications());
        }
    }
}
