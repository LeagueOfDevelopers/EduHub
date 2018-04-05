package ru.lod_misis.user.eduhub.Adapters.PlaceHolder;

import android.app.Dialog;
import android.content.Context;
import android.util.Log;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Classes.MemberRole;
import ru.lod_misis.user.eduhub.Models.Group.Member;

import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.LongClick;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.Parent;
import com.mindorks.placeholderview.annotations.expand.ParentPosition;
import com.mindorks.placeholderview.annotations.expand.SingleTop;

/**
 * Created by User on 12.03.2018.
 */
@Parent
@SingleTop
@Layout(R.layout.group_members_item)
public class MemberHeaderView  {
    @View(R.id.UserImage)
    private ImageView userImage;

    @View(R.id.UserName)
    private TextView userName;

    @View(R.id.UserRole)
    private TextView userRole;
    @View(R.id.paid)
    private ImageView paid;

    @LongClick(R.id.member_card)
    private void onLongClickListener(){

    }
    @ParentPosition
    private int mParentPosition;

    private Context context;
    private Member member;


    Dialog dialog;

    public MemberHeaderView(Member member,Context context) {
        this.member=member;
        this.context=context;
    }

    @Resolve
    private void onResolved() {

        if(member.getAvatarLink()==null){
            userImage.setImageResource(R.drawable.ic_launcher_background);}
        else {



        }
        Log.e("Error Role",member.getRole()+"");
        switch (member.getRole()+""){
            case 1+"":{userRole.setText(MemberRole.Участник.toString()); break;}
            case 2+"":{userRole.setText(MemberRole.Создатель.toString());break;}
            case 3+"":{userRole.setText(MemberRole.Учитель.toString()); break;}
        }
        userName.setText(member.getName());
        if(member.getPaid()){
            paid.setImageResource(R.drawable.ic_wallet_24dp);
        }



    }

}

