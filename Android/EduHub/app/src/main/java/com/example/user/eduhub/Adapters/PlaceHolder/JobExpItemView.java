package com.example.user.eduhub.Adapters.PlaceHolder;

import android.content.Context;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.util.Log;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.TagsAdapter;
import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;
import com.example.user.eduhub.R;
import com.mindorks.placeholderview.annotations.Layout;
import com.mindorks.placeholderview.annotations.Resolve;
import com.mindorks.placeholderview.annotations.View;
import com.mindorks.placeholderview.annotations.expand.ChildPosition;
import com.mindorks.placeholderview.annotations.expand.ParentPosition;

import java.util.ArrayList;

/**
 * Created by User on 03.02.2018.
 */

@Layout(R.layout.groups_item_view)
public class JobExpItemView {
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
    private RecyclerView recyclerView;

    @View(R.id.group_card)
    private CardView cv;

    private Context context;
    private Group group;


    public JobExpItemView(Context context, Group group) {
        this.context = context;
        this.group=group;


    }

    @Resolve
    private void onResolved() {
        participants.setText(group.getGroupInfo().getMemberAmount()+"/"+group.getGroupInfo().getSize());
        name.setText(group.getGroupInfo().getTitle());
        switch (String.valueOf(group.getGroupInfo().getGroupType())){
            case "1":{type.setText(TypeOfEducation.Лекция.toString());break;}
            case "2":{type.setText(TypeOfEducation.Семинар.toString());break;}
            case "3":{type.setText(TypeOfEducation.МастерКласс.toString());break;}
        }
        Log.d("Tags",group.getGroupInfo().getTags().toString());
        recyclerView.setHasFixedSize(true);
        StaggeredGridLayoutManager staggeredGridLayoutManager=new StaggeredGridLayoutManager(2,StaggeredGridLayoutManager.HORIZONTAL);
        recyclerView.setLayoutManager(staggeredGridLayoutManager);
        TagsAdapter adapter=new TagsAdapter((ArrayList<String>) group.getGroupInfo().getTags());
        recyclerView.setAdapter(adapter);
        cost.setText(group.getGroupInfo().getCost().toString());

    }
}
