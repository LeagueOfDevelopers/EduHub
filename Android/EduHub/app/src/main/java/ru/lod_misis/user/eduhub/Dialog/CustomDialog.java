package ru.lod_misis.user.eduhub.Dialog;

import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.Window;
import android.widget.Button;

import com.example.user.eduhub.R;

import ru.lod_misis.user.eduhub.Fakes.FakeSignInUserToGroupPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Fragments.GroupInformationFragment;
import ru.lod_misis.user.eduhub.Fragments.MainGroupFragment;
import ru.lod_misis.user.eduhub.Interfaces.IFragmentsActivities;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.ISignInUserToGroupPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ISignInUserToGroupView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Presenters.SignInUserToGroupPresenter;

/**
 * Created by User on 17.04.2018.
 */

public class CustomDialog extends Dialog implements ISignInUserToGroupView {
    public Context context;
    Button teacher;
    Button student;
    FakesButton fakessButton=new FakesButton();
    String token;
    Group group;
    SignInUserToGroupPresenter signInUserToGroupPresenter=new SignInUserToGroupPresenter(this);
    FakeSignInUserToGroupPresenter fakeSignInUserToGroupPresenter=new FakeSignInUserToGroupPresenter(this);
    IFragmentsActivities fragmentsActivities;
    GroupInformationFragment groupInformationFragment;
    public CustomDialog(@NonNull Context context, Group group,String token,IFragmentsActivities fragmentsActivities,GroupInformationFragment groupInformationFragment) {
        super(context);
        this.context=context;
        this.group=group;
        this.token=token;
        this.groupInformationFragment=groupInformationFragment;
        this.fragmentsActivities=fragmentsActivities;
    }

    public CustomDialog(@NonNull Context context, int themeResId) {
        super(context, themeResId);
    }

    protected CustomDialog(@NonNull Context context, boolean cancelable, @Nullable OnCancelListener cancelListener) {
        super(context, cancelable, cancelListener);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.custom_dialog);
        teacher=findViewById(R.id.teacher);
        student=findViewById(R.id.student);
        teacher.setOnClickListener(click->{
            if(!fakessButton.getCheckButton()){
                Log.d("CheckButton",fakessButton.getCheckButton().toString());
                signInUserToGroupPresenter.signInTeacherToGroup(token,group.getGroupInfo().getId(),context);}
            else{
                fakeSignInUserToGroupPresenter.signInTeacherToGroup(token,group.getGroupInfo().getId(),context);
            }
        });
        student.setOnClickListener(click->{
            if(!fakessButton.getCheckButton()){
                Log.d("CheckButton",fakessButton.getCheckButton().toString());
                signInUserToGroupPresenter.signInUserToGroup(token,group.getGroupInfo().getId(),getContext());}
            else{
                fakeSignInUserToGroupPresenter.signInUserToGroup(token,group.getGroupInfo().getId(),getContext());
            }
        });


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
        mainGroupFragment.setGroupInformationFragment(groupInformationFragment);
        fragmentsActivities.switchingFragmets(mainGroupFragment);
        dismiss();
    }

}
