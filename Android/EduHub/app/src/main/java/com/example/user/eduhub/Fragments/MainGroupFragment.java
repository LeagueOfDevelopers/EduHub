package com.example.user.eduhub.Fragments;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.InviteUserToGroup;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 12.01.2018.
 */

public class MainGroupFragment extends Fragment {
    ViewPager pager;
    ViewPagerAdapter adapter;
    TabLayout tabLayout;
    ChatFragment chat;
    IFragmentsActivities fragmentsActivities;
    GroupMembersFragment groupMembersFragment;
    GroupInformationFragment groupInformationFragment;
    Group group;
    User user;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    Toolbar toolbar;
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
        Log.d("Group",group.toString());
         toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle(group.getGroupInfo().getTitle());
        TextView groupTitle=v.findViewById(R.id.GroupTitle);
        groupTitle.setText(group.getGroupInfo().getTitle());
        ImageButton inviteButton=v.findViewById(R.id.invite_button);
        SharedPreferences sPref=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        user= savedDataRepository.loadSavedData(sPref);
        pager=v.findViewById(R.id.pager);
        tabLayout = (TabLayout) v.findViewById(R.id.tabs);
        ImageView imageView=v.findViewById(R.id.icon_group);
        imageView.setImageResource(R.mipmap.ic_launcher_round);
        groupInformationFragment=new GroupInformationFragment();
        groupInformationFragment.setGroup(group);
        chat=new ChatFragment();

        groupMembersFragment=new GroupMembersFragment();
        groupMembersFragment.setGroup(group);
        groupMembersFragment.setUser(user);

        adapter=new ViewPagerAdapter(getFragmentManager());
        adapter.addFragment(groupMembersFragment,"Участники");
        adapter.addFragment(chat,"Чат");
        adapter.addFragment(groupInformationFragment,"Информация");

        pager.setAdapter(adapter);




        inviteButton.setOnClickListener(click->{
            Intent intent=new Intent(getActivity(),InviteUserToGroup.class);
            intent.putExtra("group",group);
            getActivity().startActivity(intent);

        });


        return v;
    }

    @Override
    public void onResume() {
        super.onResume();
        tabLayout.setupWithViewPager(pager);
    }

    public void setGroup(Group group){
        this.group=group;
    }





}
