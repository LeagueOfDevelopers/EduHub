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
        holder.timeLeft.setText(groups.get(position).getDeadLine().toString());
        holder.tags.setText(groups.get(position).getTags().toString());

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
        TextView name;
        TextView users;
        TextView tags;
        TextView timeLeft;

        public GroupViewHolder(View itemView){
            super(itemView);
            name=itemView.findViewById(R.id.groupName);
            users=itemView.findViewById(R.id.participants);
            tags=itemView.findViewById(R.id.tags);
            timeLeft=itemView.findViewById(R.id.timeLeft);


        }




}
}


