package ru.lod_misis.user.eduhub.Adapters;

import android.app.Activity;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.IInviteCallback;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserSearchProfile;

import com.example.user.eduhub.R;

import java.util.List;

/**
 * Created by User on 06.02.2018.
 */

public class InviteUsersAdapter extends RecyclerView.Adapter<InviteUsersAdapter.InviteUsersViewHolder>  {
    private List<UserSearchProfile> userSearchProfiles;
    private Activity activity;
    private String myId;
    private String groupId;
    private String role;
    IInviteCallback inviteCallback;

    FakesButton fakesButton=new FakesButton();

    public InviteUsersAdapter(List<UserSearchProfile> userSearchProfiles, Activity activity,IInviteCallback iInviteCallback){
        this.userSearchProfiles=userSearchProfiles;
        this.activity=activity;
        this.inviteCallback=iInviteCallback;


    }
    @Override
    public InviteUsersAdapter.InviteUsersViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.invite_users_item, parent, false);
        InviteUsersAdapter.InviteUsersViewHolder gvh = new InviteUsersViewHolder(v);
        return gvh;
    }

    @Override
    public void onBindViewHolder(InviteUsersAdapter.InviteUsersViewHolder holder, final int position) {
        if(userSearchProfiles.get(position).getInvited()!=null && userSearchProfiles.get(position).getInvited()){
            holder.cv.setVisibility(View.GONE);
        }
        holder.name.setText(userSearchProfiles.get(position).getUsername());
        holder.cv.setOnClickListener(click->{
            Log.d("sdvsd","sdfsdgsdf");
            inviteCallback.InviteCallBack(userSearchProfiles.get(position).getId());
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
