package com.example.user.eduhub.Adapters;

import android.app.Activity;
import android.content.Intent;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.AuthorizedUserActivity;
import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Fakes.FakeInviteUserPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IInviteUserView;
import com.example.user.eduhub.Models.UserProfile.UserSearchProfile;
import com.example.user.eduhub.Presenters.InviteUserPresenter;
import com.example.user.eduhub.R;

import java.util.List;

/**
 * Created by User on 06.02.2018.
 */

public class InviteUsersAdapter extends RecyclerView.Adapter<InviteUsersAdapter.InviteUsersViewHolder> implements IInviteUserView {
    private List<UserSearchProfile> userSearchProfiles;
    private Activity activity;
    private String myId;
    private String groupId;
    private MemberRole role;
    private InviteUserPresenter inviteUserPresenter=new InviteUserPresenter(this);
    FakesButton fakesButton=new FakesButton();
    FakeInviteUserPresenter fakeInviteUserPresenter=new FakeInviteUserPresenter(this);
    public InviteUsersAdapter(List<UserSearchProfile> userSearchProfiles, Activity activity,String groupId,MemberRole role,String myId){
        this.userSearchProfiles=userSearchProfiles;
        this.activity=activity;
        this.groupId=groupId;
        this.role=role;
        this.myId=myId;
    }
    @Override
    public InviteUsersAdapter.InviteUsersViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.invite_users_item, parent, false);
        InviteUsersAdapter.InviteUsersViewHolder gvh = new InviteUsersViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(InviteUsersAdapter.InviteUsersViewHolder holder, final int position) {
        holder.name.setText(userSearchProfiles.get(position).getName());
        holder.cv.setOnClickListener(click->{
            if(!fakesButton.getCheckButton()){
            inviteUserPresenter.inviteUser(userSearchProfiles.get(position).getId(),role,groupId,myId);}
            else {
            fakeInviteUserPresenter
                    .inviteUser(userSearchProfiles.get(position).getId(), role,groupId,myId);
            }
        });

    }

    @Override
    public int getItemCount() {
        return userSearchProfiles.size();
    }
    @Override
    public void onAttachedToRecyclerView(RecyclerView recyclerView) {
        super.onAttachedToRecyclerView(recyclerView);
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
    public void getResponse() {
        Intent intent2=new Intent(activity,AuthorizedUserActivity.class);
        activity.startActivity(intent2);

    }

    public static class InviteUsersViewHolder extends RecyclerView.ViewHolder {
        CardView cv;
        TextView name;
        ImageView imageView;

        public InviteUsersViewHolder(View itemView){
            super(itemView);
            name=itemView.findViewById(R.id.invite_name_user);
            cv=itemView.findViewById(R.id.cv);
            imageView=itemView.findViewById(R.id.avatar_user_for_invite_activite);
        }


    }
}
