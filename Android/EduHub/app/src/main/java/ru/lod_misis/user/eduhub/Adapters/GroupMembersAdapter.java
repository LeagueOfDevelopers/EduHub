package ru.lod_misis.user.eduhub.Adapters;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.AnotherProfileActivity;
import ru.lod_misis.user.eduhub.Classes.MemberRole;
import ru.lod_misis.user.eduhub.Interfaces.IUpdateList;
import ru.lod_misis.user.eduhub.Interfaces.View.IFileRepositoryView;
import ru.lod_misis.user.eduhub.Models.AddFileResponseModel;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.Member;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.FileRepository;
import com.example.user.eduhub.R;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import java.io.IOException;
import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import okhttp3.ResponseBody;

/**
 * Created by User on 04.01.2018.
 */

public class GroupMembersAdapter extends RecyclerView.Adapter<GroupMembersAdapter.GroupMembersViewHolder>implements IFileRepositoryView {
    private ArrayList<Member> members;
    private User user;
    int role=-1;
    Group group;
    ImageView userImage2;
    Activity activity;
    IUpdateList updateList;
    Bitmap bitmap;
    Context context;
    EduHubApi eduHubApi;
    ResponseBody result;


    public GroupMembersAdapter (ArrayList<Member> members, User user, Activity activity, Group group,IUpdateList updateList,Context context){
        this.members=members;
        this.user=user;
        this.group=group;
        this.updateList=updateList;
        this.activity=activity;
        fileRepository=new FileRepository(this,context);
        eduHubApi=RetrofitBuilder.getApi(context);

    }
    public GroupMembersAdapter (ArrayList<Member> members,  Activity activity, Group group,IUpdateList updateList,Context context){
        this.members=members;
        this.group=group;
        this.updateList=updateList;
        this.activity=activity;
        eduHubApi=RetrofitBuilder.getApi(context);
        fileRepository=new FileRepository(this,context);
        user=null;

    }
    FileRepository fileRepository;
    @Override
    public GroupMembersViewHolder onCreateViewHolder(ViewGroup parent, int i) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.group_members_item, parent, false);
        GroupMembersViewHolder gvh = new GroupMembersViewHolder(v);
        return gvh;

    }

    @Override
    public void onBindViewHolder(GroupMembersViewHolder holder, int i) {
        if(user!=null){
        Log.d("User",user.getUserId());
        for (Member member:members) {
            Log.d("memberId",member.getUserId());
            if(member.getUserId().equals(user.getUserId())){
                role=member.getRole();
                Log.d("Role",role+"");
            }

        }
        }
        if(members.get(i).getAvatarLink()==null){
        }
        else {
            fileRepository.loadFileFromServer(user.getToken(),members.get(i).getAvatarLink());
            eduHubApi.loafFileFromServer("Bearer "+user.getToken(),members.get(i).getAvatarLink())

                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(next->{

                                result=next;
                            },
                            throwable -> {Log.e("GetFile",throwable.toString());},
                            ()->{
                                getFile2(result,holder.userImage);
                            });
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
        holder.cv.setOnTouchListener(new View.OnTouchListener(){

            long startTime;

            @Override
            public boolean onTouch(View v, MotionEvent event) {
                switch (event.getAction()) {
                    case MotionEvent.ACTION_DOWN: // нажатие
                        startTime = System.currentTimeMillis();

                        break;
                    case MotionEvent.ACTION_MOVE: // движение
                        break;
                    case MotionEvent.ACTION_UP:
                        if(user!=null){
                        long totalTime = System.currentTimeMillis() - startTime;
                        long totalSecunds = totalTime / 1000;
                        if( totalSecunds >= 2 )
                        {
                            if(!user.getUserId().equals(members.get(i).getUserId())&&role==2) {
                                holder.kick.setVisibility(View.VISIBLE);
                            }
                        }else{
                            Intent intent=new Intent(activity, AnotherProfileActivity.class);
                            intent.putExtra("id",members.get(i).getUserId());
                            activity.startActivity(intent);
                        }
                        }// отпускание

                    case MotionEvent.ACTION_CANCEL:

                        break;
                }
                return true;
            }
        });
       holder.kick.setOnClickListener(click->{
           Log.d("GroupIdInMemberAdapter",user.getToken());
           EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
           if(members.get(i).getRole()==3){
               eduHubApi.exitFromGroupForTeacher(user.getToken(),group.getGroupInfo().getId())
                       .subscribeOn(Schedulers.io())
                       .observeOn(AndroidSchedulers.mainThread())
                       .subscribe(()->{
                           updateList.updateList();}
                       );
           }else{



           eduHubApi.exitFromGroup("Bearer "+user.getToken(),group.getGroupInfo().getId(),members.get(i).getUserId())
                   .subscribeOn(Schedulers.io())
                   .observeOn(AndroidSchedulers.mainThread())
                   .subscribe(()->{
                           updateList.updateList();}
                   );}
       });
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
        byte[] rawBitmap;
        try {

            rawBitmap=file.bytes();
            Log.d("Проверяемчтозахрень тут",rawBitmap[5]+"");
            bitmap = BitmapFactory.decodeByteArray(rawBitmap,0,rawBitmap.length);
            userImage2.setImageBitmap(bitmap);


        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    private void getFile2(ResponseBody file,ImageView imageView) {
        byte[] rawBitmap;
        try {

            rawBitmap=file.bytes();
            Log.d("Проверяемчтозахрень тут",rawBitmap[5]+"");
            bitmap = BitmapFactory.decodeByteArray(rawBitmap,0,rawBitmap.length);
            imageView.setImageBitmap(bitmap);


        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public static class GroupMembersViewHolder extends RecyclerView.ViewHolder{
       public  ImageView userImage;
        ImageView paid;
        ImageView kick;
        TextView userName;
        TextView userRole;
        CardView cv;
        public GroupMembersViewHolder(View itemView) {
            super(itemView);
            paid=itemView.findViewById(R.id.paid);
            userImage=itemView.findViewById(R.id.UserImage);
            userName=itemView.findViewById(R.id.UserName);
            userRole=itemView.findViewById(R.id.UserRole);
            cv=itemView.findViewById(R.id.member_card);
            kick=itemView.findViewById(R.id.kick);

        }
    }
}
