package com.example.user.eduhub.Adapters.PlaceHolder;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.CardView;
import android.widget.TextView;

import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.GroupActivity;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.UserProfile.Review;
import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.ChildPosition;
import com.mindorks.placeholderview.annotations.expand.ParentPosition;

import java.io.Serializable;

/**
 * Created by User on 01.02.2018.
 */
@Layout(R.layout.group_card)
public class InvetationItemView {
    @ParentPosition
    private int mParentPosition;

    @ChildPosition
    private int mChildPosition;

    @View(R.id.participants)
    private TextView participants;

    @View(R.id.name_of_group)
    private TextView name;

    @View(R.id.type_of_education)
    private TextView type;

    @View(R.id.cost)
    private TextView cost;

    @View(R.id.tags)
    private TextView tags;

    @View(R.id.group_card)
    private  CardView cv;

    private Context context;
    private Group group;
    private Activity activity;

    public InvetationItemView(Context context, Group group,Activity activity) {
        this.context = context;
        this.group=group;
        this.activity=activity;

    }

    @Resolve
    private void onResolved() {
        participants.setText(group.getNumberOfMembers()+"/"+group.getGroupInfo().getSize());
        name.setText(group.getGroupInfo().getTitle());
        switch (String.valueOf(group.getGroupInfo().getGroupType())){
            case "1":{type.setText(TypeOfEducation.Lecture.toString());break;}
            case "2":{type.setText(TypeOfEducation.Seminar.toString());break;}
            case "3":{type.setText(TypeOfEducation.MasterClass.toString());break;}
        }
        cost.setText(group.getGroupInfo().getMoneyPerUser()+"");
        tags.setText(group.getGroupInfo().getTags().toString());

        cv.setOnClickListener(click->{
            Intent intent = new Intent(activity, GroupActivity.class);
            intent.putExtra("group",(Serializable)group);
            activity.startActivity(intent);
        });
    }

}