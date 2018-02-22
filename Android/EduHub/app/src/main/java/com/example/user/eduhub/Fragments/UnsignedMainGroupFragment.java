package com.example.user.eduhub.Fragments;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.widget.CardView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Fakes.FakeSignInUserToGroupPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Interfaces.View.ISignInUserToGroupView;
import com.example.user.eduhub.MainActivity;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.SignInUserToGroupPresenter;
import com.example.user.eduhub.R;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 01.02.2018.
 */

public class UnsignedMainGroupFragment extends Fragment implements ISignInUserToGroupView {
    ViewPager pager;
    ViewPagerAdapter adapter;
    TabLayout tabLayout;
    ChatFragment chat;
    GroupMembersFragment groupMembersFragment;
    GroupInformationFragment groupInformationFragment;
    Group group;
    User user;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    SharedPreferences sPref;
    SignInUserToGroupPresenter signInUserToGroupPresenter=new SignInUserToGroupPresenter(this);
    FakeSignInUserToGroupPresenter fakeSignInUserToGroupPresenter=new FakeSignInUserToGroupPresenter(this);
    FakesButton fakessButton=new FakesButton();
    IFragmentsActivities fragmentsActivities;
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            fragmentsActivities = (IFragmentsActivities) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement onSomeEventListener");
        }
    }
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {


        View v = inflater.inflate(R.layout.unsigned_group, null);
        Toolbar toolbar = getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle(group.getGroupInfo().getTitle());
        TextView groupTitle=v.findViewById(R.id.GroupTitle);
        groupTitle.setText(group.getGroupInfo().getTitle());
        Button signInToGroup=v.findViewById(R.id.signed_to_group);

        sPref =getActivity().getSharedPreferences("User",MODE_PRIVATE);
        pager = v.findViewById(R.id.pager);
        tabLayout = (TabLayout) v.findViewById(R.id.tabs);
        ImageView imageView = v.findViewById(R.id.icon_group);
        imageView.setImageResource(R.mipmap.ic_launcher_round);
        groupInformationFragment = new GroupInformationFragment();
        groupInformationFragment.setGroup(group);
        groupInformationFragment.setFlag(true);
        chat = new ChatFragment();
        chat.setFlag(true);
        groupMembersFragment = new GroupMembersFragment();
        groupMembersFragment.setGroup(group);

        adapter = new ViewPagerAdapter(getFragmentManager());
        adapter.addFragment(groupMembersFragment, "Участники");
        adapter.addFragment(chat, "Чат");
        adapter.addFragment(groupInformationFragment, "Информация");

        pager.setAdapter(adapter);

        tabLayout.setupWithViewPager(pager);
        signInToGroup.setOnClickListener(click->{
            if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){

               user=savedDataRepository.loadSavedData(sPref);
                if(!fakessButton.getCheckButton()){
                    Log.d("CheckButton",fakessButton.getCheckButton().toString());
               signInUserToGroupPresenter.signInUserToGroup(user.getToken(),group.getGroupInfo().getId());}
               else{
                    fakeSignInUserToGroupPresenter.signInUserToGroup(user.getToken(),group.getGroupInfo().getId());
                }

            }else{
                Intent intent = new Intent(getActivity(),MainActivity.class);
                startActivity(intent);
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
    public void getResponse() {
        MainGroupFragment mainGroupFragment=new MainGroupFragment();
        mainGroupFragment.setGroup(group);
        fragmentsActivities.switchingFragmets(mainGroupFragment);
    }
    public void setGroup(Group group) {
        this.group = group;
    }
}
