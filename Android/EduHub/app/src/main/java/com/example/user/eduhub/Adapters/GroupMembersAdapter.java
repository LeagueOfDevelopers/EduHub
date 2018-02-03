package com.example.user.eduhub.Adapters;

import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Models.Group.Member;
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
        holder.userImage.setImageResource(R.drawable.ic_launcher_background);
        Log.e("Error Role",members.get(i).getMemberRole()+"");
        switch (members.get(i).getMemberRole()+""){
            case 0+"":{holder.userRole.setText(MemberRole.Default.toString()); break;}
            case 1+"":{holder.userRole.setText(MemberRole.Member.toString()); break;}
            case 2+"":{holder.userRole.setText(MemberRole.Teacher.toString());break;}
            case 3+"":{holder.userRole.setText(MemberRole.Creator.toString()); break;}
        }
        holder.userName.setText(members.get(i).getName());
        if(members.get(i).getPaid()){
            holder.paid.setImageResource(R.drawable.ic_wallet_24dp);
        }
    }

    @Override
    public int getItemCount() {
        return members.size();

    }

    public static class GroupMembersViewHolder extends RecyclerView.ViewHolder{
        ImageView userImage;
        ImageView paid;
        TextView userName;
        TextView userRole;
        public GroupMembersViewHolder(View itemView) {
            super(itemView);
            paid=itemView.findViewById(R.id.paid);
            userImage=itemView.findViewById(R.id.UserImage);
            userName=itemView.findViewById(R.id.UserName);
            userRole=itemView.findViewById(R.id.UserRole);

        }
    }
}
