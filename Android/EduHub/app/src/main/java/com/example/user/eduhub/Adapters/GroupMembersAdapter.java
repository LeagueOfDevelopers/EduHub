package com.example.user.eduhub.Adapters;

import android.app.Activity;
import android.graphics.drawable.Drawable;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Interfaces.View.IFileRepositoryView;
import com.example.user.eduhub.Models.AddFileResponseModel;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.FileRepository;
import com.example.user.eduhub.R;

import java.io.File;
import java.util.ArrayList;

import okhttp3.ResponseBody;

/**
 * Created by User on 04.01.2018.
 */

public class GroupMembersAdapter extends RecyclerView.Adapter<GroupMembersAdapter.GroupMembersViewHolder>implements IFileRepositoryView {
    private ArrayList<Member> members;
    private User user;
    ImageView userImage2;
    Activity activity;
    public GroupMembersAdapter (ArrayList<Member> members, User user, Activity activity){
        this.members=members;
        this.user=user;
    }
    FileRepository fileRepository=new FileRepository(this,activity);
    @Override
    public GroupMembersViewHolder onCreateViewHolder(ViewGroup parent, int i) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.group_members_item, parent, false);
        GroupMembersViewHolder gvh = new GroupMembersViewHolder(v);
        return gvh;

    }

    @Override
    public void onBindViewHolder(GroupMembersViewHolder holder, int i) {
        if(members.get(i).getAvatarLink()==null){
        holder.userImage.setImageResource(R.drawable.ic_launcher_background);}
        else {
            fileRepository.loadFileFromServer(user.getToken(),members.get(i).getAvatarLink());
            userImage2=holder.userImage;

        }
        Log.e("Error Role",members.get(i).getRole()+"");
        switch (members.get(i).getRole()+""){
            case 1+"":{holder.userRole.setText(MemberRole.Участник.toString()); break;}
            case 2+"":{holder.userRole.setText(MemberRole.Создатель.toString());break;}
            case 3+"":{holder.userRole.setText(MemberRole.Учитель.toString()); break;}
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
    public void getResponse(AddFileResponseModel addFileResponseModel) {

    }

    @Override
    public void getFile(ResponseBody file) {

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
