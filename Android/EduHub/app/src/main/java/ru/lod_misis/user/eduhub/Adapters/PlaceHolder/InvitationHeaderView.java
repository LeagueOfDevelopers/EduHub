package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.util.Log;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Fakes.FakeChangeStatusOfInvitation;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.GroupActivity;
import ru.lod_misis.user.eduhub.Interfaces.View.IChangeStatusOfInvitationView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Notivications.Invitation;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.СhangeStatusOfInvitationPresenter;
import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.Collapse;
import com.mindorks.placeholderview.annotations.expand.Expand;
import com.mindorks.placeholderview.annotations.expand.Parent;
import com.mindorks.placeholderview.annotations.expand.ParentPosition;
import com.mindorks.placeholderview.annotations.expand.SingleTop;
import com.mindorks.placeholderview.annotations.expand.Toggle;

import java.io.Serializable;

/**
 * Created by User on 01.02.2018.
 */
@Parent
@SingleTop
@Layout(R.layout.invitation_header)
public class InvitationHeaderView implements IChangeStatusOfInvitationView {
    @View(R.id.name_of_invite)
    private TextView name_of_invite;

    @View(R.id.text_of_invitation)
    private TextView textOfInvitation;

    @View(R.id.toogle_notification)
    private ImageView toggleView;

    @Toggle(R.id.toogle_notification)
    private ImageView toggleBtn;

    @ParentPosition
    private int mParentPosition;

    private Context context;
    private Invitation heading;
    private Group group;
    InvetationItemView invetationItemView;
    Activity activity;
    User user;
    СhangeStatusOfInvitationPresenter сhangeStatusOfInvitationPresenter=new СhangeStatusOfInvitationPresenter(this);
    FakesButton fakesButton=new FakesButton();
    FakeChangeStatusOfInvitation fakeChangeStatusOfInvitation=new FakeChangeStatusOfInvitation(this);
    Dialog dialog;

    public InvitationHeaderView(Context context, Invitation heading, Activity activity, User user, Group group) {
        this.context = context;
        this.heading=heading;
        this.user=user;
        this.activity=activity;
        this.group=group;
    }

    @Resolve
    private void onResolved() {

        name_of_invite.setText(heading.getToUserName());
        String role="неизвестно";
        switch (heading.getSuggestedRole()){
            case "1":{role="ученика";break;}
            case "3":{role="учителя";break;}
        }
        textOfInvitation.setText(heading.getFromUserName()+" пригласил вас в группу в роли "+role+"! Ниже представлена информация о группе.");
        toggleView.setImageDrawable(context.getResources().getDrawable(R.drawable.ic_expand_less_black_24dp));

        textOfInvitation.setOnClickListener(click->{
            onCreateDialog(1).show();
        });

    }

    @Expand
    private void onExpand(){

        toggleView.setImageDrawable(context.getResources().getDrawable(R.drawable.ic_expand_more_black_24dp));
    }

    @Collapse
    private void onCollapse(){
        toggleView.setImageDrawable(context.getResources().getDrawable(R.drawable.ic_expand_less_black_24dp));

    }
    protected Dialog onCreateDialog(int id) {
        if (id == 1) {
            AlertDialog.Builder adb = new AlertDialog.Builder(context);
            // заголовок
            adb.setTitle(R.string.signInToGroup);
            // сообщение
            adb.setMessage(R.string.areShureSignInToGroup);
            // иконка
            adb.setIcon(android.R.drawable.ic_dialog_info);
            // кнопка отрицательного ответа
            adb.setNegativeButton(R.string.no, myClickListener);
            // кнопка положительного ответа
            adb.setPositiveButton(R.string.yes, myClickListener);


            adb.setNeutralButton(R.string.cansel,myClickListener);
            // создаем диалог
            return adb.create();
        }
        return null;
    }

     DialogInterface.OnClickListener myClickListener = new DialogInterface.OnClickListener() {
        public void onClick(DialogInterface dialog, int which) {
            switch (which) {
                // положительная кнопка
                case Dialog.BUTTON_POSITIVE:
                    if(!fakesButton.getCheckButton()){
                    сhangeStatusOfInvitationPresenter.changeStatus("Accepted",user.getToken(),heading.getId());
                    }
                    else {
                        fakeChangeStatusOfInvitation.changeStatus("Accepted",user.getToken(),heading.getId());
                    }
                    break;
                // негативная кнопка
                case Dialog.BUTTON_NEGATIVE:
                    if(!fakesButton.getCheckButton()){
                        сhangeStatusOfInvitationPresenter.changeStatus("Declined",user.getToken(),heading.getId());}
                    else {
                        fakeChangeStatusOfInvitation.changeStatus("Declined",user.getToken(),heading.getId());
                    }
                    break;
                case Dialog.BUTTON_NEUTRAL:
                    break;

            }
        }
    };


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
    public void Possitive() {
        Log.d("groupId",group.getGroupInfo().getId());
        Intent intent = new Intent(activity, GroupActivity.class);
        intent.putExtra("group",(Serializable)group);
        activity.startActivity(intent);
    }

    @Override
    public void Negative() {

    }
}
