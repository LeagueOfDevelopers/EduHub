package com.example.user.eduhub.Adapters;

import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.user.eduhub.Classes.Group;
import com.example.user.eduhub.R;

import java.util.ArrayList;
import java.util.Date;

/**
 * Created by user on 13.12.2017.
 */

public class GroupAdapter extends RecyclerView.Adapter<GroupAdapter.GroupViewHolder>{
    private ArrayList<Group> groups;
    public GroupAdapter(ArrayList<Group> groups){
        this.groups=groups;
    }
    @Override
    public GroupViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.group_card, parent, false);
        GroupViewHolder gvh = new GroupViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(GroupViewHolder holder, int position) {
        holder.name.setText(groups.get(position).getName());
        holder.users.setText(groups.get(position).getUsersNow()+"/"+groups.get(position).getMaxUsers());
        holder.cost.setText("$"+groups.get(position).getCost());
        holder.tags.setText(groups.get(position).getTags().toString());
        holder.typeOdEducation.setText(groups.get(position).getTypeOfEducation().toString());

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
        TextView typeOdEducation;
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
            typeOdEducation=itemView.findViewById(R.id.type_of_education);



        }




}
}


