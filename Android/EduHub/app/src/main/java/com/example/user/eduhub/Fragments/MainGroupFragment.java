package com.example.user.eduhub.Fragments;

import android.app.Activity;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ImageView;

import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 12.01.2018.
 */

public class MainGroupFragment extends Fragment {
    ViewPager pager;
    ViewPagerAdapter adapter;
    Chat chat;
    IFragmentsActivities fragmentsActivities;
    GroupMembersFragment groupMembersFragment;
    GroupInformationFragment groupInformationFragment;
    Group group;
    User user;
    SavedDataRepository savedDataRepository;
    Disposable disposable;
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            fragmentsActivities = (IFragmentsActivities) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement onSomeEventListener");
        }
    }
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.main_group_fragment, null);
        ImageButton inviteButton=v.findViewById(R.id.invite_button);
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        disposable= eduHubApi.getInformationAbotGroup(group.getGroupInfo().getId())
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                        response->{
                            group=response;},
                        error->{Log.e("ERROR",error+"");},
                        ()->{ImageView imageView=v.findViewById(R.id.icon_group);
                            imageView.setImageResource(R.mipmap.ic_launcher_round);
                            groupInformationFragment=new GroupInformationFragment();
                            groupInformationFragment.setGroup(group);
                            chat=new Chat();

                            groupMembersFragment=new GroupMembersFragment();
                            groupMembersFragment.setMembers((ArrayList<Member>) group.getMembers());
                            pager=v.findViewById(R.id.pager);
                            adapter=new ViewPagerAdapter(getFragmentManager());
                            adapter.addFragment(groupMembersFragment,"Участники");
                            adapter.addFragment(chat,"Чат");
                            adapter.addFragment(groupInformationFragment,"Информация");

                            pager.setAdapter(adapter);
                            TabLayout tabLayout = (TabLayout) v.findViewById(R.id.tabs);
                            tabLayout.setupWithViewPager(pager);});
        inviteButton.setOnClickListener(click->{
            InviteFragment inviteFragment=new InviteFragment();
            inviteFragment.setGroupId(group.getGroupInfo().getId());
            inviteFragment.setUserId(user);
            fragmentsActivities.switchingFragmets(inviteFragment);
        });

        return v;
    }
    public void setGroup(Group group){
        this.group=group;
    }

    public void setUser(User user) {
        this.user = user;
    }
}
