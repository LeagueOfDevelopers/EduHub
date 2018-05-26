package ru.lod_misis.user.eduhub.Adapters;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Classes.TypeOfEducation;
import ru.lod_misis.user.eduhub.GroupActivity;
import ru.lod_misis.user.eduhub.Main2Activity;
import ru.lod_misis.user.eduhub.MainActivity;
import ru.lod_misis.user.eduhub.Models.Group.Group;

import com.example.user.eduhub.R;
import com.squareup.picasso.Picasso;
import com.xiaofeng.flowlayoutmanager.FlowLayoutManager;

import java.util.ArrayList;

/**
 * Created by user on 13.12.2017.
 */

public class GroupAdapter extends RecyclerView.Adapter<GroupAdapter.GroupViewHolder>{
    private ArrayList<Group> groups;
    private Activity activity;
    private Context context;
    Boolean flag=false;

    public void setFlag(Boolean flag) {
        this.flag = flag;
    }

    public GroupAdapter(ArrayList<Group> groups, Activity activity, Context context){
        this.groups=groups;
        this.activity=activity;
        this.context=context;
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
        holder.users.setText(groups.get(position).getGroupInfo().getMemberAmount()+"/"+groups.get(position).getGroupInfo().getSize());
        holder.cost.setText(""+groups.get(position).getGroupInfo().getCost());
        FlowLayoutManager flowLayoutManager=new FlowLayoutManager();
        holder.tags.setLayoutManager(flowLayoutManager);
        TagsAdapter adapter=new TagsAdapter((ArrayList<String>) groups.get(position).getGroupInfo().getTags());
        holder.tags.setAdapter(adapter);


       /* switch (String.valueOf(groups.get(position).getGroupInfo().getGroupType())){
            case "1":{holder.typeOfEducation.setText(TypeOfEducation.Лекция.toString());break;}
            case "2":{holder.typeOfEducation.setText(TypeOfEducation.Семинар.toString());break;}
            case "3":{holder.typeOfEducation.setText(TypeOfEducation.МастерКласс.toString());break;}
        }*/
        holder.cv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!flag){
                Group group=groups.get(position);
                Log.d("gROUPiD",group.getGroupInfo().getId());
                Intent intent = new Intent(activity, GroupActivity.class);
                intent.putExtra("group",group);
                activity.startActivity(intent);}else{
                    Intent intent = new Intent(activity, MainActivity.class);
                    activity.startActivity(intent);
                }
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
        //TextView typeOfEducation;
        TextView name;
        TextView users;
        RecyclerView tags;
        TextView cost;

        public GroupViewHolder(View itemView){
            super(itemView);
            name=itemView.findViewById(R.id.name_of_group);
            users=itemView.findViewById(R.id.participants);
            tags=itemView.findViewById(R.id.recycler_tags);
            cost=itemView.findViewById(R.id.cost);
           // typeOfEducation=itemView.findViewById(R.id.type_of_education);
            cv=itemView.findViewById(R.id.group_card);



        }




}

}


