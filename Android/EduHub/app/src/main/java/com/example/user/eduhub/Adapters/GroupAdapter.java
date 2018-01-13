package com.example.user.eduhub.Adapters;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;
import com.example.user.eduhub.GroupActivity;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by user on 13.12.2017.
 */

public class GroupAdapter extends RecyclerView.Adapter<GroupAdapter.GroupViewHolder>{
    private ArrayList<Group> groups;
    private Activity activity;
    private User user;
    public GroupAdapter(ArrayList<Group> groups, Activity activity){
        this.groups=groups;
        this.activity=activity;
    }
    @Override
    public GroupViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.group_card, parent, false);
        GroupViewHolder gvh = new GroupViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(GroupViewHolder holder, final int position) {
        holder.name.setText(groups.get(position).getGroupInfo().getTitle());
        holder.users.setText(0+"/"+groups.get(position).getGroupInfo().getSize());
        holder.cost.setText("$"+groups.get(position).getGroupInfo().getMoneyPerUser());
        holder.tags.setText(groups.get(position).getGroupInfo().getTags().toString());
        holder.typeOfEducation.setText(String.valueOf(groups.get(position).getGroupInfo().getGroupType()));
        holder.cv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Group group=groups.get(position);
                Log.d("gROUPiD",group.getGroupInfo().getId());
                Intent intent = new Intent(activity, GroupActivity.class);
                intent.putExtra("group",group);
                activity.startActivity(intent);
            }
        });

    }

    @Override
    public int getItemCount() {
        return groups.size();
    }
    @Override
    public void onAttachedToRecyclerView(RecyclerView recyclerView) {
        super.onAttachedToRecyclerView(recyclerView);
    }

    public static class GroupViewHolder extends RecyclerView.ViewHolder {
        CardView cv;
        TextView typeOfEducation;
        TextView name;
        TextView users;
        TextView tags;
        TextView cost;

        public GroupViewHolder(View itemView){
            super(itemView);
            name=itemView.findViewById(R.id.name_of_group);
            users=itemView.findViewById(R.id.participants);
            tags=itemView.findViewById(R.id.tags);
            cost=itemView.findViewById(R.id.cost);
            typeOfEducation=itemView.findViewById(R.id.type_of_education);
            cv=itemView.findViewById(R.id.group_card);



        }




}

}


