package ru.lod_misis.user.eduhub.Fragments;

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
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Adapters.ViewPagerAdapter;
import ru.lod_misis.user.eduhub.Dialog.CustomDialog;
import ru.lod_misis.user.eduhub.Fakes.FakeSignInUserToGroupPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.IFragmentsActivities;
import ru.lod_misis.user.eduhub.Interfaces.View.ISignInUserToGroupView;
import ru.lod_misis.user.eduhub.MainActivity;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Member;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.SignInUserToGroupPresenter;
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
    GroupMembersFragment aboutGroupFragment;

    Group group;
    User user;
    Boolean flag=false;
    Boolean isTeacher=false;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    SharedPreferences sPref;
    SignInUserToGroupPresenter signInUserToGroupPresenter=new SignInUserToGroupPresenter(this);
    FakeSignInUserToGroupPresenter fakeSignInUserToGroupPresenter=new FakeSignInUserToGroupPresenter(this);
    FakesButton fakessButton=new FakesButton();
    IFragmentsActivities fragmentsActivities;
    GroupInformationFragment fragment;
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


        Button signInToGroup=v.findViewById(R.id.signed_to_group);

        sPref =getActivity().getSharedPreferences("User",MODE_PRIVATE);
        pager = v.findViewById(R.id.pager);
        tabLayout = (TabLayout) v.findViewById(R.id.tabs);




        user=savedDataRepository.loadSavedData(sPref);


        chat = new ChatFragment();
        chat.setFlag(true);

        aboutGroupFragment.setGroup(group);
        aboutGroupFragment.setUser(user);

        adapter = new ViewPagerAdapter(getFragmentManager());
        adapter.addFragment(aboutGroupFragment, "Информация");
        adapter.addFragment(chat, "Чат");


        pager.setAdapter(adapter);
        if(group.getGroupInfo().getCourseStatus()!=0&&group.getGroupInfo().getCourseStatus()!=1){
            signInToGroup.setVisibility(View.GONE);
        }else{
            if(user.getTeacher()){
                for (Member member:group.getMembers()
                     ) {
                    if(member.getRole()==3){
                        isTeacher=true;
                    }
                }
                if(isTeacher){
                    flag=true;
                }
            }
                if(group.getGroupInfo().getSize()==group.getGroupInfo().getMemberAmount()){
                    signInToGroup.setVisibility(View.GONE);

            }
        }
        tabLayout.setupWithViewPager(pager);
        signInToGroup.setOnClickListener(click->{
            if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){

                if(!user.getTeacher()){
                if(!fakessButton.getCheckButton()){
                    Log.d("CheckButton",fakessButton.getCheckButton().toString());
               signInUserToGroupPresenter.signInUserToGroup(user.getToken(),group.getGroupInfo().getId(),getContext());}
               else{
                    fakeSignInUserToGroupPresenter.signInUserToGroup(user.getToken(),group.getGroupInfo().getId(),getContext());
                }}else{
                    if(!flag){
                    CustomDialog customDialog=new CustomDialog(getContext(),group,user.getToken(),fragmentsActivities,aboutGroupFragment);
                    customDialog.show();
                    }else{
                        signInUserToGroupPresenter.signInUserToGroup(user.getToken(),group.getGroupInfo().getId(),getContext());
                    }
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
        mainGroupFragment.setAdapter(adapter);

        fragmentsActivities.switchingFragmets(mainGroupFragment);
    }
    public void setGroup(Group group) {
        this.group = group;
    }

    public void setAboutGroupFragment(GroupMembersFragment aboutGroupFragment) {
        this.aboutGroupFragment = aboutGroupFragment;
    }


}
