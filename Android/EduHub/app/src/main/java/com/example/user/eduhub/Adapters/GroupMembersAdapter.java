package com.example.user.eduhub.Adapters;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by User on 04.01.2018.
 */

public class GroupMembersAdapter extends RecyclerView.Adapter<GroupMembersAdapter.GroupMembersViewHolder> {
    private ArrayList<Member> members;
    public GroupMembersAdapter (ArrayList<Member> members){
        this.members=members;
    }
    @Override
    public GroupMembersViewHolder onCreateViewHolder(ViewGroup parent, int i) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.group_members_item, parent, false);
        GroupMembersViewHolder gvh = new GroupMembersViewHolder(v);
        return gvh;

    }

    @Override
    public void onBindViewHolder(GroupMembersViewHolder holder, int i) {

    }

    @Override
    public int getItemCount() {
        return members.size();

    }

    public static class GroupMembersViewHolder extends RecyclerView.ViewHolder{
        ImageView userImage;
        TextView userName;
        TextView userRole;
        public GroupMembersViewHolder(View itemView) {
            super(itemView);
            userImage=itemView.findViewById(R.id.UserImage);
            userName=itemView.findViewById(R.id.UserName);
            userRole=itemView.findViewById(R.id.UserRole);

        }
    }
}
